using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task3 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 3: Delete Operations", typeof(ProgramOptions));

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
                case ProgramOptions.DeleteProduct:
                    DeleteProduct(db);
                    break;

                case ProgramOptions.DeleteSupplier:
                    DeleteSupplier(db);
                    break;

                case ProgramOptions.DeleteProductType:
                    DeleteProductType(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        // Удаление товара
        private void DeleteProduct(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from Products"));
            Console.Write("Введите ID товара для удаления: ");
            int productId = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Products WHERE ProductID = @ProductID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@ProductID", productId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Товар успешно удален!"]);
        }

        // Удаление поставщика
        private void DeleteSupplier(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from Suppliers"));
            Console.Write("Введите ID поставщика для удаления: ");
            int supplierId = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@SupplierID", supplierId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Поставщик успешно удален!"]);
        }

        // Удаление типа товара
        private void DeleteProductType(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("select * from ProductTypes"));
            Console.Write("Введите ID типа товара для удаления: ");
            int typeId = int.Parse(Console.ReadLine());

            string query = "DELETE FROM ProductTypes WHERE TypeID = @TypeID";
            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@TypeID", typeId);

            db.ExecuteNonQuery(cmd);
            WindowsHandler.AddInfoWindow(["Тип товара успешно удален!"]);
        }

        public enum ProgramOptions
        {
            DeleteProduct,
            DeleteSupplier,
            DeleteProductType,
            Back,
            CountOptions
        }
    }
}
