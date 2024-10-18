
using Microsoft.EntityFrameworkCore;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task3 : IWin
    {
        #region DefaultPart
        public WindowDisplay windowDisplay = new("Task 3: Extended Reports System", typeof(ProgramOptions));

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
        #endregion

        private void HandleMenuOption()
        {
            Console.Clear();
            PointDB db = Application.dB;

            Console.CursorTop = SizeY;
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.ShowCountriesByLetter:
                    ShowCountriesByLetter();
                    break;

                case ProgramOptions.ShowCapitalsByLetter:
                    ShowCapitalsByLetter();
                    break;

                case ProgramOptions.ShowTop3CapitalsByPopulation:
                    ShowTop3CapitalsByPopulation();
                    break;

                case ProgramOptions.ShowTop3CountriesByPopulation:
                    ShowTop3CountriesByPopulation();
                    break;

                case ProgramOptions.ShowAverageCapitalPopulationByContinent:
                    ShowAverageCapitalPopulationByContinent();
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        // Отображение всех стран, чьё имя начинается с указанной буквы
        public void ShowCountriesByLetter()
        {
            Console.Write("Введите букву:");
            char letter = char.ToUpper(Console.ReadKey().KeyChar);
            Console.Write("\nВыберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            using (var context = new CountriesContext())
            {
                var countries = context.Countries
                                       .Where(c => c.CountryName.StartsWith(letter.ToString()))
                                       .Select(c => new { c.CountryName, c.Population, c.Area, c.CapitalName })
                                       .ToList();

                Task2.OutputReport(countries, outputChoice, $"Countries_StartingWith_{letter}.txt");
            }
        }

        // Отображение всех столиц, чьё имя начинается с указанной буквы
        public void ShowCapitalsByLetter()
        {
            Console.WriteLine("Введите букву:");
            char letter = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("\nВыберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            using (var context = new CountriesContext())
            {
                var capitals = context.Countries
                                      .Where(c => c.CapitalName.StartsWith(letter.ToString()))
                                      .Select(c => new { c.CapitalName, c.Population })
                                      .ToList();

                Task2.OutputReport(capitals, outputChoice, $"Capitals_StartingWith_{letter}.txt");
            }
        }

        // Показать топ-3 столиц с наименьшим количеством жителей
        public void ShowTop3CapitalsByPopulation()
        {
            Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

            using (var context = new CountriesContext())
            {
                var capitals = context.Countries
                                      .Where(c => c.IdCapitalCity.HasValue)
                                      .OrderBy(c => c.IdCapitalCityNavigation.CityPopulation)
                                      .Take(3)
                                      .Select(c => new { c.CapitalName, c.IdCapitalCityNavigation.CityPopulation })
                                      .ToList();

                Task2.OutputReport(capitals, outputChoice, "Top3CapitalsByPopulation.txt");
            }
        }

        // Показать топ-3 стран с наименьшим количеством жителей
        public void ShowTop3CountriesByPopulation()
        {
            Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

            using (var context = new CountriesContext())
            {
                var countries = context.Countries
                                       .OrderBy(c => c.Population)
                                       .Take(3)
                                       .Select(c => new { c.CountryName, c.Population })
                                       .ToList();

                Task2.OutputReport(countries, outputChoice, "Top3CountriesByPopulation.txt");
            }
        }

        // Показать среднее количество жителей в столицах по каждой части света
        public void ShowAverageCapitalPopulationByContinent()
        {
            Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

            using (var context = new CountriesContext())
            {
                var continents = context.Continents
                                        .Select(cont => new
                                        {
                                            cont.ContinentName,
                                            AveragePopulation = cont.Countries
                                                                   .Where(c => c.IdCapitalCity.HasValue)
                                                                   .Average(c => (double?)c.IdCapitalCityNavigation.CityPopulation) ?? 0
                                        })
                                        .ToList();

                Task2.OutputReport(continents, outputChoice, "AverageCapitalPopulationByContinent.txt");
            }
        }

        public enum ProgramOptions
        {
            ShowCountriesByLetter,
            ShowCapitalsByLetter,
            ShowTop3CapitalsByPopulation,
            ShowTop3CountriesByPopulation,
            ShowAverageCapitalPopulationByContinent,
            Back,
            CountOptions
        }
    }
}
