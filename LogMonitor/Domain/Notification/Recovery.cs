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
                _printer.Print(string.Format(Constants.RECOVERED, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT), _recoveredAt));
            }
        }
    }
}
