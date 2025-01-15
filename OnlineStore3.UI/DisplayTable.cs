using System;
using System.Collections.Generic;
using System.Reflection;

namespace OnlineStore3.UI
{
    internal class DisplayTable
    {
        public static void DisplayList<T>(List<T> list, string tableName)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine($"No data available in {tableName}.");
                return;
            }
            Console.WriteLine($"--- {tableName.ToUpper()} ---");
            PropertyInfo[] properties = typeof(T).GetProperties();
            Console.WriteLine(new string('-', 20 * properties.Length));
            foreach (var prop in properties)
            {
                Console.Write(prop.Name.PadRight(20));
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', 20 * properties.Length));
            foreach (var item in list)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);
                    Console.Write(value.ToString().PadRight(20));
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 20 * properties.Length));
            Console.WriteLine();
        }
    }
}
