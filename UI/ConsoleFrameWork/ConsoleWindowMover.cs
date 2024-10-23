
using System.Runtime.InteropServices;

namespace ConsoleWinTasks.UI.ConsoleFrameWork;

public static class ConsoleWindowMover
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetSystemMetrics(int nIndex);

    private const int SM_CXSCREEN = 0;  // Ширина экрана
    private const int SM_CYSCREEN = 1;  // Высота экрана

    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    /// <summary>
    /// Перемещает консольное окно в центр экрана.
    /// </summary>
    public static void MoveToCenter()
    {
        IntPtr consoleWindow = GetConsoleWindow();
        GetWindowRect(consoleWindow, out RECT consoleRect);

        // Вычисляем размеры консоли
        int consoleWidth = consoleRect.Right - consoleRect.Left;
        int consoleHeight = consoleRect.Bottom - consoleRect.Top;

        // Получаем размеры экрана
        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);

        // Вычисляем новую позицию консоли (центр экрана)
        int newLeft = (screenWidth - consoleWidth) / 2;
        int newTop = (screenHeight - consoleHeight) / 2;

        // Перемещаем консольное окно
        MoveWindow(consoleWindow, newLeft, newTop, consoleWidth, consoleHeight, true);
    }
}