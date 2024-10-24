
using System.Data;

namespace ConsoleWinTasks.UI.ConsoleFrameWork;

/// <summary>TableView</summary>
public static class TV
{

    public static DataTable ConvertToDataTable<T>(ICollection<T> collection)
    {
        DataTable dataTable = new();

        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (var item in collection)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item) ?? DBNull.Value;
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    public static void DisplayTable<T>(ICollection<T> collection)
        => DisplayTable(ConvertToDataTable(collection));

    public static void DisplayTable(DataTable table)
    {
        int[] maxLengths = new int[table.Columns.Count];

        for (int i = 0; i < table.Columns.Count; i++)
        {
            maxLengths[i] = table.Columns[i].ColumnName.Length;
            foreach (DataRow row in table.Rows)
            {
                string? str = row[i]?.ToString();
                str ??= "";
                int length = str.Length;
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
                Console.Write(new string('-', maxLengths[i] + 2));
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
                string? str = row[i]?.ToString();
                str ??= "";
                Console.Write(str.PadRight(maxLengths[i]));
                Console.Write(" ");
            }
            Console.WriteLine("|");
            PrintLine();
        }
    }
}
