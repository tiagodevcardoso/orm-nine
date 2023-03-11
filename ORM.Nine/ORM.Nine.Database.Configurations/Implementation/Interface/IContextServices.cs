using ORM.Nine.Database.Models.External.Input;
using ORM.Nine.Database.Models.External.Output;
using System.Data;

namespace ORM.Nine.Database.Configurations
{
    public interface IContextServices
    {
        void Insert(string SqlQuery);

        JsonReturn SelectJson(string Table, int NumberPage, string Search, string Sort, string Conditions);

        JsonReturn InsertJson(string Table, List<InputApiParametersConditions>? Properties);

        JsonReturn UpdateJson(string Table, string IdPrimaryKey, List<InputApiParametersConditions>? Properties);

        JsonReturn DeleteJson(string Table, string IdPrimaryKey, List<string> IdsPrimaryKeys = null);

        DataTable SelectDataTable(string SqlQuery);

        List<T> Select<T>(string SqlQuery);

        string MountConditions(Dictionary<string, string>? Conditions);
    }
}