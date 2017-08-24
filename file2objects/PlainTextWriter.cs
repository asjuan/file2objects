using System.Collections.Generic;

namespace file2objects
{
    public class PlainTextWriter
    {
        public static WriteConfigurator From(IEnumerable<object> instances)
        {
            var writer = new FileWriter(instances);
            return new WriteConfigurator(writer);
        } 
    }
}
