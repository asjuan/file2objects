using System;

namespace file2objects
{
    class GuidResolver : IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Guid")) return this;
            return new NullResolver().GetResolver(name);
        }

        public object Parse(string value)
        {
            Guid result;
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            if (Guid.TryParse(value, out result)) return result;
            throw new InvalidCastException("Cannot resolve type, check delimiter"); //todo: log position
        }
    }
}
