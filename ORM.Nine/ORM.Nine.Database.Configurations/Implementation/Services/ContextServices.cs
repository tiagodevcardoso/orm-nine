using ORM.Nine.Database.Configurations.Procedures.Internal;
using ORM.Nine.Database.Models.Configurations;
using ORM.Nine.Database.Models.Input;
using ORM.Nine.Database.Models.Output;
using System.Data;
using System.Data.SqlClient;

namespace ORM.Nine.Database.Configurations
{
    /// <summary>
    /// Class used to connect to the database
    /// </summary>
    public class ContextServices : IContextServices
    {
        // Variables
        private static string? conn { set; get; }

        /// <summary>
        /// Contructor
        /// </summary>
        public ContextServices()
        {
            conn = ConnectionStrings.Database;
        }

        /// <summary>
        /// Method for insert record
        /// </summary>
        /// <param name="SqlQuery"></param>
        public void Insert(string SqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand select = new SqlCommand(SqlQuery, connection);
                select.CommandTimeout = 0;
                SqlDataReader reader = select.ExecuteReader();
            }
        }

        /// <summary>
        /// Method to fetch data from database and convert to json format
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Table"></param>
        /// <param name="NumberPage"></param>
        /// <param name="Search"></param>
        /// <param name="Sort"></param>
        /// <returns></returns>
        public JsonReturn SelectJson(string Table, int NumberPage, string Search, string Sort, string Conditions)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                var list = new JsonReturn();

                try
                {
                    var cond = new ProcedureSelectTableInput { Table = Table, LengthPage = 10, NumberPage = NumberPage, Search = Search, Sort = Sort, Conditions = Conditions };
                    var query = new ProcedureSelectTable().Default(cond);
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<DataTable> dtList = new();

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        dtList.Add(ds.Tables[i]);
                    }

                    DataTable dt = dtList[0];

