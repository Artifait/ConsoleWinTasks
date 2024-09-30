using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task4 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 4: Advanced SQL Queries", typeof(ProgramOptions));

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
                case ProgramOptions.TopManagerByUnitsSold:
                    ShowTopManagerByUnitsSold(db);
                    break;

                case ProgramOptions.TopManagerByProfit:
                    ShowTopManagerByProfit(db);
                    break;

                case ProgramOptions.TopManagerByProfitInRange:
                    ShowTopManagerByProfitInRange(db);
                    break;

                case ProgramOptions.TopCustomerByAmountSpent:
                    ShowTopCustomerByAmountSpent(db);
                    break;

                case ProgramOptions.TopProductTypeByUnitsSold:
                    ShowTopProductTypeByUnitsSold(db);
                    break;

                case ProgramOptions.TopProductTypeByProfit:
                    ShowTopProductTypeByProfit(db);
                    break;

                case ProgramOptions.MostPopularProduct:
                    ShowMostPopularProduct(db);
                    break;

                case ProgramOptions.ProductsNotSoldInDays:
                    ShowProductsNotSoldInDays(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void ShowTopManagerByUnitsSold(PointDB db)
        {
            string query = @"
                select top 1 m.[ManagerName] 
                from [Managers] m
                join [Sales] s on m.[Id] = s.[ManagerId]
                group by m.[ManagerName]
                order by sum(s.[QuantitySold]) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowTopManagerByProfit(PointDB db)
        {
            string query = @"
                select top 1 m.[ManagerName]
                from [Managers] m
                join [Sales] s on m.[Id] = s.[ManagerId]
                join [Products] p on s.[ProductId] = p.[Id]
                group by m.[ManagerName]
                order by sum((p.[UnitPrice] - p.[CostPrice]) * s.[QuantitySold]) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowTopManagerByProfitInRange(PointDB db)
        {
            Console.Write("Введите начальную дату (yyyy-mm-dd): ");
            string startDate = Console.ReadLine();
            Console.Write("Введите конечную дату (yyyy-mm-dd): ");
            string endDate = Console.ReadLine();

            string query = @"
                select top 1 m.ManagerName
                from Managers m
                join Sales s on m.Id = s.ManagerId
                join Products p on s.ProductId = p.Id
                where s.SaleDate between @StartDate and @EndDate
                group by m.ManagerName
                order by sum((p.UnitPrice - p.CostPrice) * s.QuantitySold) desc";

            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            TV.DisplayTable(db.ExecuteQuery(cmd));
        }

        private void ShowTopCustomerByAmountSpent(PointDB db)
        {
            string query = @"
                select top 1 f.[FirmName]
                from [Firms] f
                join [Sales] s on f.[Id] = s.[FirmId]
                join [Products] p on s.[ProductId] = p.[Id]
                group by f.[FirmName]
                order by sum(p.[UnitPrice] * s.[QuantitySold]) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowTopProductTypeByUnitsSold(PointDB db)
        {
            string query = @"
                select top 1 pt.[TypeName]
                from [ProductType] pt
                join [Products] p on pt.[Id] = p.[ProductTypeId]
                join [Sales] s on p.[Id] = s.[ProductId]
                group by pt.[TypeName]
                order by sum(s.[QuantitySold]) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowTopProductTypeByProfit(PointDB db)
        {
            string query = @"
                select top 1 pt.TypeName
                from ProductType pt
                join Products p on pt.Id = p.ProductTypeId
                join Sales s on p.Id = s.ProductId
                group by pt.TypeName
                order by sum((p.UnitPrice - p.CostPrice) * s.QuantitySold) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowMostPopularProduct(PointDB db)
        {
            string query = @"
                select top 1 p.ProductName
                from Products p
                join Sales s on p.Id = s.ProductId
                group by p.ProductName
                order by sum(s.QuantitySold) desc";

            TV.DisplayTable(db.ExecuteQuery(query));
        }

        private void ShowProductsNotSoldInDays(PointDB db)
        {
            Console.Write("Введите количество дней: ");
            int days = int.Parse(Console.ReadLine());

            string query = @"
                select p.ProductName
                from Products p
                left join Sales s on p.Id = s.ProductId and s.SaleDate > getdate() - @Days 
                where s.Id is null;";

            SqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@Days", days);

            TV.DisplayTable(db.ExecuteQuery(cmd));
        }

        public enum ProgramOptions
        {
            TopManagerByUnitsSold,
            TopManagerByProfit,
            TopManagerByProfitInRange,
            TopCustomerByAmountSpent,
            TopProductTypeByUnitsSold,
            TopProductTypeByProfit,
            MostPopularProduct,
            ProductsNotSoldInDays,
            Back,
            CountOptions
        }
    }
}
