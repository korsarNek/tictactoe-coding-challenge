namespace Tictactoe;

public class Game
{
    Screen screen;
    GameState state;
    Statistics statistics;
    InputParser parser;

    public Game(Screen screen, GameState state, Statistics statistics, InputParser parser)
    {
        this.screen = screen;
        this.state = state;
        this.statistics = statistics;
        this.parser = parser;
        screen.Prompt = $"Player {state.ActivePlayer}:";
    }

    /// <summary>
    /// Runs a single frame of the game, processing the given input.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Returns true if the game continues or false if it should exit.</returns>
    public bool RunFrame(string input)
    {
        if (input == "e")
            return false;
        else if (input == "p")
            state.ActiveView = View.Stats;
        else if (input == "")
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
            var result = parser.ParseCoordinates(input);
            if (result is ParseCoordinatesResult.PatternNotMatched)
                screen.SetTransientMessage("invalid input");
            else if (result is ParseCoordinatesResult.CoordinateOutOfRange outOfRange)
                screen.SetTransientMessage($"{outOfRange.CoordinateName} should be between 1 and 3");
            else if (result is ParseCoordinatesResult.Success coords)
            {
                if (state.Grid[coords.Column, coords.Row] != null)
                    screen.SetTransientMessage("field is already taken");
                else
                {
                    state.Grid[coords.Column, coords.Row] = state.ActivePlayer;
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
            screen.SetTransientMessage("it's a draw. Press enter to start a new round");
            screen.Prompt = "";
            state.ActivePlayer = Player.X;
            state.ActiveView = View.EndGame;
        }
        else
            screen.Prompt = $"Player {state.ActivePlayer}:";

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
            screen.Start();

            screen.WriteLine("Stats:");
            screen.WriteLine($"X wins: {statistics.XWins}");
            screen.WriteLine($"Y wins: {statistics.YWins}");
            screen.WriteLine("Press enter to continue playing");
            screen.Prompt = "";

            screen.Finish();
        }

        return true;
    }
}