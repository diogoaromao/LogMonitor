namespace LogMonitor.Utils
{
    public static class Constants
    {
        #region Formats
        public static string DATETIME_FORMAT => "dd/MMM/yyyy:HH:mm:sszzzz";
        public static string DATETIME_GENERATION_FORMAT => "dd/MMM/yyyy:HH:mm:ss zzzz";
        public static string DATETIME_LOG_FORMAT => "HH:mm:ss";
        #endregion

        #region Paths
        public static string EXAMPLES_FOLDER => "examples";
        public static string RESULTS_FOLDER => "results";
        public static string TESTS_FOLDER => "tests";
        public static string GENERATED_FILE => "generated.txt";
        #endregion

        #region Validation
        public static string INVALID_ARGUMENTS => "Invalid arguments.";
        public static string INVALID_NUMBER_OF_ARGUMENTS => "Invalid number of arguments.";
        public static string INVALID_THRESHOLD => "Threshold value is not a valid number.";
        public static string INVALID_FILENAME => "Please specify at least one file name.";
        public static string THRESHOLD_POSITIVE_VALUE => "Please make sure that you've specified a value of at least 0 for the threshold.";
        public static string ARGUMENTS_EXPECTED => "-g or -f arguments expected.";
        public static string FILE_DOES_NOT_EXIST => "File or Directory not found: {0}.";
        #endregion

        #region Errors
        public static string RESULT_FILE_NOT_SET => "Result log file is not set.";
        #endregion


        #region Logs
        public static string LOG_MONITORING_STARTED => "[{0}]: Log Monitoring Started.";
        public static string WEBSITES_VISITED => "[{0}]: Websites visited since monitoring started:";
        public static string WEBSITES_VISITED_COUNT => "{0} - {1} time(s).";
        public static string HOSTS_VISITS => "[{0}]: Host who generated the most traffic:";
        public static string HOSTS_TRAFFIC => "{0} - {1} hits.";
        public static string ALERT_TRIGGERED => "[{0}]: High traffic generated an alert - hits = {1}, triggered at {2}.";
        public static string ALERT_AVERAGE => "[{0}]: Threshold = {1}, Average = {2} bytes.";
        public static string RECENT_WEBSITES_VISITED => "[{0}]: Most visited websites since last check:";
        public static string WEBSITE => "Website: {0}";
        public static string SECTIONS => "Sections:";
        public static string RECOVERED => "[{0}]: Recovered from alert triggered at {1}.";
        #endregion
    }
}
