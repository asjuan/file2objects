using System.Text;

namespace file2objects
{
    public class SqlInsertsCommand
    {
        private SqlFileWriter _writer;

        public SqlInsertsCommand(SqlFileWriter writer)
        {
            _writer = writer;
        }

        public string ToString<T>(string tableName)
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