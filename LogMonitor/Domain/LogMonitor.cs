using LogMonitor.Domain.Timer;
using System.Collections.Generic;

namespace LogMonitor.Domain
{
    public class LogMonitor
    {
        private string _file;
        private double _threshold;

        public LogMonitor(string file, double threshold)
        {
            _file = file;
            _threshold = threshold;
        }

        public void Monitor()
        {
            new StatusTimerMonitor(30000, _file);
            new AlertTimerMonitor(60000, _file, _threshold);

            while (true)
            {

            }
        }
    }
}
