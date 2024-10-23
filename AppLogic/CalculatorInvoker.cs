using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWinTasks.AppLogic
{
    public class CalculatorInvoker
    {
        private MethodInfo? _addMethod;
        private MethodInfo? _subtractMethod;
        private MethodInfo? _multiplyMethod;
        private MethodInfo? _divideMethod;
        private MethodInfo? _powerMethod;
        private MethodInfo? _squareRootMethod;
        private MethodInfo? _absMethod;

        public CalculatorInvoker()
        {
            Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName("Calculator.dll"));

            Type? calculator = assembly.GetType("Calculator.Calculator");

            if (calculator != null)
            {
                _addMethod = calculator.GetMethod("Add", BindingFlags.Public | BindingFlags.Static);
                _subtractMethod = calculator.GetMethod("Subtract", BindingFlags.Public | BindingFlags.Static);
                _multiplyMethod = calculator.GetMethod("Multiply", BindingFlags.Public | BindingFlags.Static);
                _divideMethod = calculator.GetMethod("Divide", BindingFlags.Public | BindingFlags.Static);
                _powerMethod = calculator.GetMethod("Power", BindingFlags.Public | BindingFlags.Static);
                _squareRootMethod = calculator.GetMethod("SquareRoot", BindingFlags.Public | BindingFlags.Static);
                _absMethod = calculator.GetMethod("Abs", BindingFlags.Public | BindingFlags.Static);
            }
            else
            {
                throw new Exception("Тип Calculator не найден.");
            }
        }


        public double Add(double a, double b)
        {
            return (double)_addMethod?.Invoke(null, new object[] { a, b })!;
        }

        public double Subtract(double a, double b)
        {
            return (double)_subtractMethod?.Invoke(null, new object[] { a, b })!;
        }

        public double Multiply(double a, double b)
        {
            return (double)_multiplyMethod?.Invoke(null, new object[] { a, b })!;
        }

        public double Divide(double a, double b)
        {
            return (double)_divideMethod?.Invoke(null, new object[] { a, b })!;
        }

        public double Power(double a, double b)
        {
            return (double)_powerMethod?.Invoke(null, new object[] { a, b })!;
        }

        public double SquareRoot(double a)
        {
            return (double)_squareRootMethod?.Invoke(null, new object[] { a })!;
        }

        public double Abs(double a)
        {
            return (double)_absMethod?.Invoke(null, new object[] { a })!;
        }
    }

}
