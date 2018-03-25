using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using Utils;

namespace LogMonitor.Domain.Notification
{
    public class PeriodicStatus : Notification
    {
        IEnumerable<KeyValuePair<string, List<string>>> _mostVisitedSections;

        public PeriodicStatus(IEnumerable<KeyValuePair<string, List<string>>> mostVisitedSections) : base()
        {
            _mostVisitedSections = mostVisitedSections;
        }

        public override void Notify()
        {
            lock (GlobalLocks.WriteLock)
            {
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: Most visited websites since last check:");
                foreach (var site in _mostVisitedSections)
                {
                    _printer.Print($"Website: {site.Key}");
                    _printer.Print("Sections:");

                    foreach (var value in site.Value)
                    {
                        _printer.Print($"{value}");
                    }
                }
            }
        }
    }
}
