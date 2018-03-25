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
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: High traffic generated an alert - hits = {_hits}, triggered at {_raisedAt}.");
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: Threshold = {_threshold}, Average = {_average} bytes.");
            }
        }

        public DateTime RaisedAt
            => _raisedAt;
    }
}
