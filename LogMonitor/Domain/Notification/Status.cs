using System;
using System.Collections.Generic;

namespace LogMonitor.Domain.Notification
{
    public class Status : Notification
    {
        IEnumerable<KeyValuePair<string, List<string>>> _mostVisitedSections;

        public Status(IEnumerable<KeyValuePair<string, List<string>>> mostVisitedSections)
        {
            _mostVisitedSections = mostVisitedSections;
        }

        public override void Notify()
        {
            Console.WriteLine($"Most visited websites since last check at {DateTime.Now}");
            foreach (var site in _mostVisitedSections)
            {
                Console.WriteLine($"Website: {site.Key}");
                Console.WriteLine("Sections:");
                
                foreach(var value in site.Value)
                {
                    Console.WriteLine($"{value}");
                }
            }
        }
    }
}
