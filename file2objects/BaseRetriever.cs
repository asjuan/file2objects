namespace file2objects
{
    public class BaseRetriever
    {
        public ReadConfigurator From(string source)
        {
            var reader = new FileReader();
            var lines = reader.GetLinesFromFile(source);
            return new ReadConfigurator(lines);
        }
    }
}
