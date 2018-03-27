using LogMonitor.Utils;
using System;
using Utils;

namespace LogMonitor.Domain.Notification
{
    public class Alert : Notification
    {
        private double _hits;
        private double _threshold;
        private double _average;
        private DateTime _raisedAt;

        public Alert(double hits, double threshold, double average) : base()
        {
            _hits = hits;
            _threshold = threshold;
            _average = average;
            _raisedAt = DateTime.Now;
        }

        public override void Notify()
        {
            lock (GlobalLocks.WriteLock)
            {
                _printer.Print(string.Format(Constants.ALERT_TRIGGERED, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT), _hits, _raisedAt));
                _printer.Print(string.Format(Constants.ALERT_AVERAGE, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT), _threshold, _average));
            }
        }

        public DateTime RaisedAt
            => _raisedAt;
    }
}
