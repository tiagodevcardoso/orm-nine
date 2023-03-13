namespace ORM.Nine.Database.Models.Internal.Configurations
{
    public class ConnectionStrings
    {
        public static string NameDatabase { set; get; } = "Teste";

        public static string Database { get; set; } = $"Data Source=localhost;Initial Catalog=Master;User ID=adm;Password=adm;";
    }
}