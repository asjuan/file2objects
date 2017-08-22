using System.Text;

namespace file2objects
{
    public class SqlInsertsCommand
    {
        private SqlStringWriter _writer;

        public SqlInsertsCommand(SqlStringWriter writer)
        {
            _writer = writer;
        }

        public string ToStatement<T>(string tableName)
        {
            _writer.ToSequence<T>(tableName);
            StringBuilder builder = new StringBuilder();
            foreach(var s in _writer.Sequence)
            {
                builder.AppendLine(s);
            }
            return builder.ToString();
        }
    }
}