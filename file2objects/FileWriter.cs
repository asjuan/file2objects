using System;
using System.IO;
using System.Linq;

namespace file2objects
{
    public class FileWriter
    {
        private object[] _instances;
        private char _delimiter;
        private Action<string> _writeCol;
        private Action<string> _endCol;

        public FileWriter(object[] instances)
        {
            _instances = instances;
        }

        public void ToFile<T>(string path, char delimiter)
        {
            if (_instances == null || string.IsNullOrEmpty(path)) return;
            if (_instances.Length == 0) return;
            _delimiter = delimiter;
            using (var writer = new StreamWriter(path))
            {
                _writeCol = writer.Write;
                _endCol = writer.WriteLine;
                var properties = typeof(T).GetProperties();
                WriteLine(properties.Select(p => p.Name).ToArray());
                foreach (var instance in _instances)
                {
                    var train = properties.Select(p => p.GetValue(instance).ToString()).ToArray();
                    WriteLine(train);
                }
            }
        }

        private void WriteLine(string[] columns)
        {
            int counter;
            var size = columns.Length;
            for (counter = 0; counter < size - 1; counter += 1)
            {
                _writeCol(columns[counter]);
                _writeCol(_delimiter.ToString());
            }
            _endCol(columns[counter]);
        }
    }
}
