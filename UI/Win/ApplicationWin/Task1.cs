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

            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.InsertNewProduct:
                    InsertNewProduct(db);
                    break;

                case ProgramOptions.InsertNewProductType:
                    InsertNewProductType(db);
                    break;

                case ProgramOptions.InsertNewSupplier:
                    InsertNewSupplier(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InsertNewProduct(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from ProductTypes"));
            Console.Write("Введите ID типа товара: ");
            int typeId = int.Parse(Console.ReadLine());
            TV.DisplayTable(db.ExecuteQuery("select * from Suppliers"));
            Console.Write("Введите ID поставщика: ");
            int supplierId = int.Parse(Console.ReadLine());
            Console.Write("Введите название товара: ");
            string productName = Console.ReadLine();
            Console.Write("Введите количество: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Введите цену: ");
            decimal price = decimal.Parse(Console.ReadLine());

            string query = "INSERT INTO Products (ProductName, Quantity, Price, TypeID, SupplierID) VALUES (@ProductName, @Quantity, @Price, @TypeID, @SupplierID)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@TypeID", typeId);
            cmd.Parameters.AddWithValue("@SupplierID", supplierId);
            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Товар успешно добавлен!"]);
        }

        private void InsertNewProductType(PointDB db)
        {
            Console.Write("Введите название типа товара: ");
            string typeName = Console.ReadLine();

            string query = "INSERT INTO ProductTypes (TypeName) VALUES (@TypeName)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@TypeName", typeName);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Тип товара успешно добавлен!"]);
        }

        // Вставка нового поставщика
        private void InsertNewSupplier(PointDB db)
        {
            Console.Write("Введите имя поставщика: ");
            string supplierName = Console.ReadLine();
            Console.Write("Введите контактную информацию: ");
            string contactInfo = Console.ReadLine();
            string query = "INSERT INTO Suppliers (SupplierName, ContactInfo) VALUES (@SupplierName, @ContactInfo)";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@SupplierName", supplierName);
            cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Поставщик успешно добавлен!"]);
        }

        public enum ProgramOptions
        {
            InsertNewProductType,
            InsertNewSupplier,
            InsertNewProduct,
            Back,
            CountOptions
        }
    }
}
