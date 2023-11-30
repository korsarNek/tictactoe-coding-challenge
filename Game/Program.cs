using Tictactoe;

var screen = new Screen();
var state = new GameState();
var statistics = new Statistics();
var parser = new InputParser();

screen.Prompt = $"Player {state.ActivePlayer}:";
while (true)
{
    if (state.ActiveView == View.Game || state.ActiveView == View.EndGame)
    {
        screen.Start();
        screen.DrawGrid();

        for (int x = 0; x < state.Grid.GetLength(0); x++)
            for (int y = 0; y < state.Grid.GetLength(1); y++)
                if (state.Grid[x, y] == Player.X)
                    screen.DrawGlyph(x, y, 'x');
                else if (state.Grid[x, y] == Player.O)
                    screen.DrawGlyph(x, y, 'o');

        screen.Finish();
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Stats:");
        Console.WriteLine($"X wins: {statistics.XWins}");
        Console.WriteLine($"Y wins: {statistics.YWins}");
        Console.WriteLine("Press enter to continue playing");
    }

    //ReadLine is blocking.
    var line = Console.ReadLine();
    if (line == null)
        continue;
    else if (line == "e")
        Environment.Exit(0);
    else if (line == "p")
        state.ActiveView = View.Stats;
    else if (line == "")
    {
        if (state.ActiveView == View.Stats)
            state.ActiveView = View.Game;
        else if (state.ActiveView == View.EndGame)
        {
            state.Reset();
            state.ActiveView = View.Game;
        }
    }
    else if (state.ActiveView == View.Game)
    {
        var result = parser.ParseCoordinates(line);
        if (result is ParseCoordinatesResult.PatternNotMatched)
            screen.SetTransientMessage("invalid input");
        else if (result is ParseCoordinatesResult.CoordinateOutOfRange outOfRange)
            screen.SetTransientMessage($"{outOfRange.CoordinateName} should be between 1 and 3");
        else if (result is ParseCoordinatesResult.Success coords)
        {
            if (state.Grid[coords.X, coords.Y] != null)
                screen.SetTransientMessage("field is already taken");
            else
            {
                state.Grid[coords.X, coords.Y] = state.ActivePlayer;
                state.SwitchToOppositePlayer();
            }
        }
    }

    var winner = state.HasSomeoneWon();
    if (winner != null)
    {
        screen.SetTransientMessage($"{winner} won. Press enter to start a new round");
        screen.Prompt = "";
        statistics.AddWin(winner.Value);
        state.SwitchToOppositePlayer(winner.Value);
        state.ActiveView = View.EndGame;
    }
    else if (state.IsGridFull())
    {
        screen.SetTransientMessage("It's a draw. Press enter to start a new round");
        screen.Prompt = "";
        state.ActivePlayer = Player.X;
        state.ActiveView = View.EndGame;
    }
    else
        screen.Prompt = $"Player {state.ActivePlayer}:";
}