
using System;
using System.Collections.Generic;

namespace file2objects
{
    public class PlainTextReader
    {
        public static ReadConfigurator From(string source)
        {
            var retriever = new BaseRetriever();
            return retriever.From(source);
        }

        public static ReadConfigurator Split(string single, string rowDelimiter)
        {
            var lines = single.Split(new[] { rowDelimiter }, StringSplitOptions.None);
            return new ReadConfigurator(lines);
        }
    }
}
