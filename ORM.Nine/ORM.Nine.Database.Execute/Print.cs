using System.Data;
using System.Text;

namespace ORM.Nine.Database.Execute
{
    public class Print
    {
        public static void Datatable(DataTable table)
        {
            String GetCellValueAsString(DataRow row, DataColumn column)
            {
                var cellValue = row[column];
                var cellValueAsString = cellValue is null or DBNull ? "" : cellValue.ToString();

                return cellValueAsString;
            }

            var columnWidths = new Dictionary<DataColumn, Int32>();

            foreach (DataColumn column in table.Columns)
            {
                columnWidths.Add(column, column.ColumnName.Length);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    columnWidths[column] = Math.Max(columnWidths[column], GetCellValueAsString(row, column).Length);
                }
            }

            var resultBuilder = new StringBuilder();

            resultBuilder.Append("| ");

            foreach (DataColumn column in table.Columns)
            {
                resultBuilder.Append(column.ColumnName.PadRight(columnWidths[column]));
                resultBuilder.Append(" | ");
            }

            resultBuilder.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                resultBuilder.Append("| ");

                foreach (DataColumn column in table.Columns)
                {
                    resultBuilder.Append(GetCellValueAsString(row, column).PadRight(columnWidths[column]));
                    resultBuilder.Append(" | ");
                }

                resultBuilder.AppendLine();
            }

            Console.Write(resultBuilder.ToString());
        }
    }
}