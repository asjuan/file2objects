using file2objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tests.DTO;

namespace tests
{
    [TestClass]
    public class SupportShortTest
    {
        private List<ToDoItem> _orderDetails;

        [TestInitialize]
        public void Init()
        {
            _orderDetails = PlainTextReader.From(@"..\..\Resources\ToDos.txt")
                   .DelimitBy(ColumnDelimiter.Comma)
                   .GetAListOf<ToDoItem>(
                   new MapperConfiguration
                   {
                       DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties
                   });
        }

        [TestMethod]
        public void ShouldGet1RowThatIncludesShortTypeField()
        {
            Assert.AreEqual(2, _orderDetails.Count);
            Assert.IsNotNull(_orderDetails.FirstOrDefault(o => o.PriorityOrder == 1));
        }

        [TestMethod]
        public void ShouldHandleNullableShortTypeField()
        {
            Assert.IsNull(_orderDetails.First(o => o.PriorityOrder != 1).PriorityOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ShouldHandleInvalidFileType()
        {
            _orderDetails = PlainTextReader.From(@"..\..\Resources\InvalidLog.txt")
                   .DelimitBy(ColumnDelimiter.Comma)
                   .GetAListOf<ToDoItem>(
                   new MapperConfiguration
                   {
                       DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties
                   });
        }
    }
}
