namespace ORM.Nine.Database.Models.Input
{
    public class ProcedureSelectTableInput
    {
        public string Table { set; get; }

        public int LengthPage { set; get; }

        public int NumberPage { set; get; }

        public string Search { set; get; }

        public string Sort { set; get; }

        public string Conditions { set; get; }
    }
}