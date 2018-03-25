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

        public CumulativeStatus(IOrderedEnumerable<KeyValuePair<string, int>> orderedWebsites) : base()
        {
            _orderedWebsites = orderedWebsites;
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
            }
        }
    }
}
