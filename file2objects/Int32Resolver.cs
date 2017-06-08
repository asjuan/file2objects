using System;
namespace file2objects
{
    class Int32Resolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Int32")) return this;
            return new BooleanResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            int result;
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            if (int.TryParse(value, out result)) return result;
            throw new InvalidCastException("Cannot resolve type, check delimiter");
        }
    }
}
