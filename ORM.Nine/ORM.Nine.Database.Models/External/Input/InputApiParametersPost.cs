namespace ORM.Nine.Database.Models.External.Input
{
    public class InputApiParametersPost
    {
        public string Table { set; get; }

        public List<InputApiParametersConditions>? Properties { set; get; }
    }
}
