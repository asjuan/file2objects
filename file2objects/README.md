# file2objects
Intended for .Net programmers. Read a file and map to objects in one go.

This library can read txt files and produce C# objects. It can also persist txt files from arrays of objects.

Reading from a txt file
=======================

Suppose OrderDetails files has following information

|ID|DESC  |ORDERID|QTTY|PRIORITY|PRICE|
|--|------|-------|----|--------|-----|
| 1|Cake  |   1020|   2|TRUE    |  5.0|
| 2|Coffee|   1020|   1|FALSE   |  1.0|
| 3|Bagel |   1021|   3|FALSE   |  1.5|
| 4|Pizza |   1099|   1|TRUE    |  2.5|
| 5|Soda  |   1103|   1|FALSE   | 0.52|

and OrderDetail class has following structure

```
    public class OrderDetail
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsPriority { get; set; }
        public double? Price { get; set; }
    }
```

Creating a list of OrderDetails can be written as follows

```
var orderDetails = PlainTextReader
                     .From(@"..\..\Resources\OrderDetails.txt")
                     .DelimitBy(ColumnDelimiter.Tab)
                     .GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });

```

Writing to a txt file
=====================

Now suppose, order details is comming from a webservice or other system.

To persist a sample of 5 elements and save it as a file, write the following:

```
    var sampledetails = source.Take(5).ToArray();
    PlainTextWriter
                .From(sampledetails)
                .DelimitBy(ColumnDelimiter.Tab)
                .SaveAs<OrderDetail>(path);
```

Create an insert statement out of a Collection
==============================================

SQLizeme static object is to convert a collection of object into a SQL statement to insert 
all those objects into a Database

```
_details = new OrderDetail[] {
    new OrderDetail {
        Id = 1,
        Description = null,
        OrderId= 0,
        Quantity = 2,
        Price = 3
    } };
var sql = SQLizeme.From(_details).ToStatement<OrderDetail>("MyTable");
// The instruction above will create following statement: 
// Insert into MyTable([Id],[Description],[OrderId],[Quantity],[IsPriority],[Price]) values (1,NULL,0,2,False,3)
```