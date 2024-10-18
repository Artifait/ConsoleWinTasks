using ConsoleWinApp.UI;
using ConsoleWinTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QuizTop;
using QuizTop.UI;
using System.Diagnostics;
using System.IO;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task4 : IWin
    {
        #region DefaultPart
        public WindowDisplay windowDisplay = new("Task 4: Extended Reports System", typeof(ProgramOptions));

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
                case ProgramOptions.ShowTop3CountriesByContinentMinPopulation:
                    ShowTop3CountriesByContinentMinPopulation();
                    break;

                case ProgramOptions.ShowTop3CountriesByContinentMaxPopulation:
                    ShowTop3CountriesByContinentMaxPopulation();
                    break;

                case ProgramOptions.ShowAveragePopulationInCountry:
                    ShowAveragePopulationInCountry();
                    break;

                case ProgramOptions.ShowSmallestCityByPopulationInCountry:
                    ShowSmallestCityByPopulationInCountry();
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        // Показать топ-3 стран по каждой части света с наименьшим количеством жителей
        public void ShowTop3CountriesByContinentMinPopulation()
        {
            Console.Write("Выберите вывод на экран (E) или в файл (F): ");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            using (var context = new CountriesContext())
            {
                var continents = context.Continents
                                        .Include(c => c.Countries) 
                                        .ToList();


                foreach (var continent in continents)
                {
                    Console.WriteLine(continent.ContinentName);

                    var top3Countries = continent.Countries
                                                 .OrderBy(c => c.Population)
                                                 .Take(3)
                                                 .Select(c => new { c.CountryName, c.Population })
                                                 .ToList();

                    if (top3Countries.Any())
                    {
                        Task2.OutputReport(top3Countries, outputChoice, $"{continent.ContinentName}_Top3MinPopulation.txt");
                    }
                    else
                    {
                        WindowsHandler.AddInfoWindow(new[] { $"В континенте '{continent.ContinentName}' нет данных." });
                    }
                }
            }
        }

        // Показать топ-3 стран по каждой части света с наибольшим количеством жителей
        public void ShowTop3CountriesByContinentMaxPopulation()
        {
            Console.Write("Выберите вывод на экран (E) или в файл (F): ");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            using (var context = new CountriesContext())
            {
                var continents = context.Continents
                                        .Include(c => c.Countries) 
                                        .ToList();

                foreach (var continent in continents)
                {
                    Console.WriteLine(continent.ContinentName);

                    var top3Countries = continent.Countries
                                                 .OrderByDescending(c => c.Population)
                                                 .Take(3)
                                                 .Select(c => new { c.CountryName, c.Population })
                                                 .ToList();

                    if (top3Countries.Any())
                    {
                        Task2.OutputReport(top3Countries, outputChoice, $"{continent.ContinentName}_Top3MaxPopulation.txt");
                    }
                    else
                    {
                        WindowsHandler.AddInfoWindow(new[] { $"В континенте '{continent.ContinentName}' нет данных." });
                    }
                }
            }
        }


        // Показать среднее количество жителей в конкретной стране
        public void ShowAveragePopulationInCountry()
        {
            Console.WriteLine("Введите название страны:");
            string countryName = Console.ReadLine();
            Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

            using (var context = new CountriesContext())
            {
                var averagePopulation = context.Cities
                                               .Where(c => c.IdCountryNavigation.CountryName == countryName)
                                               .Average(c => (double?)c.CityPopulation) ?? 0;

                Task2.OutputReport(new[] { new { CountryName = countryName, AveragePopulation = averagePopulation } },
                             outputChoice, $"{countryName}_AveragePopulation.txt");
            }
        }

        // Показать город с наименьшим количеством жителей в конкретной стране
        public void ShowSmallestCityByPopulationInCountry()
        {
            Console.WriteLine("Введите название страны:");
            string countryName = Console.ReadLine();
            Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

            using (var context = new CountriesContext())
            {
                var city = context.Cities
                                  .Where(c => c.IdCountryNavigation.CountryName == countryName)
                                  .OrderBy(c => c.CityPopulation)
                                  .Select(c => new { c.CityName, c.CityPopulation })
                                  .FirstOrDefault();

                if (city != null)
                {
                    Task2.OutputReport(new[] { city }, outputChoice, $"{countryName}_SmallestCity.txt");
                }
                else
                {
                    WindowsHandler.AddInfoWindow([$"Для страны '{countryName}' не найдено городов."]);
                }
            }
        }

        public enum ProgramOptions
        {
            ShowTop3CountriesByContinentMinPopulation,
            ShowTop3CountriesByContinentMaxPopulation,
            ShowAveragePopulationInCountry,
            ShowSmallestCityByPopulationInCountry,
            Back,
            CountOptions
        }
    }
}
