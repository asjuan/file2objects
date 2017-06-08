using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace file2objects
{
    public class FileReader
    {
        public IEnumerable<string> GetLinesFromFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
        public List<string[]> GetLinesSplitedBy(IEnumerable<string> items, char separator)
        {
            return items.Select(o => o.Split(separator)).ToList();

        }

        public IEnumerable<T> GetInstances<T>(List<string[]> items, MainMapper<T> mapper) where T : new()
        {
            return items.Where(o => !string.IsNullOrEmpty(o[0])).Skip(1)
                .Select(p => mapper.PickInstance(p, (values, pn, pos) => values[pos]));
        }

        public List<T1> GetInstances<T1>(IEnumerable<string[]> items, MapperConfiguration configuration, MainMapper<T1> mapper) where T1 : new()
        {
            var result = items.Where(o => !string.IsNullOrEmpty(o[0]));
            if (configuration.DefaultPropertyReader == PropertyReader.SkipHeaders)
            {
                result = result.Skip(1);
            }
            else if (configuration.DefaultPropertyReader == PropertyReader.UseHeadersToInferProperties)
            {
                var header = result.First();
                return result.Skip(1).Select(o => mapper.PickInstance(o,
                    (values, pn, pos) =>
                    {
                        var index = header.ToList().FindIndex(t => t.ToUpper().Equals(pn.ToUpper()));
                        return values[index];
                    })).ToList();
            }
            if (configuration.MapPositions != null)
            {
                return result
                    .Select(o => mapper.PickInstance(o, (values, pn, pos) =>
                    {
                        if (pn != null)
                        {
                            var index = configuration.MapPositions.ToList().FindIndex(t => t.Equals(pn));
                            return values[index];
                        }
                        return values[pos];
                    }))
                    .ToList();
            }
            return result
                .Select(o => mapper.PickInstance(o, (values, pn, pos) => values[pos]))
                .ToList();
        }
    }
}
