using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task1 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 1: Insert Operations", typeof(ProgramOptions));

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
            Console.CursorTop = SizeY;

            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.InsertNewProduct:
                    InsertNewProduct(db);
                    break;

                case ProgramOptions.InsertNewProductType:
                    InsertNewProductType(db);
                    break;

                case ProgramOptions.InsertNewManager:
                    InsertNewManager(db);
                    break;

                case ProgramOptions.InsertNewFirm:
                    InsertNewFirm(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InsertNewProduct(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM ProductType"));
            Console.Write("Введите ID типа товара: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите название товара: ");
            string productName = Console.ReadLine();
            Console.Write("Введите количество: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Введите себестоимость: ");
            decimal costPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Введите цену продажи: ");
            decimal unitPrice = decimal.Parse(Console.ReadLine());

            string query = "INSERT INTO Products (ProductName, ProductTypeId, Quantity, CostPrice, UnitPrice) VALUES (@ProductName, @ProductTypeId, @Quantity, @CostPrice, @UnitPrice)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@ProductTypeId", typeId);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@CostPrice", costPrice);
            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Товар успешно добавлен!"]);
        }

        private void InsertNewProductType(PointDB db)
        {
            Console.Write("Введите название типа товара: ");
            string typeName = Console.ReadLine();
            string query = "INSERT INTO ProductType (TypeName) VALUES (@TypeName)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@TypeName", typeName);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Тип товара успешно добавлен!"]);
        }

        private void InsertNewManager(PointDB db)
        {
            Console.Write("Введите имя менеджера: ");
            string managerName = Console.ReadLine();
            string query = "INSERT INTO Managers (ManagerName) VALUES (@ManagerName)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ManagerName", managerName);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Менеджер успешно добавлен!"]);
        }

        private void InsertNewFirm(PointDB db)
        {
            Console.Write("Введите название фирмы: ");
            string firmName = Console.ReadLine();
            string query = "INSERT INTO Firms (FirmName) VALUES (@FirmName)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@FirmName", firmName);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Фирма успешно добавлена!"]);
        }

        public enum ProgramOptions
        {
            InsertNewProduct,
            InsertNewProductType,
            InsertNewManager,
            InsertNewFirm,
            Back,
            CountOptions
        }
    }
}
