namespace ORM.Nine.Database.Models.External.Input
{
    public class InputApiParametersDelete
    {
        public string Table { set; get; }

        public string IdPrimaryKey { set; get; }

        public List<string>? IdsPrimaryKeys { set; get; }
    }
}