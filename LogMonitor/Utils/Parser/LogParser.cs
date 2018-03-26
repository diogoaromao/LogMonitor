using LogMonitor.Domain.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogMonitor.Utils.Parser
{
    public class LogParser
    {
        private static LogParser _instance;

        private LogParser(){ }

        public static LogParser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogParser();

                return _instance;
            }
        }

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

        private LineDTO parseLine(string line)
        {
            var args = line.Replace("[", "").Replace("]", "").Split(' ');

            if (args.Length < 10)
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

            var dateTime = new DateTimeOffset();
            DateTimeOffset.TryParseExact($"{args[3]}{args[4]}", Constants.DATETIME_FORMAT, 
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);

            return new LineDTO(webSite, section, size, dateTime);
        }
    }
}
