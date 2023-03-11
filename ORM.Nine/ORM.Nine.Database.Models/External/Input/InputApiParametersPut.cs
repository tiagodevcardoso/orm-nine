namespace ORM.Nine.Database.Models.External.Input
{
    public class InputApiParametersPut
    {
        public string Table { set; get; }

        public string IdPrimaryKey { set; get; }

        public List<InputApiParametersConditions>? Properties { set; get; }
    }
}
