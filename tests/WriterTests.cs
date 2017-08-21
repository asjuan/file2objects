using file2objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tests.DTO;

namespace tests
{
    [TestClass]
    public class WriterTests
    {
        private OrderDetail[] _details = new OrderDetail[1] {
            new OrderDetail
                {
                    Id = 1,
                    Description = "Apple",
                    OrderId= 0,
                    Quantity = 1,
                    Price = 1
                }
        };

        [TestMethod]
        public void ShouldWriteNothingIfEmpty()
        {
            var writer = new FileWriter(null);
            writer.ToFile<OrderDetail>(null, '\t');
        }

        [TestMethod]
        public void ShouldWriteTwoLines()
        {
            var path = @"..\..\Resources\tempDetails.txt";
            File.Delete(path);
            var writer = new FileWriter(_details);
            writer.ToFile<OrderDetail>(path, '\t');
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(path);
            Assert.AreEqual(2, lines.ToList().Count);
        }

        [TestMethod]
        public void ShouldInvokeFromWriterToCreateTwoLines()
        {
            var path = @"..\..\Resources\temp2s.txt";
            File.Delete(path);
            PlainTextWriter
                .From(_details)
                .DelimitBy(ColumnDelimiter.Tab)
                .SaveAs<OrderDetail>(path);
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(path);
            Assert.AreEqual(2, lines.ToList().Count);
        }

        [TestMethod]
        public void ShouldCreateSqlInserts()
        {
            _details = new OrderDetail[] {
                new OrderDetail {
                    Id = 1,
                    Description = "Apple",
                    OrderId= 0,
                    Quantity = 2,
                    Price = 3
                },
                new OrderDetail {
                    Id = 1,
                    Description = "Pear",
                    OrderId= 0,
                    Quantity = 4,
                    Price =1
                }
            };
            var sql = SqlInsertsWriter.From(_details).ToString<OrderDetail>("MyTable");
            Assert.IsNotNull(sql);
        }
    }
}
