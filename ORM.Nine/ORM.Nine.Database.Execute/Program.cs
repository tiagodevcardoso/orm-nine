using ORM.Nine.Database.Configurations;
using ORM.Nine.Database.Execute.Structure;
using ORM.Nine.Database.Execute.Structure.Scripts;
using ORM.Nine.Database.Models.Configurations;
using ORM.Nine.Database.Models.Table;

namespace ORM.Nine.Database.Execute
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IContextServices _contextServices = new ContextServices();
        StartingProgram:

            Console.WriteLine("NUV: Starting System...");
            Console.WriteLine("");
            Console.WriteLine("1 - Create database and tables, triggers, procedures, jobs");
            Console.WriteLine("2 - Update database");
            Console.WriteLine("3 - Execute query");
            Console.WriteLine("");
            Console.Write("Write your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");
            switch (choice)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("Initializing installation...");

                    // Database
                    var database = Create.Database(ConnectionStrings.NameDatabase);
                    _contextServices.Insert(database);
                    Console.WriteLine($"Created Database: {ConnectionStrings.NameDatabase}");

                    // Tables
                    var tables = Get.Tables();
                    Console.WriteLine($"Initializing create TABLES...");
                    foreach (var table in tables)
                    {
                        Console.WriteLine($"Initializing {table.Key}...");
                        _contextServices.Insert($"use {ConnectionStrings.NameDatabase};" + table.Value);
                        Console.WriteLine(table.Key + " - OK");
                        Console.WriteLine("");
                    }

                    // Constraints
                    var contraints = Get.TablesContraints();
                    Console.WriteLine($"Initializing create CONSTRAINTS...");
                    foreach (var constraint in contraints)
                    {
                        Console.WriteLine($"Initializing CONSTRAINTS: {constraint.Key}...");
                        _contextServices.Insert($"use {ConnectionStrings.NameDatabase};" + constraint.Value);
                        Console.WriteLine(constraint.Key + " - OK");
                        Console.WriteLine("");
                    }
                    break;
                case 2:

                    break;
                case 3:
                    Console.Write("QUERY: ");
                    string? query = Console.ReadLine();

                    Console.WriteLine("");
                    Console.WriteLine("-->");
                    Console.WriteLine("");

                    var dt = _contextServices.SelectDataTable(query);
                    Print.Datatable(dt);

                    Console.WriteLine("");
                    Console.WriteLine("<--");
                    Console.WriteLine("");
                    break;
                default:
                    break;
            }

            Console.Write("Return (Y/N): ");
            string returnSystem = Console.ReadLine();

            if (returnSystem.ToUpper().Contains("Y"))
            {
                Console.Clear();
                goto StartingProgram;
            }
            else
            {
                Environment.Exit(-1);
            }
        }
    }
}