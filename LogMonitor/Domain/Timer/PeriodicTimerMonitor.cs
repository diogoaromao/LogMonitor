using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace LogMonitor.Domain.Timer
{
    public class PeriodicTimerMonitor : StatusTimerMonitor
    {
        public PeriodicTimerMonitor(long time, string file) : base(time, file, false) { }

        protected override void printTopHits()
        {
            if (!_pageHits.Any())
            {
                lock (GlobalLocks.WriteLock)
                {
                    _printer.Print(string.Format(Constants.NO_RECENT_VISITS, DateTime.Now, (_time / 1000)));
                }
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

            INotification notification = new PeriodicStatus(mostVisitedSections);
            printNotification(notification);
        }
    }
}
