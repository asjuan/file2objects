
namespace file2objects
{
    public class PlainTextReader
    {
        public static ReadConfigurator From(string source)
        {
            var retriever = new BaseRetriever();
            return retriever.From(source);
        }
    }
}
