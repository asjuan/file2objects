using System;
using System.Reflection;

namespace file2objects
{
    public class MainMapper<T> where T : new()
    {
        public T PickInstance(string[] values, Func<string[], string, int, string> sorter)
        {
            var instance = new T();
            var properties = typeof(T).GetProperties();
            for (var counter = 0; counter < properties.Length; counter += 1)
            {
                var property = properties[counter];
                CheckTypeAndSetValue(values, sorter, instance, counter, value => property.SetValue(instance, value), property.PropertyType.FullName, property.Name);
            }
            if (properties.Length == 0)
            {
                CheckTypeAndSetValue(values, sorter, instance, 0, value => instance = (T)value, typeof(T).GetTypeInfo().Name);
            }
            return instance;
        }

        private static void CheckTypeAndSetValue(string[] values, Func<string[], string, int, string> sorter, T instance, int counter, Action<object> setHandler,string namedType ,string propertyName = null)
        {
            var parser = new ParseResolver();
            var resolver = parser.GetResolver(namedType);
            if (resolver.GetType() != typeof(NullResolver))
            {
                var value = sorter(values, propertyName, counter);
                setHandler(resolver.Parse(value));
            }
        }
    }
}
