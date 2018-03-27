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
                _printer.Print(string.Format(Constants.RECENT_WEBSITES_VISITED, DateTime.Now));
                foreach (var site in _mostVisitedSections)
                {
                    _printer.Print(string.Format(Constants.WEBSITE, site.Key));
                    _printer.Print(Constants.SECTIONS);

                    foreach (var value in site.Value)
                    {
                        _printer.Print($"{value}");
                    }
                }
            }
        }
    }
}
