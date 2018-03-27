using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace LogMonitor.Domain.Notification
{
    public class CumulativeStatus : Notification
    {
        private IOrderedEnumerable<KeyValuePair<string, int>> _orderedWebsites;
        private IEnumerable<KeyValuePair<string, int>> _hosts;

        public CumulativeStatus(IOrderedEnumerable<KeyValuePair<string, int>> orderedWebsites, IEnumerable<KeyValuePair<string, int>> hosts) : base()
        {
            _orderedWebsites = orderedWebsites;
            _hosts = hosts;
        }

        public override void Notify()
        {
            lock (GlobalLocks.WriteLock)
            {
                _printer.Print(string.Format(Constants.WEBSITES_VISITED, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)));

                foreach (var kvp in _orderedWebsites)
                {
                    _printer.Print(string.Format(Constants.WEBSITES_VISITED_COUNT, kvp.Key, kvp.Value));
                }

                _printer.Print(string.Format(Constants.HOSTS_VISITS, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)));

                foreach(var kvp in _hosts)
                {
                    _printer.Print(string.Format(Constants.HOSTS_TRAFFIC, kvp.Key, kvp.Value));
                }
            }
        }
    }
}
