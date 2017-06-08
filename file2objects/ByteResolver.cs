namespace file2objects
{
    class ByteResolver : IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Byte")) return this;
            return new StringResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return byte.Parse(value);
        }
    }
}
