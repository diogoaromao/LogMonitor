using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Domain.Parser;
using System;
using System.Collections.Generic;
using System.Timers;

namespace LogMonitor.Domain.Timer
{
    public abstract class TimerMonitor
    {
        private System.Timers.Timer _timer;
        private long _time;
        protected IEnumerable<string> _files;

        protected LogParser _logParser;

        protected readonly Object lockObj = new Object();

        public TimerMonitor(long time, IEnumerable<string> files)
        {
            _time = time;
            _files = files;
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Handle);
            _timer.Interval = time;
            _timer.Enabled = true;

            _logParser = LogParser.Instance;
        }

        protected virtual void Handle(object source, ElapsedEventArgs e)
        {
            foreach (var file in _files)
            {
                /*Thread thread = new Thread(() => parseContent(file));
                _threads.Add(thread);
                thread.Start();*/
                parseContent(file); // let's assume only one file is supplied
            }
        }

        protected abstract void parseContent(string file);

        protected virtual void printNotification(INotification notification)
        {
            notification.Notify();
        }
    }
}
