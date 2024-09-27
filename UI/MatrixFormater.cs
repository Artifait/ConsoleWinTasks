
using System.Runtime.CompilerServices;
using System.Text;

#nullable enable
namespace QuizTop.UI
{
    public static class MatrixFormater
    {
        public static string[] GetWindowMatrix(string[] strings, string title)
        {
            int count = Math.Max(MatrixFormater.GetMaxLength(strings), title.Length) + (title.Length % 2 == 0 ? 5 : 4);
            string[] windowMatrix = new string[strings.Length + 6];
            windowMatrix[0] = "+" + new string('-', count) + "+";
            windowMatrix[1] = "| " + new string(' ', count - 2) + " |";
            windowMatrix[2] = "| " + title.PadCenter(count - 2) + " |";
            windowMatrix[3] = "| " + new string(' ', count - 2) + " |";
            for (int index = 0; index < strings.Length; ++index)
                windowMatrix[index + 4] = "| " + strings[index].PadRight(count - 2) + " |";
            windowMatrix[strings.Length + 4] = "| " + new string(' ', count - 2) + " |";
            windowMatrix[strings.Length + 5] = "+" + new string('-', count) + "+";
            return windowMatrix;
        }

        public static string[] GetNumberedWindowMatrix(string title, string[] strings)
        {
            int count = Math.Max(MatrixFormater.GetMaxLength(strings), title.Length) + (title.Length % 2 == 0 ? 6 : 7);
            string[] numberedWindowMatrix = new string[strings.Length + 5];
            numberedWindowMatrix[0] = "+" + new string('-', count) + "+";
            numberedWindowMatrix[1] = "| " + title.PadCenter(count - 2) + " |";
            numberedWindowMatrix[2] = "| " + new string(' ', count - 2) + " |";
            for (int index1 = 0; index1 < strings.Length; ++index1)
            {
                string[] strArray = numberedWindowMatrix;
                int index2 = index1 + 3;
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                interpolatedStringHandler.AppendLiteral("|  ");
                interpolatedStringHandler.AppendFormatted<int>(index1 + 1);
                interpolatedStringHandler.AppendLiteral(") ");
                string str = interpolatedStringHandler.ToStringAndClear() + strings[index1].PadRight(count - 6) + " |";
                strArray[index2] = str;
            }
            numberedWindowMatrix[strings.Length + 3] = "| " + new string(' ', count - 2) + " |";
            numberedWindowMatrix[strings.Length + 4] = "+" + new string('-', count) + "+";
            return numberedWindowMatrix;
        }

        public static string[] GetWindowMatrix(string? title, string[]? options, Dictionary<string, string>? fields, bool numberedOptions = true)
        {
            int num1 = 2 * (string.IsNullOrEmpty(title) ? 0 : 1);
            int num2 = num1 + (num1 == 0 ? 2 : 1) * (fields == null || fields.Count == 0 ? 0 : 1);
            int num3 = num2 + (num2 == 0 ? 2 : 1) * (options == null || options.Length == 0 ? 0 : 1);
            if (num3 == 0)
                num3 = 1;
            int length = num3 + (2 + (options == null ? 0 : options.Length) + (fields == null ? 0 : fields.Count));
            int num4 = Math.Max(Math.Max(options == null || options.Length == 0 ? 0 : MatrixFormater.GetMaxLength(options), string.IsNullOrEmpty(title) ? 0 : title.Length), fields == null || fields.Count == 0 ? 0 : MatrixFormater.GetMaxLength(fields));
            int num5 = num4;
            int num6 = string.IsNullOrEmpty(title) ? num5 + (num4 % 2 == 0 ? 6 : 7) : num5 + (title.Length % 2 == 0 ? 6 : 7);
            string[] windowMatrix = new string[length];
            int num7 = 0;
            string[] strArray1 = windowMatrix;
            int index1 = num7;
            int num8 = index1 + 1;
            string str1 = "+" + new string('-', num6) + "+";
            strArray1[index1] = str1;
            if (!string.IsNullOrEmpty(title))
                windowMatrix[num8++] = "| " + title.PadCenter(num6 - 2) + " |";
            string[] strArray2 = windowMatrix;
            int index2 = num8;
            int num9 = index2 + 1;
            string str2 = "| " + new string(' ', num6 - 2) + " |";
            strArray2[index2] = str2;
            if (options != null && options.Length != 0)
            {
                for (int i = 0; i < options.Length; ++i)
                {
                    int index4 = num9++;
                    if (numberedOptions) windowMatrix[index4] = $"|  {i + 1}) " + options[i].PadRight(num6 - 6) + " |";
                    else windowMatrix[index4] = "|  " + options[i].PadRight(num6 - 4) + " |";
                }
                windowMatrix[num9++] = "| " + new string(' ', num6 - 2) + " |";
            }
            if (fields != null && fields.Count != 0)
            {
                for (int index5 = 0; index5 < fields.Count; ++index5)
                    windowMatrix[num9++] = ("|  " + fields.ElementAt<KeyValuePair<string, string>>(index5).Key + ": " + fields.ElementAt<KeyValuePair<string, string>>(index5).Value).PadRight(num6) + " |";
                windowMatrix[num9++] = "| " + new string(' ', num6 - 2) + " |";
            }
            string[] strArray4 = windowMatrix;
            int index6 = num9;
            int num10 = index6 + 1;
            string str4 = "+" + new string('-', num6) + "+";
            strArray4[index6] = str4;
            return windowMatrix;
        }

