using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Domain.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace LogMonitor.Domain
{
    public class LogMonitor
    {
        private ConcurrentDictionary<string, int> _pageHits;
        private ConcurrentDictionary<string, List<string>> _sections;
        private LogParser _logParser;
        private IEnumerable<string> _files;
        private double _threshold;
        private double _hits;
        private double _sumBytes;

        private readonly Object lockObj = new Object();

        public LogMonitor(IEnumerable<string> files, double threshold)
        {
            _pageHits = new ConcurrentDictionary<string, int>();
            _sections = new ConcurrentDictionary<string, List<string>>();
            _logParser = new LogParser(); // to become singleton
            _files = files;
            _threshold = threshold;
        }

        public void Monitor()
        {
            setTimers();

            while (true)
            {
                foreach (var file in _files)
                {
                    Thread thread = new Thread(() => parseContent(file));
                    thread.Start();
                }
            }
        }

        private void parseContent(string file)
        {
            var lines = _logParser.ParseContent(file);
            lock (lockObj)
            {
                foreach (var line in lines)
                {
                    _sections.AddOrUpdate(line.Website, new List<string> { line.Section }, (site, sections) =>
                    {
                        if (!sections.Contains(line.Section))
                            sections.Add(line.Section);

                        return sections;
                    });

                    _pageHits.AddOrUpdate(line.Website, 1, (id, count) => count + 1);

                    _sumBytes += line.Size;
                    _hits++;
                }
            }
        }

        private void setTimers()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
            timer.Interval = 60000;
            timer.Enabled = true;

            if (_threshold > -1)
            {
                System.Timers.Timer alertTimer = new System.Timers.Timer();
                alertTimer.Elapsed += new ElapsedEventHandler(onAlertTimedEvent);
                alertTimer.Interval = 120000;
                alertTimer.Enabled = true;
            }
        }

        private void onTimedEvent(object source, ElapsedEventArgs e)
        {
            getTopHits();
            _pageHits.Clear();
        }

        private void onAlertTimedEvent(object source, ElapsedEventArgs e)
        {
            lock (lockObj)
            {
                var average = getAverage(_sumBytes, _hits);
                if (average > _threshold)
                {
                    INotification notification = new Alert(_hits, _threshold, average);
                    printNotification(notification);
                }
            }

            _sumBytes = _hits = 0;
        }

        private void getTopHits()
        {
            lock (lockObj)
            {
                var maxValue = _pageHits.Aggregate((h1, h2) => h1.Value > h2.Value ? h1 : h2).Value;
                List<string> keys = _pageHits.Where(pair => pair.Value == maxValue)
                                    .Select(pair => pair.Key)
                                    .ToList();

                var mostVisited = _pageHits.Where(item => keys.Contains(item.Key))
                                    .Select(pair => pair.Key)
                                    .ToList();

                var mostVisitedSections = _sections.Where(item => mostVisited.Contains(item.Key));
                INotification notification = new Status(mostVisitedSections);
                printNotification(notification);
            }
        }

        private double getAverage(double bytes, double hits)
            => hits != 0 ? bytes / hits : 0;

        private void printNotification(INotification notification)
        {
            notification.Notify();
        }
    }
}
