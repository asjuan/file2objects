using System;
namespace file2objects
{
    class ParseResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name) {
            return new ByteResolver().GetResolver(name);
        }

        public object Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}
