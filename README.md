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

SQLizeme object
===============

This library can also help to populate a table from a collection of objects. 

```
//detail comes with following instance
//  new OrderDetail {
//    Id = 1,
      Description = "Pear",
      OrderId= 0,
      Quantity = 4,
      Price =1
//  }
var sql = SQLizeme.From(_details).ToStatement<OrderDetail>("MyTable");
```
