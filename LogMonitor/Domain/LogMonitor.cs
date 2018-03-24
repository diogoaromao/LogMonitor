using LogMonitor.Domain.Notification;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Domain.Parser;
using LogMonitor.Domain.Timer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace LogMonitor.Domain
{
    public class LogMonitor
    {
        private IEnumerable<string> _files;
        private double _threshold;

        public LogMonitor(IEnumerable<string> files, double threshold)
        {
            _files = files;
            _threshold = threshold;
        }

        public void Monitor()
        {
            new StatusTimerMonitor(15000, _files);
            new AlertTimerMonitor(30000, _files, _threshold);

            while (true)
            {

            }
        }
    }
}
