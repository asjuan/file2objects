using System.Collections.Generic;
using System.Linq;

namespace file2objects
{
    public class SqlFileWriter
    {
        private object[] _instances;
        public List<string> Sequence { get; internal set; }

        public SqlFileWriter(object[] instances)
        {
            _instances = instances;
            Sequence = new List<string>();
        }

        public void ToSequence<T>(string tableName)
        {
            if (_instances == null || string.IsNullOrEmpty(tableName)) return;
            if (_instances.Length == 0) return;
            var properties = typeof(T).GetProperties();
            var fields = properties.Select(p => new { p.Name, isQuoted = CheckQuotes(p.PropertyType.Name) }).ToArray();
            var fieldNames = fields.Aggregate(string.Empty, (curr, next) =>
            {
                var fieldName = "[" + next.Name + "]";
                if (string.IsNullOrEmpty(curr)) return fieldName;
                return curr + "," + fieldName;
            });
            var size = fields.Length;
            foreach (var instance in _instances)
            {

                var columns = properties.Select(p => new { p.Name, Value = p.GetValue(instance).ToString() });
                var entry = string.Empty;
                var values = string.Empty;
                for (var counter = 0; counter < size; counter += 1)
                {
                    var value = columns.First(o => o.Name.Equals(fields[counter].Name)).Value;
                    if (fields[counter].isQuoted)
                    {
                        values += "'" + value + "'";
                    }
                    else
                    {
                        values += value;
                    }
                    if (counter < size - 1)
                    {
                        values += ",";
                    }
                }
                entry = string.Format("Insert into {0}({1}) values ({2})", tableName, fieldNames, values);
                Sequence.Add(entry);
            }
        }

        private static bool CheckQuotes(string propertyTypeName)
        {
            if (propertyTypeName.Equals("String")) return true;
            return false;
        }
    }
}