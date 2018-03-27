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
        private KeyValuePair<string, int> _host;

        public CumulativeStatus(IOrderedEnumerable<KeyValuePair<string, int>> orderedWebsites, KeyValuePair<string, int> host) : base()
        {
            _orderedWebsites = orderedWebsites;
            _host = host;
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

                _printer.Print(string.Format(Constants.HOST_VISITS, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)));
            }
        }
    }
}
