namespace file2objects
{
    class StringResolver : IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("String")) return this;
            return new Int32Resolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (value == null) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return value;
        }
    }
}
