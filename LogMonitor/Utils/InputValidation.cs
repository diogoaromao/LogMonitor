using Generator;
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

            if (!argsList.Contains("-t") && !argsList.Contains("-f") && !argsList.Contains("-g"))
            {
                Console.WriteLine(Constants.INVALID_ARGUMENTS);
                return;
            }

            var thresholdIndex = argsList.FindIndex(arg => arg.Equals("-t"));

            if (argsList.Count() < 2)
            {
                Console.WriteLine(Constants.INVALID_NUMBER_OF_ARGUMENTS);
                return;
            }

            if (argsList.Count() > thresholdIndex)
            {
                double threshold = -1;
                if (!Double.TryParse(argsList.ElementAt(thresholdIndex + 1), out threshold) && thresholdIndex != -1)
                {
                    Console.WriteLine(Constants.INVALID_THRESHOLD);
                    return;
                }

                var fileIndex = argsList.FindIndex(arg => arg.Equals("-f"));
                var file = argsList.Where(f => argsList.IndexOf(f) > fileIndex)
                                .FirstOrDefault();

                if (file == null)
                {
                    Console.WriteLine(Constants.INVALID_FILENAME);
                    return;
                }

                if (argsList.Contains("-g") && threshold > -1)
                {
                    var generator = new LogGenerator(threshold);
                    generator.Generate();
                }
                else
                {
                    Console.WriteLine(string.Format(Constants.LOG_MONITORING_STARTED, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)));

                    var logMonitor = new Domain.LogMonitor(file, threshold);
                    logMonitor.Monitor();
                }
            }
        }
    }
}
