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

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("     ")]
    public void Should_parse_badinput(string inputString)
    {
        var expectedRoverPosition = new RoverPosition(CardinalDirection.None, new Position(0, 0));

        IParseInput<RoverPosition> roverPosParser = new RoverPositionParser();
        var result = roverPosParser.Parse(inputString);

        result.ShouldBe(expectedRoverPosition);
    }

    [Fact]
    public void Should_parse_roverpositions_outof_badinput()
    {
        var inputString = "  2 3  S ";
        var expectedRoverPosition = new RoverPosition(CardinalDirection.South, new Position(2, 3));

        IParseInput<RoverPosition> roverPosParser = new RoverPositionParser();
        var result = roverPosParser.Parse(inputString);

        result.ShouldBe(expectedRoverPosition);
    }
}
