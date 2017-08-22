using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace file2objects
{
    public class SqlStringWriter
    {
        private object[] _instances;
        public List<string> Sequence { get; internal set; }

        public SqlStringWriter(object[] instances)
        {
            _instances = instances;
            Sequence = new List<string>();
        }

        public void ToSequence<T>(string tableName)
        {
            if (_instances == null || string.IsNullOrEmpty(tableName)) return;
            if (_instances.Length == 0) return;
            var properties = typeof(T).GetProperties();
            var fields = properties.Select(p => new SqlEntryInfo { Name = p.Name, IsQuoted = CheckQuotes(p.PropertyType.Name) }).ToArray();
            var fieldNames = GetColumnNames(fields);
            PopulateFromInstances(tableName, properties, fields, fieldNames);
        }

        private void PopulateFromInstances(string tableName, PropertyInfo[] properties, SqlEntryInfo[] fields, string fieldNames)
        {
            foreach (var instance in _instances)
            {
                var columns = properties.Select(p => new SqlEntryValue { Name = p.Name, Value = p.GetValue(instance)?.ToString() });
                string values = GetParsedValues(fields, columns);
                var entry = string.Format("Insert into {0}({1}) values ({2})", tableName, fieldNames, values);
                Sequence.Add(entry);
            }
        }

        private static string GetColumnNames(SqlEntryInfo[] fields)
        {
            return fields.Aggregate(string.Empty, (curr, next) =>
            {
                var fieldName = "[" + next.Name + "]";
                if (string.IsNullOrEmpty(curr)) return fieldName;
                return curr + "," + fieldName;
            });
        }

        private static string GetParsedValues(SqlEntryInfo[] fields, IEnumerable<SqlEntryValue> columns)
        {
            var values = string.Empty;
            for (var counter = 0; counter < fields.Length; counter += 1)
            {
                var value = columns.First(o => o.Name.Equals(fields[counter].Name)).Value;
                if (value == null)
                {
                    values += "NULL";
                }
                else if (fields[counter].IsQuoted)
                {
                    values += "'" + value + "'";
                }
                else
                {
                    values += value;
                }
                if (counter < fields.Length - 1)
                {
                    values += ",";
                }
            }
            return values;
        }

        private static bool CheckQuotes(string propertyTypeName)
        {
            if (propertyTypeName.Equals("String")) return true;
            return false;
        }

        private class SqlEntryInfo
        {
            public bool IsQuoted { get; set; }
            public string Name { get; internal set; }
        }

        private class SqlEntryValue
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}