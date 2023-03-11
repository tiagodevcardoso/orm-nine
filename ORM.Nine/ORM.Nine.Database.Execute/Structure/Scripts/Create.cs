using ORM.Nine.Database.Models.Internal.Table;

namespace ORM.Nine.Database.Execute.Structure.Scripts
{
    public class Create
    {
        public static string Database(string Database)
        {
            return $"use master; CREATE DATABASE {Database};";
        }

        public static string Table(Table table)
        {
            string MountTable = "";

            MountTable += $"IF OBJECT_ID(N'dbo.[{table.Name.ToLower()}]', N'U') IS NULL BEGIN ";
            MountTable += $"CREATE TABLE [dbo].[{table.Name.ToLower()}](";
            foreach (TableColumn column in table.Columns)
            {
                MountTable += $"[{column.Name}] {column.Type} {((column.ValueNull) ? "NULL" : "NOT NULL")},";
            }
            MountTable = MountTable.Remove(MountTable.Length - 1);
            if (!string.IsNullOrEmpty(table.PrimaryKey))
            {
                MountTable += $", CONSTRAINT [PK_{table.Name.ToLower()}] PRIMARY KEY CLUSTERED ([{table.PrimaryKey.ToLower()}] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            }
            MountTable += "); ";
            MountTable += "END; ";

            return MountTable;
        }

        public static string TableContraint(Table table)
        {
            string MountTable = "";

            foreach (TableSetValueDefault valueDefault in table.ValueDefault)
            {
                MountTable += $" ALTER TABLE [dbo].[{table.Name.ToString().ToLower()}] ADD CONSTRAINT [DF_{table.Name.ToString().ToLower()}_{valueDefault.Column.ToString().ToLower()}] DEFAULT (({valueDefault.Value.ToString().ToLower()})) FOR [{valueDefault.Column.ToString().ToLower()}]; ";
            }

            return MountTable;
        }
    }
}