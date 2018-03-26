using LogMonitor.Utils;
using LogMonitor.Utils.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace LogMonitor.UnitTesting
{
    [TestClass]
    public class ParserTests
    {
        private string _testsPath;
        private LogParser _parser;

        public ParserTests()
        {
            _testsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, Constants.TESTS_FOLDER);
            _parser = LogParser.Instance;
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_HasContent()
        {
            // Arrange
            var file = "logParserTest1.txt";

            // Act
            var lines = _parser.ParseContent(Path.Combine(_testsPath, file));

            // Assert
            Assert.IsTrue(lines.Any());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_SingleResult()
        {
            // Arrange
            var file = "logParserTest1.txt";
            
            // Act
            var lines = _parser.ParseContent(Path.Combine(_testsPath, file));

            // Assert
            Assert.AreEqual(1, lines.Count());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_Website()
        {
            // Arrange
            var file = "logParserTest1.txt";

            // Act
            var lines = _parser.ParseContent(Path.Combine(_testsPath, file));

            // Assert
            Assert.AreEqual("http://www.google.com", lines.First().Website);
        }

        
    }
}
