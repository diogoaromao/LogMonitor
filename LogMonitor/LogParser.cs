using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LogMonitor
{
    public class LogParser
    {
        private const string EXAMPLES_FOLDER = "examples";

        private ConcurrentDictionary<string, int> hits = new ConcurrentDictionary<string, int>();
        private ConcurrentDictionary<string, List<string>> sections = new ConcurrentDictionary<string, List<string>>();

        private int nrHits;
        private double average;
        private double sum = 0;

        private double _threshold;

        private List<string> mostHits = new List<string>();

        private readonly Object obj = new Object();

        public LogParser(double threshold)
        {
            _threshold = threshold;
        }

        private void onTimedEvent(object source, ElapsedEventArgs e)
        {
            getTopHits();
            mostHits.Clear();
            hits.Clear();
        }

        private void onAlertTimedEvent(object source, ElapsedEventArgs e)
        {
            lock(obj)
            {
                average = sum / nrHits;
                if (average > _threshold)
                    printAlert();

                average = sum = nrHits = 0;
            }
        }

        public void Parse(IEnumerable<string> files)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
            timer.Interval = 60000;
            timer.Enabled = true;

            if(_threshold > -1)
            {
                System.Timers.Timer alertTimer = new System.Timers.Timer();
                alertTimer.Elapsed += new ElapsedEventHandler(onAlertTimedEvent);
                alertTimer.Interval = 120000;
                alertTimer.Enabled = true;
            }

            while (true)
            {
                foreach (var file in files)
                {
                    Thread thread = new Thread(() => parseLines(readFile(file)));
                    thread.Start();
                }
            }
        }

        private void parseLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var args = line.Split(' ');

                if (args.Length < 6)
                    continue;

                var url = args[6];
                var urlArgs = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (urlArgs.Length < 3)
                    continue;

                var webSite = string.Concat(urlArgs[0], "//", urlArgs[1]);
                var section = string.Concat(webSite, "/", urlArgs[2]);

                lock (obj)
                {
                    sections.AddOrUpdate(webSite, new List<string> { section }, (site, sections) =>
                    {
                        if (sections.Contains(section))
                            return sections;

                        sections.Add(section);
                        return sections;
                    });

                    hits.AddOrUpdate(webSite, 1, (id, count) => count + 1);

                    long bytes;
                    if (args.Length > 8 && Int64.TryParse(args[9], out bytes))
                        sum += bytes;

                    nrHits++;
                }
            }
        }

        private IEnumerable<string> readFile(string file)
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var examplePath = Path.Combine(basePath, EXAMPLES_FOLDER, file);

            var inStream = new FileStream(examplePath, FileMode.Open,
                          FileAccess.Read, FileShare.ReadWrite);

            var lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(inStream, Encoding.UTF8))
            {
                var line = string.Empty;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                        yield return line;
                }
            }
        }

        private void printTopHits()
        {
            Console.WriteLine($"Logged at: {DateTime.Now}");
            Console.WriteLine($"Top Hits Last 60 Seconds:");
            foreach (var topHit in mostHits)
            {
                Console.WriteLine("WebSite:");
                Console.WriteLine($"{topHit} with {hits[topHit]} hits");

                foreach (var section in sections[topHit])
                {
                    Console.WriteLine("Sections:");
                    Console.WriteLine($"{section}");
                }
            }
        }

        private void printAlert()
        {
            Console.WriteLine($"High traffic generated an alert - hits = {nrHits}, triggered at {DateTime.Now}");
            Console.WriteLine($"Threshold = {_threshold}, Average = {average}B");
        }

        private void getTopHits()
        {
            lock (obj)
            {
                var maxValue = hits.Aggregate((h1, h2) => h1.Value > h2.Value ? h1 : h2).Value;
                var keys = hits.Where(pair => pair.Value == maxValue)
                                    .Select(pair => pair.Key);

                mostHits.AddRange(keys);

                printTopHits();
            }
        }
    }
}