                    list.Table = Table;
                    list.Quantity = dt.Rows.Count;
                    list.Rows = new List<JsonReturnRows>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var line = new JsonReturnRows();
                        line.Item = new List<JsonReturnRowsDetails>();

                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ColumnName.Equals("error"))
                            {
                                list.Error = new JsonReturnError() { Type = (string)dt.Rows[0]["error"], Value = (string)dt.Rows[0]["error_value"] };
                                list.Quantity = 0;
                                break;
                            }

                            if (!column.ColumnName.Equals("_id"))
                            {
                                var _item = new JsonReturnRowsDetails()
                                {
                                    Column = column.ColumnName,
                                    Value = !DBNull.Value.Equals(row[column.ColumnName]) ? row[column.ColumnName] : null
                                };

                                line.Item.Add(_item);
                            }
                        }

                        if (list.Error == null)
                        {
                            line.Id = (Guid)row["_id"];

                            list.Rows.Add(line);

                            if (dtList.Count > 1)
                            {
                                DataTable pagination = dtList[1];

                                list.Pagination = new JsonReturnPagination()
                                {
                                    CurrentPage = (int?)pagination.Rows[0]["currentPage"],
                                    LastPage = (int?)pagination.Rows[0]["lastPage"],
                                    LaterPage = (int?)pagination.Rows[0]["laterPage"],
                                    PreviousPage = (int?)pagination.Rows[0]["previousPage"],
                                };
                            }
                        }
                    }
                }
                catch (Exception ErrorTry)
                {
                    list.Table = Table;
                    list.Quantity = 0;
                    list.Error = new JsonReturnError() { Type = "ER - X", Value = ErrorTry.Message };
                }

                return list;
            }
        }

        /// <summary>
        /// Method to fetch data from database and convert to json format
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Table"></param>
        /// <param name="Properties"></param>
        /// <returns></returns>
        public JsonReturn InsertJson(string Table, List<InputApiParametersConditions>? Properties)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                var list = new JsonReturn();

                try
                {
                    string properties = "", values = "";

                    foreach (var item in Properties)
                    {
                        properties += item.Field + ",";
                        values += "''" + item.Value + "'',";
                    }

                    properties = properties.Remove(properties.Length - 1);
                    values = values.Remove(values.Length - 1);

                    var cond = new ProcedureInsertTableInput { Table = Table, Property = properties, Values = values };
                    var query = new ProcedureInsertDatabase().Default($"USE {ConnectionStrings.NameDatabase}; DECLARE @id UNIQUEIDENTIFIER; SET @id  = NEWID(); INSERT {cond.Table} (_id, {cond.Property}) VALUES (@id, {cond.Values}); SELECT * FROM {cond.Table} WHERE _id = @id;");
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<DataTable> dtList = new();

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        dtList.Add(ds.Tables[i]);
                    }

                    DataTable dt = dtList[0];

                    list.Table = Table;
                    list.Quantity = dt.Rows.Count;
                    list.Rows = new List<JsonReturnRows>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var line = new JsonReturnRows();
                        line.Item = new List<JsonReturnRowsDetails>();

                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ColumnName.Equals("error"))
                            {
                                list.Error = new JsonReturnError() { Type = (string)dt.Rows[0]["error"], Value = (string)dt.Rows[0]["error_value"] };
                                list.Quantity = 0;
                                break;
                            }

                            if (!column.ColumnName.Equals("_id"))
                            {
                                var _item = new JsonReturnRowsDetails()
                                {
                                    Column = column.ColumnName,
                                    Value = !DBNull.Value.Equals(row[column.ColumnName]) ? row[column.ColumnName] : null
                                };

                                line.Item.Add(_item);
                            }
                        }

                        if (list.Error == null)
                        {
                            line.Id = (Guid)row["_id"];

                            list.Rows.Add(line);

                            if (dtList.Count > 1)
                            {
                                DataTable pagination = dtList[1];

                                list.Pagination = new JsonReturnPagination()
                                {
                                    CurrentPage = (int?)pagination.Rows[0]["currentPage"],
                                    LastPage = (int?)pagination.Rows[0]["lastPage"],
                                    LaterPage = (int?)pagination.Rows[0]["laterPage"],
                                    PreviousPage = (int?)pagination.Rows[0]["previousPage"],
                                };
                            }
                        }
                    }
                }
                catch (Exception ErrorTry)
                {
                    list.Table = Table;
                    list.Quantity = 0;
                    list.Error = new JsonReturnError() { Type = "ER - X", Value = ErrorTry.Message };
                }

                return list;
            }
        }

        /// <summary>
        /// Method to fetch data from database and convert to json format
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Table"></param>
        /// <param name="Properties"></param>
        /// <returns></returns>
        public JsonReturn UpdateJson(string Table, string IdPrimaryKey, List<InputApiParametersConditions>? Properties)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                var list = new JsonReturn();

                try
                {
                    var values = "";

                    foreach (var item in Properties)
                    {
                        values += item.Field + " = ''" + item.Value + "'',";
                    }

                    values = values.Remove(values.Length - 1);

                    var cond = new ProcedureInsertTableInput { Table = Table, Values = values };
                    var query = new ProcedureInsertDatabase().Default($"USE {ConnectionStrings.NameDatabase}; UPDATE {cond.Table} SET {cond.Values} WHERE _id = ''{IdPrimaryKey}''; SELECT * FROM {cond.Table} WHERE _id = ''{IdPrimaryKey}'';");
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<DataTable> dtList = new();

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        dtList.Add(ds.Tables[i]);
                    }

                    DataTable dt = dtList[0];

                    list.Table = Table;
                    list.Quantity = dt.Rows.Count;
                    list.Rows = new List<JsonReturnRows>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var line = new JsonReturnRows();
                        line.Item = new List<JsonReturnRowsDetails>();

                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ColumnName.Equals("error"))
                            {
                                list.Error = new JsonReturnError() { Type = (string)dt.Rows[0]["error"], Value = (string)dt.Rows[0]["error_value"] };
                                list.Quantity = 0;
                                break;
                            }

                            if (!column.ColumnName.Equals("_id"))
                            {
                                var _item = new JsonReturnRowsDetails()
                                {
                                    Column = column.ColumnName,
                                    Value = !DBNull.Value.Equals(row[column.ColumnName]) ? row[column.ColumnName] : null
                                };

                                line.Item.Add(_item);
                            }
                        }

                        if (list.Error == null)
                        {
                            line.Id = (Guid)row["_id"];

                            list.Rows.Add(line);

                            if (dtList.Count > 1)
                            {
                                DataTable pagination = dtList[1];

                                list.Pagination = new JsonReturnPagination()
                                {
                                    CurrentPage = (int?)pagination.Rows[0]["currentPage"],
                                    LastPage = (int?)pagination.Rows[0]["lastPage"],
                                    LaterPage = (int?)pagination.Rows[0]["laterPage"],
                                    PreviousPage = (int?)pagination.Rows[0]["previousPage"],
                                };
                            }
                        }
                    }
                }
                catch (Exception ErrorTry)
                {
                    list.Table = Table;
                    list.Quantity = 0;
                    list.Error = new JsonReturnError() { Type = "ER - X", Value = ErrorTry.Message };
                }

                return list;
            }
        }

        /// <summary>
        /// Method to fetch data from database and convert to json format
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Table"></param>
        /// <param name="Properties"></param>
        /// <returns></returns>
        public JsonReturn DeleteJson(string Table, string IdPrimaryKey, List<string> IdsPrimaryKeys = null)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                var list = new JsonReturn();

                try
                {
                    string conditions = "";

                    if(IdsPrimaryKeys.Count > 0)
                    {
                        for (int i = 0; i < IdsPrimaryKeys.Count; i++)
                        {
                            conditions += $"''{IdsPrimaryKeys[i]}'',";
                        }
                    }
                    if(!String.IsNullOrEmpty(IdPrimaryKey))
                    {
                        conditions += $"''{IdPrimaryKey}'',";
                    }

                    conditions = conditions.Remove(conditions.Length - 1);

                    var query = new ProcedureInsertDatabase().Default($"USE {ConnectionStrings.NameDatabase}; DELETE FROM {Table} WHERE _id IN ({conditions}); SELECT * FROM {Table} WHERE _id IN ({conditions});");
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<DataTable> dtList = new();

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        dtList.Add(ds.Tables[i]);
                    }

                    DataTable dt = dtList[0];

                    list.Table = Table;
                    list.Quantity = dt.Rows.Count;
                    list.Rows = new List<JsonReturnRows>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var line = new JsonReturnRows();
                        line.Item = new List<JsonReturnRowsDetails>();

                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ColumnName.Equals("error"))
                            {
                                list.Error = new JsonReturnError() { Type = (string)dt.Rows[0]["error"], Value = (string)dt.Rows[0]["error_value"] };
                                list.Quantity = 0;
                                break;
                            }

                            if (!column.ColumnName.Equals("_id"))
                            {
                                var _item = new JsonReturnRowsDetails()
                                {
                                    Column = column.ColumnName,
                                    Value = !DBNull.Value.Equals(row[column.ColumnName]) ? row[column.ColumnName] : null
                                };

                                line.Item.Add(_item);
                            }
                        }

                        if (list.Error == null)
                        {
                            line.Id = (Guid)row["_id"];

                            list.Rows.Add(line);

                            if (dtList.Count > 1)
                            {
                                DataTable pagination = dtList[1];

                                list.Pagination = new JsonReturnPagination()
                                {
                                    CurrentPage = (int?)pagination.Rows[0]["currentPage"],
                                    LastPage = (int?)pagination.Rows[0]["lastPage"],
                                    LaterPage = (int?)pagination.Rows[0]["laterPage"],
                                    PreviousPage = (int?)pagination.Rows[0]["previousPage"],
                                };
                            }
                        }
                    }
                }
                catch (Exception ErrorTry)
                {
                    list.Table = Table;
                    list.Quantity = 0;
                    list.Error = new JsonReturnError() { Type = "ER - X", Value = ErrorTry.Message };
                }

                return list;
            }
        }

        /// <summary>
        /// Method to fetch data from database and convert to DataTable format
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public DataTable SelectDataTable(string SqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand select = new SqlCommand(new ProcedureInsertDatabase().Default(SqlQuery), connection);
                select.CommandTimeout = 0;
                SqlDataReader reader = select.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                return dt;
            }
        }

        /// <summary>
        /// Method created to query a table and return the collection list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public List<T> Select<T>(string SqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand select = new SqlCommand(SqlQuery, connection);
                select.CommandTimeout = 0;
                SqlDataReader reader = select.ExecuteReader();
                var employeeList = new ReflectionPopulator<T>().CreateList(reader);

                return employeeList.Cast<T>().ToList();
            }
        }

        private class ReflectionPopulator<T>
        {
            public virtual List<T> CreateList(SqlDataReader reader)
            {
                var results = new List<T>();
                var properties = typeof(T).GetProperties();

                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        try
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }
                    results.Add(item);
                }
                return results;
            }
        }

        public string MountConditions(Dictionary<string, string>? Conditions)
        {
            string conditions = "";
            if (Conditions.Count > 0)
            {
                foreach (var item in Conditions)
                {
                    conditions = $" AND {item.Key} = ''{item.Value}'' ";
                }
            }
            return conditions;
        }
    }
}