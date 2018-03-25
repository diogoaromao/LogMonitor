using LogMonitor.Domain.Timer;
using System.Collections.Generic;

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
            new StatusTimerMonitor(30000, _files);
            new AlertTimerMonitor(60000, _files, _threshold);

            while (true)
            {

            }
        }
    }
}
