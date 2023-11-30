namespace Tictactoe;

public class Screen
{
    private string? transientMessage = null;
    private IConsoleWrapper console;

    public void SetTransientMessage(string message)
    {
        transientMessage = message;
    }

    public string Prompt { get; set; } = string.Empty;

    public Screen(IConsoleWrapper console)
    {
        this.console = console;
    }

    public void Start()
    {
        console.CursorVisible = false;
        console.Clear();
    }

    public void DrawGrid()
    {
        console.WriteLine(" | | ");
        console.WriteLine("-----");
        console.WriteLine(" | | ");
        console.WriteLine("-----");
        console.WriteLine(" | | ");
    }

    public void DrawGlyph(int gridX, int gridY, char glyph)
    {
        var (left, top) = console.GetCursorPosition();
        console.SetCursorPosition(gridX * 2, gridY * 2);
        console.Write(glyph);
        console.SetCursorPosition(left, top);
    }

    public void WriteLine(string line)
    {
        console.WriteLine(line);
    }

    public void Finish()
    {
        if (!string.IsNullOrWhiteSpace(transientMessage))
            console.WriteLine(transientMessage);
        transientMessage = null;

        if (!string.IsNullOrEmpty(Prompt))
            console.Write(Prompt);

        console.CursorVisible = true;
    }
}