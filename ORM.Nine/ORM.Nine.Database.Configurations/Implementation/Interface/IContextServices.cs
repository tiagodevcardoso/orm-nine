using ORM.Nine.Database.Models.Output;
using System.Data;

namespace ORM.Nine.Database.Configurations
{
    public interface IContextServices
    {
        void Insert(string SqlQuery);

        JsonReturn SelectJson(string Procedure, string Table, int NumberPage, string Search, string Sort, string Conditions);

        DataTable SelectDataTable(string SqlQuery);

        List<T> Select<T>(string SqlQuery);

        string MountProcedure(string Procedure, Dictionary<string, object> Parametros);

        string MountConditions(Dictionary<string, string> Conditions);
    }
}