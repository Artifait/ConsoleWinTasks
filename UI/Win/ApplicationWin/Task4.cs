using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
	public class Task4 : IWin
	{
		public WindowDisplay windowDisplay = new("Task 4: Advanced Display Operations", typeof(ProgramOptions));

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
				case ProgramOptions.DisplayProductsByType:
					DisplayProductsByType(db);
					break;

				case ProgramOptions.DisplayProductsByManager:
					DisplayProductsByManager(db);
					break;

				case ProgramOptions.DisplayProductsByFirm:
					DisplayProductsByFirm(db);
					break;

				case ProgramOptions.DisplayRecentSale:
					DisplayRecentSale(db);
					break;

				case ProgramOptions.DisplayAverageQuantityByType:
					DisplayAverageQuantityByType(db);
					break;

				case ProgramOptions.Back:
					Application.WinStack.Pop();
					break;
			}
		}

		private void DisplayProductsByType(PointDB db)
		{
			Console.Write("Введите ID типа товара: ");
			int typeId = int.Parse(Console.ReadLine());
			string query = "SELECT * FROM Products WHERE ProductTypeId = @TypeId";
			SqlCommand cmd = new(query);
			cmd.Parameters.AddWithValue("@TypeId", typeId);
			TV.DisplayTable(db.ExecuteQuery(cmd));
		}

		private void DisplayProductsByManager(PointDB db)
		{
			Console.Write("Введите ID менеджера: ");
			int managerId = int.Parse(Console.ReadLine());
			string query = "SELECT * FROM Products p INNER JOIN Sales s ON p.Id = s.ProductId WHERE s.ManagerId = @ManagerId";
			SqlCommand cmd = new(query);
			cmd.Parameters.AddWithValue("@ManagerId", managerId);
			TV.DisplayTable(db.ExecuteQuery(cmd));
		}

		private void DisplayProductsByFirm(PointDB db)
		{
			Console.Write("Введите ID фирмы: ");
			int firmId = int.Parse(Console.ReadLine());
			string query = "SELECT * FROM Products p INNER JOIN Sales s ON p.Id = s.ProductId WHERE s.FirmId = @FirmId";
			SqlCommand cmd = new(query);
			cmd.Parameters.AddWithValue("@FirmId", firmId);
			TV.DisplayTable(db.ExecuteQuery(cmd));
		}

		private void DisplayRecentSale(PointDB db)
		{
			string query = "SELECT TOP 1 * FROM Sales ORDER BY SaleDate DESC";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		private void DisplayAverageQuantityByType(PointDB db)
		{
			string query = "SELECT pt.TypeName, AVG(p.Quantity) AS AverageQuantity FROM Products p INNER JOIN ProductType pt ON p.ProductTypeId = pt.Id GROUP BY pt.TypeName";
			TV.DisplayTable(db.ExecuteQuery(query));
		}

		public enum ProgramOptions
		{
			DisplayProductsByType,
			DisplayProductsByManager,
			DisplayProductsByFirm,
			DisplayRecentSale,
			DisplayAverageQuantityByType,
			Back,
			CountOptions
		}
	}
}
