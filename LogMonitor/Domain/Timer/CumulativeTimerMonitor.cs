using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace LogMonitor.Domain.Timer
{
    public class CumulativeTimerMonitor : StatusTimerMonitor
    {
        public CumulativeTimerMonitor(long time, string file) : base(time, file, true) { }

        protected override void printTopHits()
        {
            if (!_pageHits.Any())
                return;

            var orderedHits = _pageHits.OrderByDescending(pair => pair.Value);

            var maxValue = _hosts.Aggregate((h1, h2) => h1.Value > h2.Value ? h1 : h2).Value;
            List<string> keys = _hosts.Where(pair => pair.Value == maxValue)
                                .Select(pair => pair.Key)
                                .ToList();

            var hosts = _hosts.Where(item => keys.Contains(item.Key));

            INotification notification = new CumulativeStatus(orderedHits, hosts);
            printNotification(notification);
        }
    }
}
