using ORM.Nine.Database.Models.External.Input;
using ORM.Nine.Database.Models.Internal.Configurations;
using System.Text;

namespace ORM.Nine.Database.Configurations.Procedures.Internal
{
    public class ProcedureSelectTable
    {
        /// <summary>
        /// Method for create query in select table for database internal
        /// </summary>
        /// <returns></returns>
        public string Default(ProcedureSelectTableInput input)
        {
            var sql = new StringBuilder();

            sql.Append(@$"
			USE {ConnectionStrings.NameDatabase};
            DECLARE 	@TABLE			VARCHAR(150) = '{input.Table}';
			DECLARE 	@LENGTHPAGE		INT = {input.LengthPage};
			DECLARE 	@NUMBERPAGE		INT = {input.NumberPage};
			DECLARE 	@SEARCH			VARCHAR(50) = '{input.Search}';
			DECLARE 	@SORT			VARCHAR(50) = '{input.Sort}';
			DECLARE 	@CONDITIONS		VARCHAR(5000) = '{input.Conditions}';

			BEGIN TRANSACTION;
			BEGIN TRY

				DECLARE @COUNTSQL VARCHAR(MAX) = '_id'
				DECLARE @SQL VARCHAR(MAX);

				IF @SORT = ''
				BEGIN
					SET @SORT = @COUNTSQL;
				END
	
				SET @SQL = '
				-- datos with table
				;WITH _table AS(
					SELECT * FROM [' + @TABLE + '] WHERE 1=1 ' + @CONDITIONS + '
				)
				SELECT *
				FROM _table
				ORDER BY ' + @SORT + '
				OFFSET (' + CONVERT(VARCHAR, @NUMBERPAGE) + '- 1) * ' + CONVERT(VARCHAR,@LENGTHPAGE) + ' ROWS
				FETCH NEXT ' + CONVERT(VARCHAR, @LENGTHPAGE) + ' ROWS ONLY;

				-- datos with pagination

				;WITH _tablePagination AS(
					SELECT * FROM [' + @TABLE + '] WHERE 1=1 ' + @CONDITIONS + '
				), 
				_tablePaginationGroup AS(
				SELECT 
				(SELECT CASE WHEN (' + CONVERT(VARCHAR, @NUMBERPAGE) + ' - 1) = 0 THEN 1 ELSE ' + CONVERT(VARCHAR, @NUMBERPAGE) + ' - 1 END) previousPage, 
				(SELECT CASE WHEN ' + CONVERT(VARCHAR, @NUMBERPAGE) + ' < CONVERT(INT, CEILING(CONVERT(DECIMAL, (SELECT COUNT(' + @COUNTSQL + ') FROM _tablePagination)) / CONVERT(DECIMAL, ' + CONVERT(VARCHAR, @LENGTHPAGE) + '))) THEN ' + CONVERT(VARCHAR, @NUMBERPAGE) + ' + 1 ELSE ' + CONVERT(VARCHAR, @NUMBERPAGE) + ' END) laterPage, 
				CONVERT(INT, ' + CONVERT(VARCHAR, @NUMBERPAGE) + ') currentPage,
				CONVERT(INT, CEILING(CONVERT(DECIMAL, (SELECT COUNT(' + @COUNTSQL + ') FROM _tablePagination)) / CONVERT(DECIMAL, ' + CONVERT(VARCHAR, @LENGTHPAGE) + '))) lastPage
				FROM _tablePagination
				ORDER BY ' + @SORT + '
				OFFSET (' + CONVERT(VARCHAR, @NUMBERPAGE) + '- 1) * ' + CONVERT(VARCHAR,@LENGTHPAGE) + ' ROWS
				FETCH NEXT ' + CONVERT(VARCHAR, @LENGTHPAGE) + ' ROWS ONLY)
				SELECT * FROM _tablePaginationGroup GROUP BY previousPage, laterPage, currentPage, lastPage
				'

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