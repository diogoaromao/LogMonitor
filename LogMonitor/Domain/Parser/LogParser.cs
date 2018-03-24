using LogMonitor.Domain.DTO;
using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogMonitor.Domain.Parser
{
    public class LogParser
    { 
        public LogParser(){ }

        public IEnumerable<LineDTO> ParseContent(string file)
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var examplePath = Path.Combine(basePath, Constants.EXAMPLES_FOLDER, file);

            var inStream = new FileStream(examplePath, FileMode.Open,
                          FileAccess.Read, FileShare.ReadWrite);

            var lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(inStream, Encoding.UTF8))
            {
                var line = string.Empty;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                        yield return parseLine(line);
                }
            }
        }

        public LineDTO parseLine(string line)
        {
            var args = line.Split(' ');

            if (args.Length < 6)
                return default(LineDTO);

            var url = args[6];
            var urlArgs = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlArgs.Length < 3)
                return default(LineDTO);

            var webSite = string.Concat(urlArgs[0], "//", urlArgs[1]);
            var section = string.Concat(webSite, "/", urlArgs[2]);

            double size = 0;
            if (args.Length > 8 && Double.TryParse(args[9], out double bytes))
                size = bytes;

            return new LineDTO(webSite, section, size);
        }
    }
}
