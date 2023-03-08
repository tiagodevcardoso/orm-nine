namespace ORM.Nine.Database.Models.Configurations
{
    public class ConnectionStrings
    {
        public static string NameDatabase { set; get; } = "[NAME_DATABASE]";

        public static string Database { get; set; } = $"Data Source=[DATA_SOURCE];Initial Catalog={NameDatabase};User ID=[USER_ID];Password=[PASSWORD];";
    }
}