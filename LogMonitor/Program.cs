using System;
using System.IO;
using System.Linq;

namespace LogMonitor
{
    public class Program
    {
        private const string EXAMPLES_FOLDER = "examples";

        public static void Main(string[] args)
        {
            var argsList = args.ToList();

            if (!argsList.Contains("-t") && !argsList.Contains("-f"))
            {
                Console.WriteLine("Invalid arguments");
                return;
            }

            var thresholdIndex = argsList.FindIndex(arg => arg.Equals("-t"));

            if (args.Length < 2)
            {
                Console.WriteLine("Invalid number of arguments");
                return;
            }

            if (args.Length > thresholdIndex)
            {
                double threshold = -1;
                if (!Double.TryParse(args[thresholdIndex + 1], out threshold) && thresholdIndex != -1)
                {
                    Console.WriteLine("Threshold value is not a valid number");
                    return;
                }

                var fileIndex = argsList.FindIndex(arg => arg.Equals("-f"));
                var files = argsList.Where(f => argsList.IndexOf(f) > fileIndex);

                if (!files.Any())
                {
                    Console.WriteLine("Please specify at least one file name");
                    return;
                }

                var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var examplesPath = Path.Combine(basePath, EXAMPLES_FOLDER);

                Console.WriteLine($"Log Monitoring Started at {DateTime.Now}");
                var logParser = new LogParser(threshold);
                logParser.Parse(files);
            }
        }
    }
}
