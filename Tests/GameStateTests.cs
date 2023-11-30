using NUnit.Framework;
using Tictactoe;

namespace Tests;

public class GameStateTests
{
    GameState state;

    [SetUp]
    public void SetUp()
    {
        state = new GameState();
    }

    [Test]
    public void TestWonHorizontal()
    {
        state.Grid[0, 0] = Player.X;
        state.Grid[1, 0] = Player.X;
        state.Grid[2, 0] = Player.X;

        Assert.AreEqual(Player.X, state.HasSomeoneWon());
    }

    [Test]
    public void TestWonVertical()
    {
        state.Grid[0, 0] = Player.O;
        state.Grid[0, 1] = Player.O;
        state.Grid[0, 2] = Player.O;

        Assert.AreEqual(Player.O, state.HasSomeoneWon());
    }

    [Test]
    public void TestTopLeftDiagonal()
    {
        state.Grid[0, 0] = Player.X;
        state.Grid[1, 1] = Player.X;
        state.Grid[2, 2] = Player.X;

        Assert.AreEqual(Player.X, state.HasSomeoneWon());
    }

    [Test]
    public void TestTopRightDiagonal()
    {
        state.Grid[2, 0] = Player.X;
        state.Grid[1, 1] = Player.X;
        state.Grid[0, 2] = Player.X;

        Assert.AreEqual(Player.X, state.HasSomeoneWon());
    }

    [Test]
    public void TestLineBreak()
    {
        state.Grid[1, 1] = Player.X;
        state.Grid[1, 2] = Player.X;
        state.Grid[2, 0] = Player.X;

        Assert.AreEqual(null, state.HasSomeoneWon());
    }

    [Test]
    public void TestInterrupted()
    {
        state.Grid[0, 0] = Player.X;
        state.Grid[0, 1] = Player.O;
        state.Grid[0, 2] = Player.X;

        Assert.AreEqual(null, state.HasSomeoneWon());
    }
}