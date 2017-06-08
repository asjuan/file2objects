
namespace file2objects
{
    public class Utils
    {
        public static char GetChar(ColumnDelimiter delimiter)
        {
            var charDelimiter = '\t';
            switch (delimiter)
            {
                case ColumnDelimiter.Colon:
                    charDelimiter = ':';
                    break;
                case ColumnDelimiter.Semicolon:
                    charDelimiter = ';';
                    break;
                case ColumnDelimiter.Comma:
                    charDelimiter = ',';
                    break;
                case ColumnDelimiter.WhiteSpace:
                    charDelimiter = ' ';
                    break;
                case ColumnDelimiter.Pipe:
                    charDelimiter = '|';
                    break;
            }

            return charDelimiter;
        }
    }
}
