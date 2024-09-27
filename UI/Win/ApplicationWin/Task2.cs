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
                case ProgramOptions.UpdateProductInfo:
                    UpdateProductInfo(db);
                    break;

                case ProgramOptions.UpdateSupplierInfo:
                    UpdateSupplierInfo(db);
                    break;

                case ProgramOptions.UpdateProductTypeInfo:
                    UpdateProductTypeInfo(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        // Обновление информации о товаре
        private void UpdateProductInfo(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from Products"));
            Console.Write("Введите ID товара для обновления: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое количество: ");
            int newQuantity = int.Parse(Console.ReadLine());
            Console.Write("Введите новую цену: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());

            string query = "UPDATE Products SET Quantity = @Quantity, Price = @Price WHERE ProductID = @ProductID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@Quantity", newQuantity);
            cmd.Parameters.AddWithValue("@Price", newPrice);
            cmd.Parameters.AddWithValue("@ProductID", productId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Информация о товаре успешно обновлена!"]);
        }

        // Обновление информации о поставщике
        private void UpdateSupplierInfo(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from Suppliers"));
            Console.Write("Введите ID поставщика для обновления: ");
            int supplierId = int.Parse(Console.ReadLine());
            Console.Write("Введите новую контактную информацию: ");
            string newContactInfo = Console.ReadLine();

            string query = "UPDATE Suppliers SET ContactInfo = @ContactInfo WHERE SupplierID = @SupplierID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ContactInfo", newContactInfo);
            cmd.Parameters.AddWithValue("@SupplierID", supplierId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Информация о поставщике успешно обновлена!"]);
        }

        // Обновление информации о типе товара
        private void UpdateProductTypeInfo(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from ProductTypes"));
            Console.Write("Введите ID типа товара для обновления: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название типа товара: ");
            string newTypeName = Console.ReadLine();

            string query = "UPDATE ProductTypes SET TypeName = @TypeName WHERE TypeID = @TypeID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@TypeName", newTypeName);
            cmd.Parameters.AddWithValue("@TypeID", typeId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Информация о типе товара успешно обновлена!"]);
        }

        public enum ProgramOptions
        {
            UpdateProductInfo,
            UpdateSupplierInfo,
            UpdateProductTypeInfo,
            Back,
            CountOptions
        }
    }
}
