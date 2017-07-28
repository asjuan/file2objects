using System.Collections.Generic;

namespace file2objects
{
    public class ReadConfigurator
    {
        private static IEnumerable<string> _lines;
        
        public ReadConfigurator(IEnumerable<string> lines)
        {
            _lines = lines;
        }

        public ReadCommand DelimitBy(char delimiter)
        {
            var reader = new FileReader();
            var splitedItems = reader.GetLinesSplitedBy(_lines, delimiter);
            return new ReadCommand(splitedItems);
        }
        public ReadCommand DelimitBy(ColumnDelimiter delimiter)
        {
            return DelimitBy(Utils.GetChar(delimiter));
        }
        
    }
}