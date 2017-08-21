namespace file2objects
{
    public class SqlInsertsWriter
    {
        public static SqlInsertsCommand From(object[] instances)
        {
            var writer = new SqlFileWriter(instances);
            return new SqlInsertsCommand(writer);
        } 
    }
}
