using System.Data;

namespace ConsoleWinApp.UI
{
    /// <summary>TableView</summary>
    public static class TV 
    {
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
