using NUnit.Framework;
using Tictactoe;

namespace Tests;

public class GameTests
{
    private Game game;
    private StringConsoleWrapper output;
    private Statistics statistics;

    [SetUp]
    public void Setup()
    {
        output = new StringConsoleWrapper();
        statistics = new Statistics();
        game = new Game(new Screen(output), new GameState(), statistics, new InputParser());
    }

    [Test]
    public void InitialGameState()
    {
        game.RunFrame("");

        Assert.AreEqual(
            """
             | | 
            -----
             | | 
            -----
             | | 
            Player X:
            """, output.ToString());
    }

    [Test]
    public void EnterRowAndColumn()
    {
        game.RunFrame("2:2");

        Assert.AreEqual(
            """
             | | 
            -----
             |x| 
            -----
             | | 
            Player O:
            """, output.ToString());
    }

    [Test]
    public void ErrorOnWrongInput()
    {
        game.RunFrame("4:3");

        Assert.AreEqual(
            """
             | | 
            -----
             | | 
            -----
             | | 
            row should be between 1 and 3
            Player X:
            """, output.ToString());
    }

    [Test]
    public void PlayerCanWin()
    {
        game.RunFrame("1:1"); //X
        game.RunFrame("2:1"); //O
        game.RunFrame("2:2"); //X
        game.RunFrame("3:1"); //O
        game.RunFrame("3:3"); //X

        Assert.AreEqual(
            """
            x| | 
            -----
            o|x| 
            -----
            o| |x
            X won. Press enter to start a new round

            """, output.ToString());

        game.RunFrame("");
        Assert.AreEqual(
            """
             | | 
            -----
             | | 
            -----
             | | 
            Player O:
            """, output.ToString());
    }

    [Test]
    public void DetectDraw()
    {
        game.RunFrame("1:1"); //X
        game.RunFrame("1:3"); //O
        game.RunFrame("1:2"); //X
        game.RunFrame("2:1"); //O
        game.RunFrame("2:2"); //X
        game.RunFrame("3:2"); //O
        game.RunFrame("2:3"); //X
        game.RunFrame("3:3"); //O
        game.RunFrame("3:1"); //X

        Assert.AreEqual(
            """
            x|x|o
            -----
            o|x|x
            -----
            x|o|o
            it's a draw. Press enter to start a new round

            """, output.ToString());

        game.RunFrame("");
        Assert.AreEqual(
            """
             | | 
            -----
             | | 
            -----
             | | 
            Player X:
            """, output.ToString());
    }

    [Test]
    public void ShowStats()
    {
        statistics.XWins = 1;
        statistics.YWins = 2;
        game.RunFrame("p");

        Assert.AreEqual(
            """
            Stats:
            X wins: 1
            Y wins: 2
            Press enter to continue playing

            """, output.ToString());
    }

    [Test]
    public void CloseGame()
    {
        Assert.IsTrue(game.RunFrame(""));
        // Game loop checks the return value.
        Assert.IsFalse(game.RunFrame("e"));
    }
}