using LogMonitor.Domain.DTO;
using LogMonitor.Utils;
using LogMonitor.Utils.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace LogMonitor.UnitTesting.Parser
{
    [TestClass]
    public class ParserBaseTests
    {
        protected string _testsPath;
        protected LogParser _parser;
        protected string _testPath;

        public ParserBaseTests()
        {
            _testsPath = Path.Combine("C:\\Projects\\LogMonitor\\LogMonitor.UnitTesting\\", Constants.TESTS_FOLDER);
            _parser = LogParser.Instance;
        }

        protected void SetupTest(string file)
        {
            // Arrange
            _testPath = Path.Combine(_testsPath, file);
        }
    }
}
