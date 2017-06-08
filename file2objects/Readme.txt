File2Objects Library

Target Framework .Net 4.6.1

This library is provided under the Microsoft Public License (Ms-PL)

This is a simple tool to retrieve data stored in plain text files.

Nowadays it doesn't the flavor, testing is a most, there are several good practices around. Sometimes you use mocks or stubs, prior to concrete implementations. 

But some times you just need to take a shortcut and retrieve something to test functionallity,
one common approach is to implement a MotherClass or a Factory so your tests
can invoke it. From my experience I know that it requires effort to maintain that additional piece of code. That's the problem this little library tries to address,
by storing sample data in a plain text file and mapping that data to a concrete class, so new instances can be retrieved just as you do with a factory.

Usage: 
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\OrderDetails.txt").DelimitBy(ColumnDelimiter.Tab).GetAListOf<OrderDetail>();
            Assert.AreEqual(orderDetails.Count, 5);

The file OrderDetails.txt contain tab delimited data.

Important: the first row contains the headers. The sequence of the columns match the properties of the OrderDetail class.
  
Known issues

It doesn´t detect dates. It only get lists out of text files. If the format is not met the library crashes.