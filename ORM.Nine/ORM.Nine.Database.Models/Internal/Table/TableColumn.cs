namespace ORM.Nine.Database.Models.Internal.Table
{
    public class TableColumn
    {
        public string? Name { set; get; }

        public string? Type { set; get; }

        public int? TypeLength { set; get; }

        public bool ValueNull { set; get; }

        public int Order { set; get; }
    }
}