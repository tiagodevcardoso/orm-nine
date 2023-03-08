namespace ORM.Nine.Database.Models.Output
{
    public class JsonReturn
    {
        public string? Table { set; get; }

        public int Quantity { set; get; }

        public JsonReturnPagination? Pagination { set; get; }

        public JsonReturnError? Error { set; get; }

        public List<JsonReturnRows>? Rows { set; get; }
    }
}