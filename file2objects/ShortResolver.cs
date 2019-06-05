namespace file2objects
{
    internal class ShortResolver : IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("System.Int16")) return this;
            return new NullResolver().GetResolver(name);
        }

        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return short.Parse(value);
        }
    }
}