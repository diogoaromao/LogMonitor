using LogMonitor.Utils;

namespace LogMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new InputValidation(args).Validate();
        }
    }
}