        public static string[] GetNumberedWindowMatrix(string title,string[] strings, Dictionary<string, string> fields)
        {
            if (fields.Count == 0 || fields == null)
                return GetNumberedWindowMatrix(title, strings);
            int num1 = Math.Max(Math.Max(GetMaxLength(strings), title.Length), GetMaxLength(fields)) + (title.Length % 2 == 0 ? 6 : 7);
            string[] numberedWindowMatrix = new string[strings.Length + fields.Count + 6];
            numberedWindowMatrix[0] = "+" + new string('-', num1) + "+";
            numberedWindowMatrix[1] = "| " + title.PadCenter(num1 - 2) + " |";
            numberedWindowMatrix[2] = "| " + new string(' ', num1 - 2) + " |";
            for (int index1 = 0; index1 < strings.Length; ++index1)
            {
                string[] strArray = numberedWindowMatrix;
                int index2 = index1 + 3;
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                interpolatedStringHandler.AppendLiteral("|  ");
                interpolatedStringHandler.AppendFormatted<int>(index1 + 1);
                interpolatedStringHandler.AppendLiteral(") ");
                string str = interpolatedStringHandler.ToStringAndClear() + strings[index1].PadRight(num1 - 6) + " |";
                strArray[index2] = str;
            }
            int num2 = strings.Length + 4;
            numberedWindowMatrix[num2 - 1] = "| " + new string(' ', num1 - 2) + " |";
            for (int index3 = 0; index3 < fields.Count; ++index3)
            {
                string[] strArray = numberedWindowMatrix;
                int index4 = num2 + index3;
                KeyValuePair<string, string> keyValuePair = fields.ElementAt<KeyValuePair<string, string>>(index3);
                string key = keyValuePair.Key;
                keyValuePair = fields.ElementAt<KeyValuePair<string, string>>(index3);
                string str1 = keyValuePair.Value;
                string str2 = ("|  " + key + ": " + str1).PadRight(num1) + " |";
                strArray[index4] = str2;
            }
            numberedWindowMatrix[num2 + fields.Count] = "| " + new string(' ', num1 - 2) + " |";
            numberedWindowMatrix[num2 + fields.Count + 1] = "+" + new string('-', num1) + "+";
            return numberedWindowMatrix;
        }

        public static char[,] GetWindowMatrixChar(string[] strings, string title) => StringToCharArrayMatrix(GetWindowMatrix(strings, title));
        public static char[,] GetWindowMatrixChar(string? title,string[]? options, Dictionary<string, string>? fields, bool numberedOptions = true) => StringToCharArrayMatrix(GetWindowMatrix(title, options, fields));

        public static string[] GetQuestionMatrix(string[] strings)
        {
            int num = GetMaxLength(strings) + 4;
            return new string[strings.Length + 6];
        }

        public static void СhangeLine(string[] strings, int indexLine, int StartPos, string? NewLine)
        {
            if (NewLine == null)
                return;
            char[] charArray = strings[indexLine].ToCharArray();
            int num = StartPos;
            foreach (char ch in NewLine)
                charArray[num++] = ch;
            string str = new StringBuilder(charArray.Length).Append(charArray).ToString();
            strings[indexLine] = str;
        }

        public static void ReWriteLine(string[] strings, int indexLine, string defoalt, string newLine)
        {
            strings[indexLine] = defoalt;
            СhangeLine(strings, indexLine, defoalt.IndexOf(':') + 2, newLine);
        }

        public static void ReWriteLine(string[] strings, string field, string newLine)
        {
            for (int indexLine = strings.Length - 3; indexLine >= 0; --indexLine)
            {
                if (strings[indexLine].IndexOf(field) == 3)
                {
                    СhangeLine(strings, indexLine, 3 + field.Length + 2, newLine);
                    break;
                }
            }
        }

        public static void ReplaceCharInString(ref string source, int index, char newSymb)
        {
            char[] charArray = source.ToCharArray();
            charArray[index] = newSymb;
            source = new string(charArray);
        }

        public static char[,] StringToCharArrayMatrix(string[] strings)
        {
            int length1 = strings.Length;
            int length2 = strings[0].Length;
            char[,] charArrayMatrix = new char[length1, length2];
            for (int index1 = 0; index1 < length1; ++index1)
            {
                for (int index2 = 0; index2 < length2; ++index2)
                    charArrayMatrix[index1, index2] = strings[index1][index2];
            }
            return charArrayMatrix;
        }

        public static int GetMaxLength(string[] strings) => strings != null && strings.Length != 0 ? strings.Max(s => s.Length) : 0;
        public static int GetMaxLength(Dictionary<string, string> strings) => strings.Max(s => s.Key.Length + s.Value.Length) + 2;

    }
}
