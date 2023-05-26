namespace MarsRover.Tests;

public class RoverPositionParserTests
{
    [Fact]
    public void Should_parse_rover_positions()
    {
        var inputString = "1 2 S";
        var expectedRoverPosition = new RoverPosition(CardinalDirection.South, new Position(1, 2));

        IParseInput<RoverPosition> roverPosParser = new RoverPositionParser();
        var result = roverPosParser.Parse(inputString);

        result.ShouldBe(expectedRoverPosition);
    }
}
