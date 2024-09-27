
using QuizTop.UI.Win.WinNotification;


#nullable enable
namespace QuizTop.UI
{
    public static class WindowsHandler
    {
        public static Dictionary<Type, IWin> WinForms = new Dictionary<Type, IWin>();

        public static T GetWindow<T>() where T : IWin, new()
        {
            if (!WinForms.ContainsKey(typeof(T)))
                WinForms[typeof(T)] = (IWin)new T();
            return (T)WinForms[typeof(T)];
        }

        public static void AddErroreWindow(string[] messages, bool isFatal = false)
        {
            List<string> stringList1 = new();
            foreach (string message in messages)
                stringList1.AddRange(message.Split('\n'));

            IWin window;
            if (isFatal)
            {
                window = GetWindow<WinErrore>();
                ((WinErrore)window).UpdateErroreMsg(stringList1.ToArray());
            }
            else
            {
                window = GetWindow<WinFatalError>();
                ((WinFatalError)window).UpdateFatalErrorMsg(stringList1.ToArray());
            }
            Application.WinStack.Push(window);
        }
        public static void AddInfoWindow(string[]? messages)
        {
            WinInfo window = GetWindow<WinInfo>();
            List<string> stringList1 = new();
            if (messages == null)
                stringList1.Add("Нет Сообщений");
            else
                foreach (string message in messages)
                    stringList1.AddRange(message.Split('\n'));

            window.UpdateInfoMsg(stringList1.ToArray());
            Application.WinStack.Push(window);
        }

        public static string PadCenter(this string str, int totalWidth)
        {
            int count1 = (totalWidth - str.Length) / 2;
            int count2 = totalWidth - str.Length - count1;
            return new string(' ', count1) + str + new string(' ', count2);
        }
    }
}
