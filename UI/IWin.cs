
using System;

#nullable enable
namespace QuizTop.UI
{
    public interface IWin
    {
        Type? ProgramOptionsType { get; }
        Type? ProgramFieldsType { get; }
        WindowDisplay WindowDisplay { get; set; }

        void Show();
        void InputHandler();

        int SizeX { get; }
        int SizeY { get; }
    }
}
