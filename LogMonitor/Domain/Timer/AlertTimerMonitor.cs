using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using System;
using System.Collections.Generic;

namespace LogMonitor.Domain.Timer
{
    public class AlertTimerMonitor : TimerMonitor
    {
        private double _threshold;
        private double _hits;
        private double _sumBytes;

        private Queue<INotification> _alerts;

        public AlertTimerMonitor(long runFrequency, long time, string file, double threshold) : base(runFrequency, time, file)
        {
            _threshold = threshold;
            _alerts = new Queue<INotification>();
        }

        protected override void parseContent(string file)
        {
            DateTimeOffset dateTime = DateTimeOffset.Now;

            _sumBytes = _hits = 0;

            var lines = _logParser.ParseContent(file);

            foreach (var line in lines)
            {
                if (isLineInvalid(line, dateTime))
                    continue;

                _sumBytes += line.Size;
                _hits++;
            }

            var average = getAverage(_sumBytes, _hits);
            if (average > _threshold)
            {
                INotification notification = new Alert(_hits, _threshold, average);
                printNotification(notification);

                _alerts.Enqueue(notification);
            }
            else if (_alerts.Count != 0)
            {
                foreach (var alert in _alerts)
                {
                    INotification notification = new Recovery((Alert)alert);
                    printNotification(notification);
                }

                _alerts.Clear();
            }
        }

        private double getAverage(double bytes, double hits)
            => hits != 0 ? bytes / hits : 0;
    }
}
