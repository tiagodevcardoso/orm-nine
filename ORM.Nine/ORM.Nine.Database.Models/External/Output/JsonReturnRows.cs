namespace ORM.Nine.Database.Models.External.Output
{
    public class JsonReturnRows
    {
        public Guid Id { set; get; }

        public List<JsonReturnRowsDetails>? Item { set; get; }
    }
}