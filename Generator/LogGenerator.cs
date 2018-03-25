using LogMonitor.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Generator
{
    public class LogGenerator
    {
        private double _threshold;

        public LogGenerator(double threshold)
        {
            _threshold = threshold;
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

            sb.AppendLine($"pwpark.remote.Princeton.EDU - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.quora.com/pub/peace/VRS9.html HTTP/1.0\" 200 2187");
            Thread.Sleep(1000);
            sb.AppendLine($"debasement.clark.net - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.facebook.com/pub/tblake/www/intel.gif HTTP/1.0\" 304 -");
            Thread.Sleep(1000);
            sb.AppendLine($"ppp.a2.ulaval.ca - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.datadog.com/pub/job/vk/flowers1.gif HTTP/1.0\" 200 4288");
            Thread.Sleep(1000);
            sb.AppendLine($"shep102.wustl.edu - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.facebook.com/pub/pribut/redblsm.gif HTTP/1.0\" 200 269");
            Thread.Sleep(1000);
            sb.AppendLine($"shep102.wustl.edu - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.facebook.com/pub/pribut/spsport.html HTTP/1.0\" 200 3589");
            Thread.Sleep(1000);
            sb.AppendLine($"ppp.mia.94.shadow.net - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.quora.com/pub/robert/current.html HTTP/1.0\" 200 30337");
            Thread.Sleep(1000);
            sb.AppendLine($"crl12.crl.com - - [{DateTimeOffset.Now.AddMinutes(1).ToString(Constants.DATETIME_GENERATION_FORMAT)}] \"GET http://www.quora.com/pub/atomicbk/logo2.gif HTTP/1.0\" 200 12871");


            return sb.ToString();
        }
    }
}
