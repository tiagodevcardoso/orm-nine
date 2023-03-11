namespace ORM.Nine.Database.Models.Internal.Table
{
    public class Table
    {
        public string Name { set; get; }

        public List<TableColumn> Columns { set; get; }

        public List<TableSetValueDefault> ValueDefault { set; get; }

        public string? PrimaryKey { set; get; }
    }
}