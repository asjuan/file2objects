using System.Collections.Generic;

namespace file2objects
{
    public class ReadCommand
    {
        private List<string[]> _splitedItems;

        public ReadCommand(List<string[]> splitedItems)
        {
            _splitedItems = splitedItems;
        }

        public List<T> GetAListOf<T>(MapperConfiguration configuration = null) where T : new()
        {
            if (configuration == null)
            {
                configuration = new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders
                };
            }
            var reader = new FileReader();
            var mapper = new MainMapper<T>();

            return reader.GetInstances<T>(_splitedItems, configuration, mapper);
        }
    }
}