using file2objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using tests.DTO;

namespace tests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void ShouldGet5Lines()
        {
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            Assert.AreEqual(6, lines.ToList().Count);
        }

        [TestMethod]
        public void ShouldGetFirstItem()
        {
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            var splited = reader.GetLinesSplitedBy(lines, '\t');
            var details = reader.GetInstances<OrderDetail>(splited, new file2objects.MainMapper<OrderDetail>()).ToList();
            Assert.AreEqual(details.FirstOrDefault().Description, "Cake");
        }
        [TestMethod]
        public void ShouldGet5Entries()
        {
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            var splited = reader.GetLinesSplitedBy(lines, '\t');
            var details = reader.GetInstances<OrderDetail>(splited, new MainMapper<OrderDetail>()).ToList();
            Assert.AreEqual(5, details.ToList().Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticImprovements()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\OrderDetails.txt").DelimitBy('\t').GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByTab()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\OrderDetails.txt").DelimitBy(ColumnDelimiter.Tab).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByComma()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByComma.txt").DelimitBy(ColumnDelimiter.Comma).GetAListOf<OrderDetail>();
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByPipe()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>();
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByWhitespace()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByWhitespace.txt").DelimitBy(ColumnDelimiter.WhiteSpace).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.ReadAllFile });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesFirstRowContainsFieldNames()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\UnsortedByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5ByUsingAdvancedMapper()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByComma.txt")
                .DelimitBy(ColumnDelimiter.Comma)
                .GetAListOf<OrderDetail>(
                new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders,
                    MapPositions = new string[6] { "Id", "Description", "OrderId", "Quantity", "IsPriority", "Price" }
                });
            Assert.AreEqual(5, orderDetails.Count);
        }

        [TestMethod]
        public void ShouldGet2Tokens()
        {
            var tokens = PlainTextRetriever.From(@"..\..\Resources\TokeAndIdTuples.txt")
                .DelimitBy(ColumnDelimiter.Pipe)
                .GetAListOf<Guid>(
                new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders,
                    MapPositions = new string[1] { "TOKEN"}
                }
                );
            Assert.AreEqual(2, tokens.Count);
        }

        [TestMethod]
        public void ShouldGetActivityLogs()
        {
            var tokens = PlainTextRetriever.From(@"..\..\Resources\TokeAndIdTuples.txt")
                .DelimitBy(ColumnDelimiter.Pipe)
                .GetAListOf<ActivityLog>();
            Assert.AreEqual(2, tokens.Count);
        }

        [TestMethod]
        public void ShouldDetectError()
        {
            var tokens = PlainTextRetriever.From(@"..\..\Resources\InvalidLog.txt")
                .DelimitBy(ColumnDelimiter.Pipe)
                .GetAListOf<ActivityLog>();
            Assert.AreEqual(3, tokens.Count);
        }
    }
}
