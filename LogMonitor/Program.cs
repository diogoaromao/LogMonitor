using System.IO;
using System.Linq;

namespace LogMonitor
{
    public class Program
    {
        private const string EXAMPLES_FOLDER = "examples";

        public static void Main(string[] args)
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var examplesPath = Path.Combine(basePath, EXAMPLES_FOLDER);
            var files = Directory
                .GetFiles(examplesPath)
                 .Select(f => Path.GetFileName(f))
                 .Where(f => args.Contains(f));

            var logParser = new LogParser();
            logParser.Parse(files);
        }
    }
}
