using System.Data;

namespace ConsoleWinApp.UI
{
    /// <summary>TableView</summary>
    public static class TV 
    {
        public static DataTable ConvertToDataTable<T>(ICollection<T> collection)
        {
            DataTable dataTable = new();

            var firstItem = collection.FirstOrDefault();
            if (firstItem != null)
            {
                foreach (var prop in firstItem.GetType().GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in collection)
                {
                    var row = dataTable.NewRow();
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        public static DataTable ConvertToDataTable<T>(IQueryable<T> query)
        {
            DataTable dataTable = new DataTable();

            // Получаем тип элемента из запроса
            var firstItem = query.FirstOrDefault();
            if (firstItem != null)
            {
                // Автоматическое создание колонок на основе свойств первого элемента
                foreach (var prop in firstItem.GetType().GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }

                // Заполнение DataTable данными из запроса
                foreach (var item in query.ToList())
                {
                    var row = dataTable.NewRow();
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        public static void DisplayTable<T>(IQueryable<T> query)
        {
            DisplayTable(ConvertToDataTable(query));
        }
        public static void DisplayTable<T>(ICollection<T> query)
        {
            DisplayTable(ConvertToDataTable(query));
        }
        public static void DisplayTable(DataTable table)
        {
            int[] maxLengths = new int[table.Columns.Count];

            for (int i = 0; i < table.Columns.Count; i++)
            {
                maxLengths[i] = table.Columns[i].ColumnName.Length;
                foreach (DataRow row in table.Rows)
                {
                    int length = row[i].ToString().Length;
                    if (length > maxLengths[i])
                    {
                        maxLengths[i] = length;
                    }
                }
            }

            void PrintLine()
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Console.Write("+");
                    Console.Write(new string('-', maxLengths[i] + 2)); // +2 для пробелов между значениями
                }
                Console.WriteLine("+");
            }

            PrintLine();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write("| ");
                Console.Write(table.Columns[i].ColumnName.PadRight(maxLengths[i]));
                Console.Write(" ");
            }
            Console.WriteLine("|");

            PrintLine();

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Console.Write("| ");
                    Console.Write(row[i].ToString().PadRight(maxLengths[i]));
                    Console.Write(" ");
                }
                Console.WriteLine("|");
                PrintLine();
            }
        }
    }
}
