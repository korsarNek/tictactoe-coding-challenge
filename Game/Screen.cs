using System.Diagnostics;

namespace Tictactoe
{
    class Screen
    {
        private string? transientMessage = null;

        public void SetTransientMessage(string message)
        {
            transientMessage = message;
        }

        public string Prompt { get; set; } = string.Empty;

        public void Start()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void DrawGrid()
        {
            Console.WriteLine(" | | ");
            Console.WriteLine("-----");
            Console.WriteLine(" | | ");
            Console.WriteLine("-----");
            Console.WriteLine(" | | ");
        }

        public void DrawGlyph(int gridX, int gridY, char glyph)
        {
            var (left, top) = Console.GetCursorPosition();
            Console.SetCursorPosition(gridX * 2, gridY * 2);
            Console.Write(glyph);
            Console.SetCursorPosition(left, top);
        }

        public void Finish()
        {
            if (!string.IsNullOrWhiteSpace(transientMessage))
                Console.WriteLine(transientMessage);
            transientMessage = null;

            if (!string.IsNullOrEmpty(Prompt))
                Console.Write(Prompt);

            Console.CursorVisible = true;
        }
    }
}