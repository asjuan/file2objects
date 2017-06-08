namespace file2objects
{
    class DecimalResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Decimal")) return this;
            return new DoubleResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return decimal.Parse(value);
        }
        
    }
}