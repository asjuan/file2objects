namespace file2objects
{
    public class WriteConfigurator
    {
        private FileWriter _writer;

        public WriteConfigurator(FileWriter writer)
        {
            _writer = writer;
        }

        public WriteCommand DelimitBy(char delimiter)
        {
            return new WriteCommand(_writer, delimiter);
        }

        public WriteCommand DelimitBy(ColumnDelimiter delimiter)
        {
            return DelimitBy(Utils.GetChar(delimiter));
        }
    }
}