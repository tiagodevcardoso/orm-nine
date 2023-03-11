using ORM.Nine.Database.Models.Internal.Attributes;
using ORM.Nine.Database.Models.Internal.Table;
using System.Reflection;

namespace ORM.Nine.Database.Execute.Structure
{
    public class Get
    {
        public static Dictionary<string, string> Tables()
        {
            var List = new Dictionary<string, string>();
            Assembly SampleAssembly = Assembly.LoadFrom("ORM.Nine.Database.Models.dll");
            Type[] typelist = SampleAssembly.GetTypes().Where(x => x.Namespace.Equals("ORM.Nine.Database.Models.Tables")).ToArray();

            for (int i = 0; i < typelist.Length; i++)
            {
                Type type = typelist[i];

                var Table = new Table();
                var ValueDefault = new List<TableSetValueDefault>();
                Table.Name = type.Name;

                if (type != null)
                {
                    var list = new List<TableColumn>();

                    // Get fields in class
                    foreach (PropertyInfo field in type.GetProperties())
                    {
                        Attribute[] attrs = Attribute.GetCustomAttributes(field);

                        // Set if is primary key
                        if (((PropertyField)attrs[0]).Primary)
                        {
                            Table.PrimaryKey = ((PropertyField)attrs[0]).Name;
                        }

                        // Set value default
                        if (!string.IsNullOrEmpty(((PropertyField)attrs[0]).Default))
                        {
                            ValueDefault.Add(new TableSetValueDefault() { Column = ((PropertyField)attrs[0]).Name, Value = ((PropertyField)attrs[0]).Default });
                        }

                        list.Add(new TableColumn()
                        {
                            Name = ((PropertyField)attrs[0]).Name,
                            Type = ((PropertyField)attrs[0]).Type,
                            TypeLength = 0,
                            ValueNull = ((PropertyField)attrs[0]).ValueNull,
                            Order = ((PropertyField)attrs[0]).Order
                        });
                    }

                    Table.Columns = list.OrderBy(x => x.Order).ToList();
                }

                Table.ValueDefault = ValueDefault;
                List.Add(Table.Name, Scripts.Create.Table(Table));
            }
            return List;
        }

        public static Dictionary<string, string> TablesContraints()
        {
            var List = new Dictionary<string, string>();
            Assembly SampleAssembly = Assembly.LoadFrom("ORM.Nine.Database.Models.dll");
            Type[] typelist = SampleAssembly.GetTypes().Where(x => x.Namespace.Equals("ORM.Nine.Database.Models.Tables")).ToArray();

            for (int i = 0; i < typelist.Length; i++)
            {
                Type type = typelist[i];

                var Table = new Table();
                var ValueDefault = new List<TableSetValueDefault>();
                Table.Name = type.Name;

                if (type != null)
                {
                    var list = new List<TableColumn>();

                    // Get fields in class
                    foreach (PropertyInfo field in type.GetProperties())
                    {
                        Attribute[] attrs = Attribute.GetCustomAttributes(field);

                        // Set if is primary key
                        if (((PropertyField)attrs[0]).Primary)
                        {
                            Table.PrimaryKey = ((PropertyField)attrs[0]).Name;
                        }

                        // Set value default
                        if (!string.IsNullOrEmpty(((PropertyField)attrs[0]).Default))
                        {
                            ValueDefault.Add(new TableSetValueDefault() { Column = ((PropertyField)attrs[0]).Name, Value = ((PropertyField)attrs[0]).Default });
                        }

                        list.Add(new TableColumn()
                        {
                            Name = ((PropertyField)attrs[0]).Name,
                            Type = ((PropertyField)attrs[0]).Type,
                            TypeLength = 0,
                            ValueNull = ((PropertyField)attrs[0]).ValueNull,
                            Order = ((PropertyField)attrs[0]).Order
                        });
                    }

                    Table.Columns = list.OrderBy(x => x.Order).ToList();
                }

                Table.ValueDefault = ValueDefault;
                List.Add(Table.Name, Scripts.Create.TableContraint(Table));
            }
            return List;
        }
    }
}