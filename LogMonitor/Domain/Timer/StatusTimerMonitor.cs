using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LogMonitor.Domain.Timer
{
    public class StatusTimerMonitor : TimerMonitor
    {
        private Dictionary<string, int> _pageHits;
        private Dictionary<string, List<string>> _sections;

        public StatusTimerMonitor(long time, string file) : base(time, file)
        {
            _pageHits = new Dictionary<string, int>();
            _sections = new Dictionary<string, List<string>>();
        }

        protected override void parseContent(string file)
        {
            _sections.Clear();
            _pageHits.Clear();

            var lines = _logParser.ParseContent(file);
            foreach (var line in lines)
            {
                if (isLineInvalid(line))
                    continue;

                /*_sections.AddOrUpdate(line.Website, new List<string> { line.Section }, (site, sections) =>
                {
                    if (!sections.Contains(line.Section))
                        sections.Add(line.Section);

                    return sections;
                });

                _pageHits.AddOrUpdate(line.Website, 1, (id, count) => count + 1);*/

                if (_sections.ContainsKey(line.Website))
                {
                    var sections = _sections[line.Website];
                    if (!sections.Contains(line.Section))
                    {
                        sections.Add(line.Section);
                        _sections[line.Website] = sections;
                    }
                }
                else
                {
                    var sections = new List<string>();
                    sections.Add(line.Section);
                    _sections.Add(line.Website, sections);
                }

                if(_pageHits.ContainsKey(line.Website))
                {
                    _pageHits[line.Website] = ++_pageHits[line.Website];
                }
                else
                {
                    _pageHits.Add(line.Website, 1);
                }
            }

            getTopHits();
        }

        private void getTopHits()
        {
            if (!_pageHits.Any())
            {
                Console.WriteLine($"No logs detected for the past {_time / 1000} seconds at {DateTime.Now}");
                return;
            }

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
}
