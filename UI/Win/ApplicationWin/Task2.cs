using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task2 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 2: Update Operations", typeof(ProgramOptions));

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => null;

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public void Show() => windowDisplay.Show();

        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);
            WindowTools.UpdateCursorPos(lower, ref windowDisplay, (int)ProgramOptions.CountOptions);

            if (WindowTools.IsKeySelect(lower)) HandleMenuOption();
        }

        private void HandleMenuOption()
        {
            Console.Clear();
            PointDB db = Application.dB;

            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.UpdateProduct:
                    UpdateProduct(db);
                    break;

                case ProgramOptions.UpdateFirm:
                    UpdateFirm(db);
                    break;

                case ProgramOptions.UpdateManager:
                    UpdateManager(db);
                    break;

                case ProgramOptions.UpdateProductType:
                    UpdateProductType(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void UpdateProduct(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Products"));
            Console.Write("Введите ID товара для обновления: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название товара: ");
            string productName = Console.ReadLine();
            Console.Write("Введите новое количество: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Введите новую цену: ");
            decimal price = decimal.Parse(Console.ReadLine());

            string query = "UPDATE Products SET ProductName = @ProductName, Quantity = @Quantity, UnitPrice = @Price WHERE Id = @ProductId";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Товар успешно обновлен!"]);
        }

        private void UpdateFirm(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Firms"));
            Console.Write("Введите ID фирмы для обновления: ");
            int firmId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название фирмы: ");
            string firmName = Console.ReadLine();

            string query = "UPDATE Firms SET FirmName = @FirmName WHERE Id = @FirmId";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@FirmName", firmName);
            cmd.Parameters.AddWithValue("@FirmId", firmId);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Фирма успешно обновлена!"]);
        }

        private void UpdateManager(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Managers"));
            Console.Write("Введите ID менеджера для обновления: ");
            int managerId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя менеджера: ");
            string managerName = Console.ReadLine();

            string query = "UPDATE Managers SET ManagerName = @ManagerName WHERE Id = @ManagerId";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ManagerName", managerName);
            cmd.Parameters.AddWithValue("@ManagerId", managerId);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Менеджер успешно обновлен!"]);
        }

        private void UpdateProductType(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM ProductType"));
            Console.Write("Введите ID типа товара для обновления: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название типа: ");
            string typeName = Console.ReadLine();

            string query = "UPDATE ProductType SET TypeName = @TypeName WHERE Id = @TypeId";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@TypeName", typeName);
            cmd.Parameters.AddWithValue("@TypeId", typeId);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Тип товара успешно обновлен!"]);
        }

        public enum ProgramOptions
        {
            UpdateProduct,
            UpdateFirm,
            UpdateManager,
            UpdateProductType,
            Back,
            CountOptions
        }
    }
}
