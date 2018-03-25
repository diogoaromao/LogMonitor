using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogMonitor.Domain.Timer
{
    public class CumulativeTimerMonitor : StatusTimerMonitor
    {
        public CumulativeTimerMonitor(long time, string file) : base(time, file, true) { }

        protected override void printTopHits()
        {
            if (!_pageHits.Any())
            {
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: No websites have been visited");
                return;
            }

            var orderedHits = _pageHits.OrderByDescending(pair => pair.Value);

            INotification notification = new CumulativeStatus(orderedHits);
            printNotification(notification);
        }
    }
}
