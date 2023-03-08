using System.Text;

namespace ORM.Nine.Database.Configurations.Procedures.Internal
{
    public class ProcedureInsertDatabase
    {
        /// <summary>
		/// Method for create query in database internal
		/// </summary>
		/// <returns></returns>
		public string Default(string Query)
        {
            var sql = new StringBuilder();

            sql.Append(@$"
            DECLARE @QUERY VARCHAR(500) = '{Query}';

			BEGIN TRANSACTION;
			BEGIN TRY

				DECLARE @COUNTSQL VARCHAR(MAX) = '_id'
				DECLARE @SQL VARCHAR(MAX);
	
				SET @SQL = '' + @QUERY + ''

				EXECUTE(@SQL);

			END TRY
			BEGIN CATCH
				SELECT 'ER - ' + CONVERT(VARCHAR, ERROR_SEVERITY()) error, ERROR_MESSAGE() error_value 
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
			END CATCH;

			IF @@TRANCOUNT > 0
				COMMIT TRANSACTION;
            ");

            return sql.ToString();
        }
    }
}