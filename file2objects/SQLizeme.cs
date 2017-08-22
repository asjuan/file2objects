namespace file2objects
{
    public class SQLizeme
    {
        public static SqlInsertsCommand From(object[] instances)
        {
            var writer = new SqlStringWriter(instances);
            return new SqlInsertsCommand(writer);
        } 
    }
}
