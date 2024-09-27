
#nullable enable
namespace QuizTop.UI.Win.WinNotification
{
    public class WinErrore : IWin
    {
        public WindowDisplay windowDisplay = new(MatrixFormater.GetWindowMatrixChar(new string[] { "Ошибка" }, "Errore"));

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => null;

        public Type? ProgramFieldsType => null;

        public void UpdateErroreMsg(string[] ErroreMessages)
        {
            windowDisplay.Fields.Clear();
            for (int index = 0; index < ErroreMessages.Length; ++index)
                windowDisplay.Fields[(index + 1).ToString()] = ErroreMessages[index];
            windowDisplay.UpdateCanvas();
        }

        public int SizeX => windowDisplay.MaxLeft;

        public int SizeY => windowDisplay.MaxTop;

        public void InputHandler()
        {
            Console.ReadKey();
            Console.Clear();
            Application.WinStack.Pop();
        }

        public void Show()
        {
            Console.Clear();
            windowDisplay.Show();
        }
    }

    public class WinFatalError : IWin
    {
        public WindowDisplay windowDisplay = new(MatrixFormater.GetWindowMatrixChar(new string[] { "Фатальная Ошибка" }, "FatalError"));

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => null;

        public Type? ProgramFieldsType => null;

        // Метод для обновления сообщения об ошибке
        public void UpdateFatalErrorMsg(string[] ErrorMessages)
        {
            windowDisplay.Fields.Clear();
            for (int index = 0; index < ErrorMessages.Length; ++index)
                windowDisplay.Fields[(index + 1).ToString()] = ErrorMessages[index];
            windowDisplay.UpdateCanvas();
        }

        public int SizeX => windowDisplay.MaxLeft;

        public int SizeY => windowDisplay.MaxTop;

        // Обработчик ввода, завершает программу после нажатия любой клавиши
        public void InputHandler()
        {
            Console.ReadKey(); // Ожидаем нажатие клавиши
            Console.Clear();
            Console.WriteLine("Программа завершится после нажатия любой клавиши...");
            Console.ReadKey(); // Ожидаем подтверждение для завершения
            Environment.Exit(1); // Завершение программы с кодом ошибки 1
        }

        // Метод для показа окна
        public void Show()
        {
            Console.Clear();
            windowDisplay.Show();
        }
    }
}
