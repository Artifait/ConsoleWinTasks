using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin
{
    public class Task4 : IWin
    {
        public WindowDisplay windowDisplay = new("Task 4: Show Information", typeof(ProgramOptions));

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
                case ProgramOptions.ShowSupplierWithMaxProducts:
                    ShowSupplierWithMaxProducts(db);
                    break;

                case ProgramOptions.ShowSupplierWithMinProducts:
                    ShowSupplierWithMinProducts(db);
                    break;

                case ProgramOptions.ShowProductTypeWithMaxProducts:
                    ShowProductTypeWithMaxProducts(db);
                    break;

                case ProgramOptions.ShowProductTypeWithMinProducts:
                    ShowProductTypeWithMinProducts(db);
                    break;

                case ProgramOptions.ShowProductsByDaysPassed:
                    ShowProductsByDaysPassed(db);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        // Запрос для отображения поставщика с наибольшим количеством товаров
        private void ShowSupplierWithMaxProducts(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery(@"
                select s.[SupplierName], s.[ContactInfo], sum(p.[Quantity]) as cnt
                from [Suppliers] s
                join [Products] p on s.[Id] = p.[Id]
                group by s.[SupplierName], s.[ContactInfo]
                having sum(p.[Quantity]) = (
                    select max(t.[TotalQuantity])
                    from (
                        select sum(p2.[Quantity]) as [TotalQuantity]
                        from [Suppliers] s2
                        join [Products] p2 on s2.[Id] = p2.[Id]
                        group by s2.[SupplierName]
                    ) t );"
            ));
        }

        // Запрос для отображения поставщика с наименьшим количеством товаров
        private void ShowSupplierWithMinProducts(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery(@"
                select s.[SupplierName], s.[ContactInfo], sum(p.[Quantity]) as cnt
                from [Suppliers] s
                join [Products] p on s.[Id] = p.[Id]
                group by s.[SupplierName], s.[ContactInfo]
                having sum(p.[Quantity]) = (
                    select min(t.[TotalQuantity])
                    from (
                        select sum(p2.[Quantity]) as [TotalQuantity]
                        from [Suppliers] s2
                        join [Products] p2 on s2.[Id] = p2.[Id]
                        group by s2.[SupplierName]
                    ) t
                );"
            ));
        }

        // Запрос для отображения типа товаров с наибольшим количеством товаров
        private void ShowProductTypeWithMaxProducts(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery(@"
                select pt.[TypeName]
                from [ProductTypes] pt
                join [Products] p on pt.[Id] = p.[Id]
                group by pt.[TypeName]
                having sum(p.[Quantity]) = (
                    select max(t.[TotalQuantity])
                    from (
                        select sum(p2.[Quantity]) as [TotalQuantity]
                        from [ProductTypes] pt2
                        join [Products] p2 on pt2.[Id] = p2.[Id]
                        group by pt2.[TypeName]
                    ) t
                );"
            ));
        }

        // Запрос для отображения типа товаров с наименьшим количеством товаров
        private void ShowProductTypeWithMinProducts(PointDB db)
        {
            TV.DisplayTable(db.ExecuteQuery(@"
                select pt.[TypeName]
                from [ProductTypes] pt
                join [Products] p on pt.[Id] = p.[Id]
                group by pt.[TypeName]
                having sum(p.[Quantity]) = (
                    select min(t.[TotalQuantity])
                    from (
                        select sum(p2.[Quantity]) as [TotalQuantity]
                        from [ProductTypes] pt2
                        join [Products] p2 on pt2.[Id] = p2.[Id]
                        group by pt2.[TypeName]
                    ) t
                );"
            ));
        }

        // Запрос для отображения товаров, которые были поставлены заданное количество дней назад
        private void ShowProductsByDaysPassed(PointDB db)
        {
            Console.WriteLine("Введите количество дней:");
            if (int.TryParse(Console.ReadLine(), out int daysPassed))
            {
                TV.DisplayTable(db.ExecuteQuery($@"
                    select *
                    from [Products] p
                    where datediff(day, p.[DeliveryDate], getdate()) >= {daysPassed};"
                ));
            }
            else
            {
                Console.WriteLine("Ошибка: некорректный ввод.");
            }
        }

        public enum ProgramOptions
        {
            ShowSupplierWithMaxProducts,
            ShowSupplierWithMinProducts,
            ShowProductTypeWithMaxProducts,
            ShowProductTypeWithMinProducts,
            ShowProductsByDaysPassed,
            Back,
            CountOptions
        }
    }
}
