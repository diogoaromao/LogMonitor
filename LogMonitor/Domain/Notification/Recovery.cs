using System;

namespace LogMonitor.Domain.Notification
{
    public class Recovery : Notification
    {
        private DateTime _recoveredAt;

        public Recovery(Alert alert)
        {
            _recoveredAt = alert.RaisedAt;
        }

        public override void Notify()
        {
            Console.WriteLine($"Recovered from alert triggered at {_recoveredAt} at {DateTime.Now}");
        }
    }
}
