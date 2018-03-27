using LogMonitor.Domain.DTO;
using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils.Logger;
using LogMonitor.Utils.Parser;
using System;
using System.Timers;

namespace LogMonitor.Domain.Timer
{
    public abstract class TimerMonitor
    {
        private System.Timers.Timer _timer;
        protected long _runFrequency;
        protected long _time;
        protected string _file;

        protected LogParser _logParser;
        protected Printer _printer;

        protected readonly Object lockObj = new Object();

        public TimerMonitor(long runFrequency, long time, string file)
        {
            _runFrequency = runFrequency;
            _time = time;
            _file = file;
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Handle);
            _timer.Interval = runFrequency;
            _timer.Enabled = true;

            _logParser = LogParser.Instance;
            _printer = Printer.Instance;
        }

        protected virtual void Handle(object source, ElapsedEventArgs e)
        {
            parseContent(_file);
        }

        protected abstract void parseContent(string file);

        protected virtual void printNotification(INotification notification)
        {
            notification.Notify();
        }

        protected bool isLineInvalid(LineDTO line, DateTimeOffset dateTime)
            => line == default(LineDTO)
                    || dateTime.Subtract(line.DateTime).TotalMilliseconds > _time
                    || dateTime.Subtract(line.DateTime).TotalMilliseconds < 0
                    || line.Website.Contains("-");
    }
}
