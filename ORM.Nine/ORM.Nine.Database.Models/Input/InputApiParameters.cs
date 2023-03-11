namespace ORM.Nine.Database.Models.Input
{
    public class InputApiParameters
    {
        public string Table { set; get; }

        public int NumberPage { set; get; }

        public string Search { set; get; }

        public string Sort { set; get; }

        public List<InputApiParametersConditions>? Conditions { set; get; }
    }
}