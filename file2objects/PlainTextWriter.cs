namespace file2objects
{
    public class PlainTextWriter
    {
        public static WriteConfigurator From(object[] instances)
        {
            var writer = new FileWriter(instances);
            return new WriteConfigurator(writer);
        } 
    }
}
