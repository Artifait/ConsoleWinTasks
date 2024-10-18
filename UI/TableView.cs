using System.Data;
using System.IO;

namespace ConsoleWinApp.UI
{
    /// <summary>TableView with output stream support</summary>
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
            DataTable dataTable = new();

            var firstItem = query.FirstOrDefault();
            if (firstItem != null)
            {
                foreach (var prop in firstItem.GetType().GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }

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

        public static void DisplayTable<T>(IQueryable<T> query, TextWriter output = null)
        {
            DisplayTable(ConvertToDataTable(query), output);
        }

        public static void DisplayTable<T>(ICollection<T> query, TextWriter output = null)
        {
            DisplayTable(ConvertToDataTable(query), output);
        }

        public static void DisplayTable(DataTable table, TextWriter output = null)
        {
            output ??= Console.Out;

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
                    output.Write("+");
                    output.Write(new string('-', maxLengths[i] + 2));
                }
                output.WriteLine("+");
            }

            PrintLine();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                output.Write("| ");
                output.Write(table.Columns[i].ColumnName.PadRight(maxLengths[i]));
                output.Write(" ");
            }
            output.WriteLine("|");

            PrintLine();

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    output.Write("| ");
                    output.Write(row[i].ToString().PadRight(maxLengths[i]));
                    output.Write(" ");
                }
                output.WriteLine("|");
                PrintLine();
            }
        }
    }
}
