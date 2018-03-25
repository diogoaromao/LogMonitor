using LogMonitor.Utils;
using System;
using Utils;

namespace LogMonitor.Domain.Notification
{
    public class Recovery : Notification
    {
        private DateTime _recoveredAt;

        public Recovery(Alert alert) : base()
        {
            _recoveredAt = alert.RaisedAt;
        }

        public override void Notify()
        {
            lock (GlobalLocks.WriteLock)
            {
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: Recovered from alert triggered at {_recoveredAt}.");
            }
        }
    }
}
