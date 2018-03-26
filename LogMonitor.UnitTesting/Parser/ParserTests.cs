using LogMonitor.Domain.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LogMonitor.UnitTesting.Parser
{
    [TestClass]
    public class ParserTests : ParserBaseTests
    {
        public ParserTests() : base() { }

        [TestMethod]
        public void LogParser_ParseSingleLine_HasContent()
        {
            // Arrange
            SetupTest("logParserTest1.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.IsTrue(lines.Any());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_SingleResult()
        {
            // Arrange
            SetupTest("logParserTest1.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.AreEqual(1, lines.Count());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_Website()
        {
            // Arrange
            SetupTest("logParserTest1.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.AreEqual("http://www.google.com", lines.First().Website);
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_LessThanSixArguments()
        {
            // Arrange
            SetupTest("logParserTest2.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.AreEqual(default(LineDTO), lines.First());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_LessThanTenArguments()
        {
            // Arrange
            SetupTest("logParserTest2.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.AreEqual(default(LineDTO), lines.First());
        }

        [TestMethod]
        public void LogParser_ParseSingleLine_DashedSize()
        {
            // Arrange
            SetupTest("logParserTest3.txt");

            // Act
            var lines = _parser.ParseContent(_testPath);

            // Assert
            Assert.AreEqual(0, lines.First().Size);
        }
    }
}
