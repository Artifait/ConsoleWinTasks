using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWinTasks.AppLogic;

public class WinApiManager
{
    public enum TypeWindow
    {
        MB_ABORTRETRYIGNORE = (int)0x00000002L, MB_CANCELTRYCONTINUE = (int)0x00000006L,
        MB_HELP = (int)0x00004000L, MB_OK = (int)0x00000000L,
        MB_OKCANCEL = (int)0x00000001L, MB_RETRYCANCEL = (int)0x00000005L,
        MB_YESNO = (int)0x00000004L, MB_YESNOCANCEL = (int)0x00000003L
    }
    public enum TypeAnswer
    {
        CANCEL = 2,
        YES = 6,
        NO = 7,
    }


    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

}
