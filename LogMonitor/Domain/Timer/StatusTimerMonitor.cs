using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LogMonitor.Domain.Timer
{
    public class StatusTimerMonitor : TimerMonitor
    {
        private ConcurrentDictionary<string, int> _pageHits;
        private ConcurrentDictionary<string, List<string>> _sections;

        public StatusTimerMonitor(long time, IEnumerable<string> files) : base(time, files)
        {
            _pageHits = new ConcurrentDictionary<string, int>();
            _sections = new ConcurrentDictionary<string, List<string>>();
        }

        protected override void parseContent(string file)
        {
            _sections.Clear();
            _pageHits.Clear();

            var lines = _logParser.ParseContent(file);
            /* lock (lockObj)
            {*/
            foreach (var line in lines)
            {
                if (isLineInvalid(line))
                    continue;

                _sections.AddOrUpdate(line.Website, new List<string> { line.Section }, (site, sections) =>
                {
                    if (!sections.Contains(line.Section))
                        sections.Add(line.Section);

                    return sections;
                });

                _pageHits.AddOrUpdate(line.Website, 1, (id, count) => count + 1);
            }
            // }

            getTopHits();
        }

        private void getTopHits()
        {
            /* lock (lockObj)
            {*/
            if(!_pageHits.Any())
                Console.WriteLine($"No logs detected for the past {_time / 1000} seconds at {DateTime.Now}");

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
            //}
        }
    }
}
