namespace ORM.Nine.Database.Models.Input
{
    public class InputApiParametersPut
    {
        public string Table { set; get; }

        public string IdPrimaryKey { set; get; }

        public List<InputApiParametersConditions>? Properties { set; get; }
    }
}
