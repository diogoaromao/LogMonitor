using LogMonitor.Utils.Validation;

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
