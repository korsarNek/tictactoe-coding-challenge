using NUnit.Framework;
using Tictactoe;

namespace Tests;

public class InputParserTests
{
    InputParser parser;

    [SetUp]
    public void SetUp()
    {
        parser = new InputParser();
    }

    [Test]
    public void TestInvalidFormat()
    {
        Assert.IsInstanceOf<ParseCoordinatesResult.PatternNotMatched>(parser.ParseCoordinates("22"));
    }

    [Test]
    public void TestNotNumbers()
    {
        Assert.IsInstanceOf<ParseCoordinatesResult.PatternNotMatched>(parser.ParseCoordinates("d:a"));
    }

    [Test]
    public void TestNegativeNumbers()
    {
        Assert.IsInstanceOf<ParseCoordinatesResult.PatternNotMatched>(parser.ParseCoordinates("-1:2"));
    }

    [Test]
    public void TestOutOfBounds()
    {
        Assert.IsInstanceOf<ParseCoordinatesResult.CoordinateOutOfRange>(parser.ParseCoordinates("4:1"));
        Assert.IsInstanceOf<ParseCoordinatesResult.CoordinateOutOfRange>(parser.ParseCoordinates("0:1"));
    }

    [Test]
    public void TestSuccess()
    {
        var result = parser.ParseCoordinates("2:3");
        Assert.IsInstanceOf<ParseCoordinatesResult.Success>(result);
        var success = (ParseCoordinatesResult.Success)result;

        Assert.AreEqual(1, success.Row);
        Assert.AreEqual(2, success.Column);
    }
}