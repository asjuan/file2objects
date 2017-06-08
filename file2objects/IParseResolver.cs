namespace file2objects
{
    interface IParseResolver
    {
        IParseResolver GetResolver(string name);

        object Parse(string value);
    }
}
