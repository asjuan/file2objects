namespace file2objects
{
    class DoubleResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Double")) return this; 
            return new Int64Resolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return double.Parse(value);
        }
    }
}