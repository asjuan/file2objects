namespace file2objects
{
    class BooleanResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Boolean")) return this;
            return new DecimalResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return bool.Parse(value);
        }
    }
}
