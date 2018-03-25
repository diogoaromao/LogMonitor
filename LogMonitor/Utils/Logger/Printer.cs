using System;
using System.IO;

namespace LogMonitor.Utils.Logger
{
    public class Printer
    {
        private string _filePath;

        private static Printer _instance;

        private Printer() { }

        public static Printer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Printer();

                return _instance;
            }
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        public void Print(string log)
        {
            if (string.IsNullOrEmpty(_filePath))
                throw new Exception(Constants.RESULT_FILE_NOT_SET);

            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine(log);
            }

            Console.WriteLine(log);
        }
    }
}
