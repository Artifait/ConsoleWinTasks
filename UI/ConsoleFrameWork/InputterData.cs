using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWinTasks.UI.ConsoleFrameWork
{
    /// <summary>
    /// InputterData
    /// </summary>
    public class IND
    {
        public static DateOnly InputDateTime(string TitleDate)
        {
            Console.WriteLine("Введите Дату " + TitleDate + ":\n");
            Console.Write("   ");
            int day = int.Parse(InputProperty("День"));
            Console.Write("   ");
            int month = int.Parse(InputProperty("Месяц"));
            Console.Write("   ");
            return new DateOnly(int.Parse(InputProperty("Год")), month, day);
        }
        public static string InputProperty(string property)
        {
            Console.Write("Введите " + property + ": ");
            Console.CursorVisible = true;
            string str = Console.ReadLine();
            Console.CursorVisible = Application.CursorVisible;
            return str;
        }
    }
}
