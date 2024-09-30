using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
	public class Task3 : IWin
	{
		public WindowDisplay windowDisplay = new("Task 3: Display Operations", typeof(ProgramOptions));

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
				case ProgramOptions.DisplayAllProducts:
					DisplayAllProducts(db);
					break;

				case ProgramOptions.DisplayAllProductTypes:
					DisplayAllProductTypes(db);
					break;

				case ProgramOptions.DisplayAllManagers:
					DisplayAllManagers(db);
					break;

				case ProgramOptions.DisplayMaxQuantityProducts:
					DisplayMaxQuantityProducts(db);
					break;

				case ProgramOptions.DisplayMinQuantityProducts:
					DisplayMinQuantityProducts(db);
					break;

				case ProgramOptions.DisplayMinCostPriceProducts:
					DisplayMinCostPriceProducts(db);
					break;

				case ProgramOptions.DisplayMaxCostPriceProducts:
					DisplayMaxCostPriceProducts(db);
					break;

				case ProgramOptions.Back:
					Application.WinStack.Pop();
					break;
			}
		}

		private void DisplayAllProducts(PointDB db)
		{
			TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Products"));
		}

		private void DisplayAllProductTypes(PointDB db)
		{
			TV.DisplayTable(db.ExecuteQuery("SELECT * FROM ProductType"));
		}

		private void DisplayAllManagers(PointDB db)
		{
			TV.DisplayTable(db.ExecuteQuery("SELECT * FROM Managers"));
		}

		private void DisplayMaxQuantityProducts(PointDB db)
		{
			string query = "SELECT TOP 1 * FROM Products ORDER BY Quantity DESC";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		private void DisplayMinQuantityProducts(PointDB db)
		{
			string query = "SELECT TOP 1 * FROM Products ORDER BY Quantity ASC";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		private void DisplayMinCostPriceProducts(PointDB db)
		{
			string query = "SELECT TOP 1 * FROM Products ORDER BY CostPrice ASC";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		private void DisplayMaxCostPriceProducts(PointDB db)
		{
			string query = "SELECT TOP 1 * FROM Products ORDER BY CostPrice DESC";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		public enum ProgramOptions
		{
			DisplayAllProducts,
			DisplayAllProductTypes,
			DisplayAllManagers,
			DisplayMaxQuantityProducts,
			DisplayMinQuantityProducts,
			DisplayMinCostPriceProducts,
			DisplayMaxCostPriceProducts,
			Back,
			CountOptions
		}
	}
}
