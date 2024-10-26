using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleWinTasks.AppLogic;

public static class ConsoleWindowBouncer
{
    public static nint consoleWindow = GetConsoleWindow();
    public static int dx = 1, dy = 1; // Скорость по x и y
    private static int _delay = 1;
    public static int Delay
    {
        get => _delay;
        set {
            if(value <= 0)
                _delay = 1;
            else
                _delay = value;
        }
    }
    
    private static int screenWidth = GetSystemMetrics(SM_CXSCREEN);
    private static int screenHeight = GetSystemMetrics(SM_CYSCREEN);
    private static Thread bounceThread;
    private static bool isBouncing = false;
    public static event Action<int, int> OnPositionChanged;

    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern nint GetConsoleWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetSystemMetrics(int nIndex);

    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;

    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static void StartBouncing()
    {
        if (isBouncing) return;
        isBouncing = true;
        bounceThread = new Thread(Bounce);
        bounceThread.Start();
    }

    public static void StopBouncing()
    {
        isBouncing = false;
        bounceThread?.Join();
    }

    private static void Bounce()
    {
        while (isBouncing)
        {
            if (!GetWindowRect(consoleWindow, out RECT consoleRect))
                break;

            int consoleWidth = consoleRect.Right - consoleRect.Left;
            int consoleHeight = consoleRect.Bottom - consoleRect.Top;

            int newLeft = consoleRect.Left + dx;
            int newTop = consoleRect.Top + dy;

            OnPositionChanged?.Invoke(newLeft, newTop);

            // Проверка границ экрана и смена направления
            if (newLeft < 0 || newLeft + consoleWidth > screenWidth)
                dx = -dx;
            if (newTop < 0 || newTop + consoleHeight > screenHeight)
                dy = -dy;

            MoveWindow(consoleWindow, newLeft, newTop, consoleWidth, consoleHeight, true);
            Thread.Sleep(Delay); // Задержка для скорости движения окна
        }
    }
}
