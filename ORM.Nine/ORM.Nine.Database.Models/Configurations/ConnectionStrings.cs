namespace ORM.Nine.Database.Models.Configurations
{
    public class ConnectionStrings
    {
        public static string NameDatabase { set; get; } = "[DATABASE]";

        public static string Database { get; set; } = $"Data Source=[SERVER];Initial Catalog=Master;User ID=[USER_ID];Password=[PASSWORD];";
    }
}