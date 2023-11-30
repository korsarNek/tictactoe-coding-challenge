namespace Tictactoe;

// Needed a wrapper interface, because while the console supports redirecting the output to a different stream to use it for tests,
// it doesn't support the cursor. So I included a separate wrapper that supports the cursor.
// I also didn't want to rewrite the rendering of the game to work without the cursor.
public interface IConsoleWrapper
{
    public void WriteLine(string text);

    public void Write(string text);

    public void Write(char text);

    public void Clear();

    public bool CursorVisible { set; }

    public void SetCursorPosition(int left, int top);

    public (int left, int top) GetCursorPosition();
}

public class ConsoleWrapper : IConsoleWrapper
{
    public bool CursorVisible { set => Console.CursorVisible = value; }

    public void Clear()
    {
        Console.Clear();
    }

    public (int left, int top) GetCursorPosition()
    {
        return Console.GetCursorPosition();
    }

    public void SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
    }

    public void Write(string text)
    {
        Console.Write(text);
    }

    public void Write(char text)
    {
        Console.Write(text);
    }

    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}