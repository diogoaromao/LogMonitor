using System;

namespace LogMonitor.Domain.Notification
{
    public class Alert : Notification
    {
        private double _hits;
        private double _threshold;
        private double _average;

        public Alert(double hits, double threshold, double average)
        {
            _hits = hits;
            _threshold = threshold;
            _average = average;
        }

        public override void Notify()
        {
            Console.WriteLine($"High traffic generated an alert - hits = {_hits}, triggered at {DateTime.Now}");
            Console.WriteLine($"Threshold = {_threshold}, Average = {_average}B");
        }
    }
}
