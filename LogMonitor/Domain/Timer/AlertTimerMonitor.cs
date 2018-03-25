using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using System.Collections.Generic;

namespace LogMonitor.Domain.Timer
{
    public class AlertTimerMonitor : TimerMonitor
    {
        private double _threshold;
        private double _hits;
        private double _sumBytes;

        private bool alerted;

        public AlertTimerMonitor(long time, string file, double threshold) : base(time, file)
        {
            _threshold = threshold;
        }

        protected override void parseContent(string file)
        {
            _sumBytes = _hits = 0;

            var lines = _logParser.ParseContent(file);

            foreach (var line in lines)
            {
                if (isLineInvalid(line))
                    continue;

                _sumBytes += line.Size;
                _hits++;
            }

            var average = getAverage(_sumBytes, _hits);
            if (average > _threshold)
            {
                INotification notification = new Alert(_hits, _threshold, average);
                printNotification(notification);
                alerted = true;
            }
            else if(alerted)
            {
                INotification notification = new Recovery();
                printNotification(notification);
                alerted = false;
            }
        }

        private double getAverage(double bytes, double hits)
            => hits != 0 ? bytes / hits : 0;
    }
}
