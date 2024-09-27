
#nullable enable

namespace QuizTop.UI
{
    public static class WindowTools
    {
        public static char[] keysDown = new char[] { 's', 'ы' };
        public static char[] keysUp = new char[] { 'w', 'ц' };
        public static char[] keysSelect = new char[] { 'e', 'у' };

        public static bool IsKeyFromArray(char[] array, char ch)
        {
            foreach (int num in array)
                if (num == ch) return true; 

            return false;
        }

        public static bool IsKeyDown(char ch) => IsKeyFromArray(keysDown, ch);
        public static bool IsKeyUp(char ch) => IsKeyFromArray(keysUp, ch);
        public static bool IsKeySelect(char ch) => IsKeyFromArray(keysSelect, ch);


        public static void UpdateCursorPos(char input, ref int CursorCaseNow, int CountMethod)
        {
            if (IsKeyDown(input)) ++CursorCaseNow;
            else if (IsKeyUp(input)) --CursorCaseNow;

            CircleUpdateCursor(ref CursorCaseNow, 0, CountMethod);
        }

        public static void UpdateCursorPos(char input, ref WindowDisplay window, int CountMethod)
        {
            if (IsKeyDown(input)) ++window.CursorPosition;
            else if (IsKeyUp(input)) --window.CursorPosition;
            CircleUpdateCursor(ref window, 0, CountMethod);
        }

        public static void CircleUpdateCursor(ref int CursorPos, int minValue, int maxValueNoVkl)
        {
            if (CursorPos < minValue)
                CursorPos = maxValueNoVkl - 1;
            if (CursorPos < maxValueNoVkl)//так сделал компилятор и в этом что то есть, но всё таки не очень читаемо
                return;
            CursorPos = minValue;
        }

        public static void CircleUpdateCursor(ref WindowDisplay window, int minValue, int maxValueNoVkl)
        {
            if (window.CursorPosition < minValue)
                window.CursorPosition = maxValueNoVkl - 1;

            if (window.CursorPosition < maxValueNoVkl)
                return;

            window.CursorPosition = minValue;
        }

        
    }
}
