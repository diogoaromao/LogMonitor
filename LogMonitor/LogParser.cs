using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogMonitor
{
    public class LogParser
    {
        private const string EXAMPLES_FOLDER = "examples";

        private ConcurrentDictionary<string, int> hits = new ConcurrentDictionary<string, int>();

        public void Parse(IEnumerable<string> files)
        {
            var tasks = new List<Task>();

            while (true)
            {
                foreach (var file in files)
                {
                    tasks.Add(Task.Factory.StartNew(() => parseLines(readFile(file))));
                }

                Thread.Sleep(10000);

                printTopHits(getTopHits());
                hits.Clear();
            }
        }

        private void parseLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                hits.AddOrUpdate(line, 1, (id, count) => count + 1);
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

        private void printTopHits(IEnumerable<string> topHits)
        {
            Console.WriteLine($"Top Hits Last 10 Seconds:");
            foreach (var topHit in topHits)
                Console.WriteLine($"{topHit}");
        }

        private IEnumerable<string> getTopHits()
        {
            var maxValue = hits.Aggregate((h1, h2) => h1.Value > h2.Value ? h1 : h2).Value;
            return hits.Where(pair => pair.Value == maxValue)
                                .Select(pair => pair.Key);
        }
    }
}
