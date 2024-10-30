using ConsoleWinTasks.UI.Win.WinTemplate;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task1 : CwTask
    {
        #region GeneratedCode
        public enum ProgramOptions
        {
            Back,
            Task1,
            Task2,
            Task3,
            Task4,
            Task5
        }

        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public Task1() : base(nameof(Task1))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler },
                { (int)ProgramOptions.Task1, Task1Handler },
                { (int)ProgramOptions.Task2, Task2Handler },
                { (int)ProgramOptions.Task3, Task3Handler },
                { (int)ProgramOptions.Task4, Task4Handler },
                { (int)ProgramOptions.Task5, Task5Handler }
            };
        }
        #endregion

        #region Logic

        private void Task1Handler()
        {
            Log("Task1Handler started.");
            Thread numberThread = new Thread(() => PrintNumbers(0, 50));
            numberThread.Start();
            numberThread.Join();
            Log("Task1Handler completed.");
            Console.ReadKey();
            ToStartWritePos();
        }

        private void Task2Handler()
        {
            Log("Task2Handler started - requesting range from user.");
            Console.Write("Введите начало диапазона: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Введите конец диапазона: ");
            int end = int.Parse(Console.ReadLine());

            Log($"User provided range: {start} to {end}.");

            Thread numberThread = new Thread(() => PrintNumbers(start, end));
            numberThread.Start();
            numberThread.Join();
            Log("Task2Handler completed.");
            Console.ReadKey();
            ToStartWritePos();
        }

        private void Task3Handler()
        {
            Log("Task3Handler started - requesting multiple threads and range from user.");
            Console.Write("Введите количество потоков: ");
            int threadCount = int.Parse(Console.ReadLine());

            Console.Write("Введите начало диапазона: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Введите конец диапазона: ");
            int end = int.Parse(Console.ReadLine());

            Log($"User provided thread count: {threadCount} and range: {start} to {end}.");

            int rangePerThread = (end - start + 1) / threadCount;
            List<Thread> thrs = new();
            for (int i = 0; i < threadCount; i++)
            {
                int threadStart = start + i * rangePerThread;
                int threadEnd = (i == threadCount - 1) ? end : threadStart + rangePerThread - 1;

                Log($"Starting thread {i + 1} for range {threadStart} to {threadEnd}.");
                Thread numberThread = new Thread(() => PrintNumbers(threadStart, threadEnd));
                numberThread.Start();
                thrs.Add(numberThread);
            }
            for (int i = 0; i < threadCount; i++)
                thrs[i].Join();
            
            Log("Task3Handler completed.");
            Console.ReadKey();
            ToStartWritePos();
        }

        private void Task4Handler()
        {
            Log("Task4Handler started - generating random numbers and calculating min, max, avg.");
            Random rand = new Random();
            int[] numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rand.Next(1, 10001);
            }

            Thread minThread = new(() => FindMinimum(numbers));
            Thread maxThread = new(() => FindMaximum(numbers));
            Thread avgThread = new(() => FindAverage(numbers));

            minThread.Start();
            maxThread.Start();
            avgThread.Start();

            minThread.Join();
            maxThread.Join();
            avgThread.Join();

            Log("Task4Handler completed.");
            Console.ReadKey();
            ToStartWritePos();
        }

        private void Task5Handler()
        {
            Log("Task5Handler started - generating random numbers, calculating min/max/avg, and saving results.");
            Random rand = new Random();
            int[] numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rand.Next(1, 10001);
            }

            int min = numbers.Min();
            int max = numbers.Max();
            double average = numbers.Average();

            Log($"Calculated values - Min: {min}, Max: {max}, Average: {average:F2}");

            Thread minThread = new(() => Log($"Минимум: {min}"));
            Thread maxThread = new(() => Log($"Максимум: {max}"));
            Thread avgThread = new(() => Log($"Среднее: {average}"));

            minThread.Start();
            maxThread.Start();
            avgThread.Start();

            Thread fileThread = new(() => SaveResultsToFile(numbers, min, max, average));
            fileThread.Start();
            Log("Task5Handler completed.");
        }

        private void ToStartWritePos()
        {
            Console.Clear();
            WindowDisplay.Show(showInput: false);
            Console.CursorTop = SizeY;
            Console.CursorLeft = 0;
        }

        private void SaveResultsToFile(int[] numbers, int min, int max, double average)
        {
            string filePath = "Results.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Набор чисел:");
                foreach (int number in numbers)
                {
                    writer.WriteLine(number);
                }
                writer.WriteLine();
                writer.WriteLine($"Минимум: {min}");
                writer.WriteLine($"Максимум: {max}");
                writer.WriteLine($"Среднее: {average:F2}");
            }
            Log($"Результаты сохранены в файл: {filePath}");
        }

        private void PrintNumbers(int start, int end)
        {
            Log($"Thread {Thread.CurrentThread.ManagedThreadId} printing numbers from {start} to {end}");
            for (int i = start; i <= end; i++)
            {
                Log($"Thread {Thread.CurrentThread.ManagedThreadId} printing {i}");
                Thread.Sleep(5);
            }
        }

        private void FindMinimum(int[] numbers)
        {
            int min = numbers.Min();
            Log($"Минимум: {min}");
        }

        private void FindMaximum(int[] numbers)
        {
            int max = numbers.Max();
            Log($"Максимум: {max}");
        }

        private void FindAverage(int[] numbers)
        {
            double average = numbers.Average();
            Log($"Среднее: {average}");
        }

        private void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [Thread {Thread.CurrentThread.ManagedThreadId}] {message}");
        }

        #endregion
    }
}
