using System;
using System.Linq;

namespace LogMonitor.Utils
{
    public class InputValidation
    {
        private string[] _args;

        public InputValidation(string[] args)
        {
            _args = args;
        }

        public void Validate()
        {
            var argsList = _args.ToList();

            if (!argsList.Contains("-t") && !argsList.Contains("-f"))
            {
                Console.WriteLine(Constants.INVALID_ARGUMENTS);
                return;
            }

            var thresholdIndex = argsList.FindIndex(arg => arg.Equals("-t"));

            if (_args.Length < 2)
            {
                Console.WriteLine(Constants.INVALID_NUMBER_OF_ARGUMENTS);
                return;
            }

            if (_args.Length > thresholdIndex)
            {
                double threshold = -1;
                if (!Double.TryParse(_args[thresholdIndex + 1], out threshold) && thresholdIndex != -1)
                {
                    Console.WriteLine(Constants.INVALID_THRESHOLD);
                    return;
                }

                var fileIndex = argsList.FindIndex(arg => arg.Equals("-f"));
                var files = argsList.Where(f => argsList.IndexOf(f) > fileIndex);

                if (!files.Any())
                {
                    Console.WriteLine(Constants.INVALID_FILENAME);
                    return;
                }

                Console.WriteLine(string.Format(Constants.LOG_MONITORING_STARTED, DateTime.Now));

                var logMonitor = new Domain.LogMonitor(files, threshold);
                logMonitor.Monitor();
            }
        }
    }
}
