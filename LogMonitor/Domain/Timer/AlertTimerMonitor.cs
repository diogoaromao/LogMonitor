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

        // private List<Thread> _threads;

        public AlertTimerMonitor(long time, IEnumerable<string> files, double threshold) : base(time, files)
        {
            _threshold = threshold;
            // _threads = new List<Thread>();
        }

        protected override void parseContent(string file)
        {
            _sumBytes = _hits = 0;

            var lines = _logParser.ParseContent(file);
            /* lock (lockObj)
            { */
                foreach (var line in lines)
                {
                    _sumBytes += line.Size;
                    _hits++;
                }
            // }

            var average = getAverage(_sumBytes, _hits);
            if (average > _threshold)
            {
                INotification notification = new Alert(_hits, _threshold, average);
                printNotification(notification);
            }
        }

        private double getAverage(double bytes, double hits)
            => hits != 0 ? bytes / hits : 0;
    }
}
