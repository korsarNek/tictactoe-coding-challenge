using System.Text.RegularExpressions;

namespace Tictactoe
{
    class InputParser
    {
        public ParseCoordinatesResult ParseCoordinates(string line)
        {
            var match = Regex.Match(line,  @"^(?<row>\d):(?<column>\d)$");
            if (!match.Success)
                return new ParseCoordinatesResult.PatternNotMatched();

            var row = int.Parse(match.Groups["row"].Value);
            var column = int.Parse(match.Groups["column"].Value);

            if (row < 1 || row > 3)
                return new ParseCoordinatesResult.CoordinateOutOfRange("row");
            if (column < 1 || column > 3)
                return new ParseCoordinatesResult.CoordinateOutOfRange("column");

            return new ParseCoordinatesResult.Success(column - 1, row - 1);
        }
    }

    // Long winded union as C# doesn't support discrimindated unions and I prefer it over the classical exceptions for their visibility.
    record ParseCoordinatesResult
    {
        public record PatternNotMatched() : ParseCoordinatesResult;
        public record CoordinateOutOfRange(string CoordinateName) : ParseCoordinatesResult();
        public record Success(int X, int Y) : ParseCoordinatesResult();

        private ParseCoordinatesResult() {}
    }
}