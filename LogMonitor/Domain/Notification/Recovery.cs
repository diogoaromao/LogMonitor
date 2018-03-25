using System;

namespace LogMonitor.Domain.Notification
{
    public class Recovery : Notification
    {
        public Recovery() { }

        public override void Notify()
        {
            Console.WriteLine($"Recovered from last alert at {DateTime.Now}");
        }
    }
}
