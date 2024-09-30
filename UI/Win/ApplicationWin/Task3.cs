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

                case ProgramOptions.DeleteManager:
                    DeleteManager(db);
                    break;

                case ProgramOptions.DeleteProductType:
                    DeleteProductType(db);
                    break;

                case ProgramOptions.DeleteFirm:
                    DeleteFirm(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void DeleteProduct(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Products"));
            Console.Write("Введите ID товара для удаления: ");
            int productId = int.Parse(Console.ReadLine());

            string archiveQuery = "INSERT INTO ArchivedProducts SELECT * FROM Products WHERE Id = @ProductId";
            string deleteQuery = "DELETE FROM Products WHERE Id = @ProductId";
            SqlCommand cmdArchive = new(archiveQuery);
            SqlCommand cmdDelete = new(deleteQuery);

            cmdArchive.Parameters.AddWithValue("@ProductId", productId);
            cmdDelete.Parameters.AddWithValue("@ProductId", productId);

            db.ExecuteNonQuery(cmdArchive);
            db.ExecuteNonQuery(cmdDelete);
            WindowsHandler.AddInfoWindow(["Товар успешно удален и архивирован!"]);
        }

        private void DeleteManager(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Managers"));
            Console.Write("Введите ID менеджера для удаления: ");
            int managerId = int.Parse(Console.ReadLine());

            string archiveQuery = "INSERT INTO ArchivedManagers SELECT * FROM Managers WHERE Id = @ManagerId";
            string deleteQuery = "DELETE FROM Managers WHERE Id = @ManagerId";
            SqlCommand cmdArchive = new(archiveQuery);
            SqlCommand cmdDelete = new(deleteQuery);

            cmdArchive.Parameters.AddWithValue("@ManagerId", managerId);
            cmdDelete.Parameters.AddWithValue("@ManagerId", managerId);

            db.ExecuteNonQuery(cmdArchive);
            db.ExecuteNonQuery(cmdDelete);
            WindowsHandler.AddInfoWindow(["Менеджер успешно удален и архивирован!"]);
        }

        private void DeleteProductType(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM ProductType"));
            Console.Write("Введите ID типа товара для удаления: ");
            int typeId = int.Parse(Console.ReadLine());

            string archiveQuery = "INSERT INTO ArchivedProductTypes SELECT * FROM ProductType WHERE Id = @TypeId";
            string deleteQuery = "DELETE FROM ProductType WHERE Id = @TypeId";
            SqlCommand cmdArchive = new(archiveQuery);
            SqlCommand cmdDelete = new(deleteQuery);

            cmdArchive.Parameters.AddWithValue("@TypeId", typeId);
            cmdDelete.Parameters.AddWithValue("@TypeId", typeId);

            db.ExecuteNonQuery(cmdArchive);
            db.ExecuteNonQuery(cmdDelete);
            WindowsHandler.AddInfoWindow(["Тип товара успешно удален и архивирован!"]);
        }

        private void DeleteFirm(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Firms"));
            Console.Write("Введите ID фирмы для удаления: ");
            int firmId = int.Parse(Console.ReadLine());

            string archiveQuery = "INSERT INTO ArchivedFirms SELECT * FROM Firms WHERE Id = @FirmId";
            string deleteQuery = "DELETE FROM Firms WHERE Id = @FirmId";
            SqlCommand cmdArchive = new(archiveQuery);
            SqlCommand cmdDelete = new(deleteQuery);

            cmdArchive.Parameters.AddWithValue("@FirmId", firmId);
            cmdDelete.Parameters.AddWithValue("@FirmId", firmId);

            db.ExecuteNonQuery(cmdArchive);
            db.ExecuteNonQuery(cmdDelete);
            WindowsHandler.AddInfoWindow(["Фирма успешно удалена и архивирована!"]);
        }

        public enum ProgramOptions
        {
            DeleteProduct,
            DeleteManager,
            DeleteProductType,
            DeleteFirm,
            Back,
            CountOptions
        }
    }
}
