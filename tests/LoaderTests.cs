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
            var details = reader.GetInstances<OrderDetail>(splited, new MainMapper<OrderDetail>()).ToList();
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
            var orderDetails = PlainTextReader.From(@"..\..\Resources\OrderDetails.txt").DelimitBy('\t').GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByTab()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\OrderDetails.txt").DelimitBy(ColumnDelimiter.Tab).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByComma()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\SameByComma.txt").DelimitBy(ColumnDelimiter.Comma).GetAListOf<OrderDetail>();
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByPipe()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\SameByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>();
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByWhitespace()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\SameByWhitespace.txt").DelimitBy(ColumnDelimiter.WhiteSpace).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.ReadAllFile });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5EntriesFirstRowContainsFieldNames()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\UnsortedByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties });
            Assert.AreEqual(5, orderDetails.Count);
        }
        [TestMethod]
        public void ShouldGet5ByUsingAdvancedMapper()
        {
            var orderDetails = PlainTextReader.From(@"..\..\Resources\SameByComma.txt")
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
            var tokens = PlainTextReader.From(@"..\..\Resources\TokeAndIdTuples.txt")
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
            var tokens = PlainTextReader.From(@"..\..\Resources\TokeAndIdTuples.txt")
                .DelimitBy(ColumnDelimiter.Pipe)
                .GetAListOf<ActivityLog>();
            Assert.AreEqual(2, tokens.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void ShouldDetectError()
        {
            var tokens = PlainTextReader.From(@"..\..\Resources\InvalidLog.txt")
                .DelimitBy(ColumnDelimiter.Pipe)
                .GetAListOf<ActivityLog>();
        }
        [TestMethod]
        public void ShouldMapFromText()
        {
            var single = "ID\tDESC\tORDERID\tQTTY\tPRIORITY\tPRICE\n1\tCake\t1020\t2\tTRUE\t5.0\n2\tCoffee\t1020\t1\tFALSE\t1.0\n3\tBagel\t1021\t3\tFALSE\t1.5\n4\tPizza\t1099\t1\tTRUE\t2.5\n5\tSoda\t1103\t1\tFALSE\t0.52";
            var details = PlainTextReader.Split(single, "\n")
                    .DelimitBy(ColumnDelimiter.Tab)
                    .GetAListOf<OrderDetail>();
            Assert.AreEqual(details.FirstOrDefault().Description, "Cake");
        }
    }
}
