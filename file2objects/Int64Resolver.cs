using System;
namespace file2objects
{
    class Int64Resolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Int64")) return this;
            return new GuidResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            long result;
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            if (long.TryParse(value, out result)) return result;
            throw new InvalidCastException("Cannot resolve type, check delimiter");
        }
    }
}
