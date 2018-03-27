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
                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: Websites visited since monitoring started:");

                foreach (var kvp in _orderedWebsites)
                {
                    _printer.Print($"{kvp.Key} - {kvp.Value} time(s)");
                }

                _printer.Print($"[{DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)}]: Host who generated the most traffic: {_host.Key} - {_host.Value} hits");
            }
        }
    }
}
