namespace ORM.Nine.Database.Models.Input
{
    public class InputApiParametersDelete
    {
        public string Table { set; get; }

        public string IdPrimaryKey { set; get; }

        public List<string>? IdsPrimaryKeys { set; get; }
    }
}