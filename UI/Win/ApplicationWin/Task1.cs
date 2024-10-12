using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task1 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 1: Data Operations", typeof(ProgramOptions));

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
            PointDB db = Application.db;
            Console.CursorTop = SizeY;

            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.AddCountriesData:
                    AddCountriesData(db);
                    break;

                case ProgramOptions.EditCountriesData:
                    EditCountriesData(db);
                    break;

                case ProgramOptions.DeleteCountriesData:
                    DeleteCountriesData(db);
                    break;

                case ProgramOptions.AddCoffeeTypesData:
                    AddCoffeeTypesData(db);
                    break;

                case ProgramOptions.EditCoffeeTypesData:
                    EditCoffeeTypesData(db);
                    break;

                case ProgramOptions.DeleteCoffeeTypesData:
                    DeleteCoffeeTypesData(db);
                    break;

                case ProgramOptions.AddCoffeeData:
                    AddCoffeeData(db);
                    break;

                case ProgramOptions.EditCoffeeData:
                    EditCoffeeData(db);
                    break;

                case ProgramOptions.DeleteCoffeeData:
                    DeleteCoffeeData(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void AddCountriesData(PointDB db)
        {
            Console.WriteLine("Enter the Country Name: ");
            string countryName = Console.ReadLine();

            string insertQuery = "INSERT INTO Countries (CountryName) VALUES (@CountryName)";
            SqlParameter[] parameters = { new SqlParameter("@CountryName", countryName) };
            db.ExecuteQuery(insertQuery, parameters);

            Console.WriteLine("Country added successfully!");
            Console.WriteLine("Current state of the Countries table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));
        }

        private void EditCountriesData(PointDB db)
        {
            Console.WriteLine("Current state of the Countries table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));

            Console.WriteLine("Enter the Id of the Country to edit: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new Country Name: ");
            string newCountryName = Console.ReadLine();

            string updateQuery = "UPDATE Countries SET CountryName = @CountryName WHERE Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@CountryName", newCountryName), new SqlParameter("@Id", id) };
            db.ExecuteQuery(updateQuery, parameters);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Country updated successfully!");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));
        }

        private void DeleteCountriesData(PointDB db)
        {
            Console.WriteLine("Current state of the Countries table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));

            Console.WriteLine("Enter the Id of the Country to delete: ");
            int id = int.Parse(Console.ReadLine());

            if (id < 0)
            {
                Console.WriteLine("Operation canceled.");
                return;
            }

            string referenceCheckQuery = "SELECT * FROM Coffee WHERE CountryID = @Id";
            SqlParameter[] checkParams = { new SqlParameter("@Id", id) };
            var result = db.ExecuteQuery(referenceCheckQuery, checkParams);

            Console.Clear();
            Console.CursorTop = SizeY;

            if (result.Rows.Count > 0)
            {

                Console.WriteLine("Невозможно удалить эту страну, поскольку на нее ссылаются следующие записи в таблице Кофе:");
                TV.DisplayTable(result);
            }
            else
            {
                string deleteQuery = "DELETE FROM Countries WHERE Id = @Id";
                db.ExecuteQuery(deleteQuery, checkParams);
                Console.WriteLine("Country deleted successfully!");
                TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));
            }
        }

        private void AddCoffeeTypesData(PointDB db)
        {
            Console.WriteLine("Enter the Coffee Type Name: ");
            string coffeeTypeName = Console.ReadLine();

            string insertQuery = "INSERT INTO CoffeeTypes (CoffeeTypeName) VALUES (@CoffeeTypeName)";
            SqlParameter[] parameters = { new SqlParameter("@CoffeeTypeName", coffeeTypeName) };
            db.ExecuteQuery(insertQuery, parameters);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Coffee Type added successfully!");
            Console.WriteLine("Current state of the CoffeeTypes table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));
        }

        private void EditCoffeeTypesData(PointDB db)
        {
            Console.WriteLine("Current state of the CoffeeTypes table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));

            Console.WriteLine("Enter the Id of the Coffee Type to edit: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new Coffee Type Name: ");
            string newCoffeeTypeName = Console.ReadLine();

            string updateQuery = "UPDATE CoffeeTypes SET CoffeeTypeName = @CoffeeTypeName WHERE Id = @Id";
            SqlParameter[] parameters = { new("@CoffeeTypeName", newCoffeeTypeName), new("@Id", id) };
            db.ExecuteQuery(updateQuery, parameters);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Coffee Type updated successfully!");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));
        }

        private void DeleteCoffeeTypesData(PointDB db)
        {
            Console.WriteLine("Current state of the CoffeeTypes table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));

            Console.WriteLine("Enter the Id of the Coffee Type to delete: ");
            int id = int.Parse(Console.ReadLine());

            if (id < 0)
            {
                WindowsHandler.AddErroreWindow(["Operation canceled."]);
                return;
            }

            string referenceCheckQuery = "SELECT * FROM Coffee WHERE CoffeeTypeID = @Id";
            SqlParameter[] checkParams = { new("@Id", id) };
            var result = db.ExecuteQuery(referenceCheckQuery, checkParams);

            Console.Clear();
            Console.CursorTop = SizeY;

            if (result.Rows.Count > 0)
            {
                Console.WriteLine("Невозможно удалить этот тип, поскольку на него ссылаются следующие записи в таблице Кофе:");
                TV.DisplayTable(result);
            }
            else
            {
                string deleteQuery = "DELETE FROM CoffeeTypes WHERE Id = @Id";
                db.ExecuteQuery(deleteQuery, checkParams);
                Console.WriteLine("Coffee Type deleted successfully!");
                TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));
            }
        }

        private void AddCoffeeData(PointDB db)
        {
            Console.WriteLine("Available Countries:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Countries"));

            Console.WriteLine("Enter the Country ID: ");
            int countryId = int.Parse(Console.ReadLine());

            Console.WriteLine("Available Coffee Types:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM CoffeeTypes"));

            Console.WriteLine("Enter the Coffee Type ID: ");
            int coffeeTypeId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Coffee Name: ");
            string coffeeName = Console.ReadLine();
            Console.WriteLine("Enter the Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter the Quantity in Grams: ");
            int quantityInGrams = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Cost Price: ");
            decimal costPrice = decimal.Parse(Console.ReadLine());

            string insertQuery = "INSERT INTO Coffee (CoffeeName, CountryID, CoffeeTypeID, Description, QuantityInGrams, CostPrice) " +
                                 "VALUES (@CoffeeName, @CountryID, @CoffeeTypeID, @Description, @QuantityInGrams, @CostPrice)";
            SqlParameter[] parameters =
            {
                new("@CoffeeName", coffeeName),
                new("@CountryID", countryId),
                new("@CoffeeTypeID", coffeeTypeId),
                new("@Description", description),
                new("@QuantityInGrams", quantityInGrams),
                new("@CostPrice", costPrice)
            };

            db.ExecuteQuery(insertQuery, parameters);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Coffee added successfully!");
            Console.WriteLine("Current state of the Coffee table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Coffee"));
        }

        private void EditCoffeeData(PointDB db)
        {
            Console.WriteLine("Current state of the Coffee table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Coffee"));

            Console.WriteLine("Enter the Coffee ID to edit: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new Coffee Name: ");
            string newCoffeeName = Console.ReadLine();
            Console.WriteLine("Enter the new Description: ");
            string newDescription = Console.ReadLine();
            Console.WriteLine("Enter the new Quantity in Grams: ");
            int newQuantityInGrams = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new Cost Price: ");
            decimal newCostPrice = decimal.Parse(Console.ReadLine());

            string updateQuery = "UPDATE Coffee SET CoffeeName = @CoffeeName, Description = @Description, " +
                                 "QuantityInGrams = @QuantityInGrams, CostPrice = @CostPrice WHERE CoffeeID = @Id";
            SqlParameter[] parameters =
            {
                new("@CoffeeName", newCoffeeName),
                new("@Description", newDescription),
                new("@QuantityInGrams", newQuantityInGrams),
                new("@CostPrice", newCostPrice),
                new("@Id", id)
            };
            db.ExecuteQuery(updateQuery, parameters);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Coffee updated successfully!");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Coffee"));
        }

        private void DeleteCoffeeData(PointDB db)
        {
            Console.Clear();
            Console.CursorTop = SizeY;
            Console.WriteLine("Current state of the Coffee table:");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Coffee"));

            Console.WriteLine("Enter the Coffee ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            if (id < 0)
            {
                WindowsHandler.AddErroreWindow(["Operation canceled."]);
                return;
            }

            string deleteQuery = "DELETE FROM Coffee WHERE CoffeeID = @Id";
            SqlParameter[] deleteParams = { new("@Id", id) };
            db.ExecuteQuery(deleteQuery, deleteParams);

            Console.Clear();
            Console.CursorTop = SizeY;

            Console.WriteLine("Coffee deleted successfully!");
            TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Coffee"));
        }

        public enum ProgramOptions
        {
            AddCountriesData,
            EditCountriesData,
            DeleteCountriesData,

            AddCoffeeTypesData,
            EditCoffeeTypesData,
            DeleteCoffeeTypesData,

            AddCoffeeData,
            EditCoffeeData,
            DeleteCoffeeData,

            Back,
            CountOptions
        }
    }
}
