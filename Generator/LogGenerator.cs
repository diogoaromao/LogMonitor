using LogMonitor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generator
{
    public class LogGenerator
    {
        private double _threshold;
        private Random _random;
        private DateTimeOffset _lastDateTime;

        private const string ipAddress = "127.0.0.1";
        private const string userIdentifier = "-";
        private const string userId = "-";
        private const string request = "\"GET";
        private const string protocol = "HTTP/1.0\"";
        private const string statusCode = "200";
        private const int lines = 50;
        private List<string> resources = new List<string>
        {
            "http://www.quora.com/pub/peace/VRS9.html",
            "http://www.facebook.com/pub/tblake/www/intel.gif",
            "http://www.datadog.com/pub/job/vk/flowers1.gif",
            "http://www.facebook.com/pub/pribut/redblsm.gif",
            "http://www.facebook.com/pub/pribut/spsport.html",
            "http://www.quora.com/pub/robert/current.html",
            "http://www.quora.com/pub/atomicbk/logo2.gif",
            "http://www.google.com/atomicbk/images/atomgirl.jpg",
            "http://www.google.com/pub/atomicbk/catalog/logo2.gif",
            "http://www.facebook.com/pub/atomicbk/catalog/new.gif",
            "http://www.google.com/pub/atomicbk/catalog/new.gif",
            "http://www.quora.com/pub/rjgula/network.html",
            "http://www.datadog.com/pub/job/vk/view18.jpg",
            "http://www.facebook.com/pub/tblake/www/aacc.gif",
            "http://www.quora.com/pub/atomicbk/seanc.gif",
            "http://www.facebook.com/atomicbk/catalog/new.gif"
        };

        public LogGenerator(double threshold)
        {
            _threshold = threshold;
            _random = new Random((int)DateTime.Now.Ticks);
            _lastDateTime = DateTimeOffset.Now.AddMinutes(2);
        }

        public void Generate()
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var generatedPath = Path.Combine(basePath, Constants.EXAMPLES_FOLDER, Constants.GENERATED_FILE);

            if (File.Exists(generatedPath))
                File.Delete(generatedPath);

            File.WriteAllText(generatedPath, createContent());
        }

        private string createContent()
        {
            var sb = new StringBuilder();

            for(var line = 0; line < lines; line++)
            {
                sb.AppendLine(createLogEntry());
            }

            return sb.ToString();
        }

        private string createLogEntry()
            => $"{ipAddress} {userIdentifier} {userId} [{generateDateTime()}] {request} {getRandomWebsite()} {protocol} {statusCode} {generateSize()}";

        private string generateDateTime()
        {
            _lastDateTime = _lastDateTime.AddSeconds(2);

           return _lastDateTime.ToString(Constants.DATETIME_GENERATION_FORMAT);
        }

        private string getRandomWebsite()
        {
            var index = _random.Next(0, resources.Count - 1);
            return resources.ElementAt(index);
        }

        private string generateSize()
        {
            var lowerBound = (int)_threshold;
            var upperBound = lowerBound + 1000;

            var size = _random.Next(lowerBound, upperBound);
            return size.ToString();
        }
    }
}
