using LogMonitor.Domain.Timer;

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
            new PeriodicTimerMonitor(30000, _file);
            new CumulativeTimerMonitor(60000, _file);
            new AlertTimerMonitor(2000, 120000, _file, _threshold);

            while (true)
            {

            }
        }
    }
}
