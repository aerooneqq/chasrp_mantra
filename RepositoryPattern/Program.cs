using RepositoryPattern.Database;
using RepositoryPattern.Filters;
using System;
using System.Collections.Generic;

namespace RepositoryPattern
{
    class Program
    {
        private readonly static IRepository<Model> database = new Database.Database<Model>();

        static void Main(string[] args)
        {
            Model nick = new Model("Nick", 21);
            Model john = new Model("John", 22);
            Model hanna = new Model("Hanna", 23);
            Model emma = new Model("Emma", 21);

            database.Insert(nick);
            database.Insert(john);
            database.Insert(hanna);
            database.Insert(emma);

            Console.WriteLine("Database initial state: ");
            database.Print();

            DoSelection();

            database.Delete(new EqualityFilter<int>("Age", 21) |
                            new EqualityFilter<string>("Name", "Hanna"));

            Console.WriteLine("Database state after deletion: ");
            database.Print();

            DoSelection();


            Console.WriteLine("Print any key...");
            Console.ReadKey(true);
        }

        static void DoSelection()
        {
            Console.WriteLine("Getting records with age = 21");
            Print(database.Get(new EqualityFilter<int>("Age", 21)));

            Console.WriteLine("Getting recrods with age = 21 AND name = \"Emma\"");
            Print(database.Get(new EqualityFilter<int>("Age", 21) &
                               new EqualityFilter<string>("Name", "Emma")));

            Console.WriteLine("Getting recrods with age = 21 OR name = \"Hanna\"");
            Print(database.Get(new EqualityFilter<int>("Age", 21) |
                               new EqualityFilter<string>("Name", "Hanna")));

            Console.WriteLine("Getting all records with age IN {21, 22}");
            Print(database.Get(new InFilter<int>("Age", new List<int>(new[] { 21, 22 }))));
        }

        static void Print<T>(IEnumerable<T> records)
        {
            foreach (T obj in records)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine();
        }
    }
}
