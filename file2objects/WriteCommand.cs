using System;

namespace file2objects
{
    public class WriteCommand
    {
        private char _delimiter;
        private FileWriter _writer;

        public WriteCommand(FileWriter writer, char delimiter)
        {
            _writer = writer;
            _delimiter = delimiter;
        }

        public void SaveAs<T>(string path)
        {
            _writer.ToFile<T>(path, _delimiter);
        }
    }
}