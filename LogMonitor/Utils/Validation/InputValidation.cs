using Generator;
using LogMonitor.Utils.Logger;
using System;
using System.IO;
using System.Linq;

namespace LogMonitor.Utils.Validation
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

                if (threshold > -1)
                {
                    if (argsList.Contains("-g"))
                    {
                        var generator = new LogGenerator(threshold);
                        generator.Generate();
                    }
                    else if(argsList.Contains("-f"))
                    {
                        var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                        var filePath = Path.Combine(basePath, Constants.EXAMPLES_FOLDER, file);
                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine(string.Format(Constants.FILE_DOES_NOT_EXIST, filePath));
                            return;
                        }

                        var resultFilePath = Path.Combine(basePath, Constants.RESULTS_FOLDER, file);

                        var printer = Printer.Instance;
                        printer.SetFilePath(resultFilePath);

                        if (File.Exists(resultFilePath))
                            File.Delete(resultFilePath);

                        printer.Print(string.Format(Constants.LOG_MONITORING_STARTED, DateTime.Now.ToString(Constants.DATETIME_LOG_FORMAT)));

                        var logMonitor = new Domain.LogMonitor(file, threshold);
                        logMonitor.Monitor();
                    }
                    else
                    {
                        Console.WriteLine(Constants.ARGUMENTS_EXPECTED);
                    }
                }
                else
                {
                    Console.WriteLine(Constants.THRESHOLD_POSITIVE_VALUE);
                }
            }
        }
    }
}
