namespace MarsRover.Tests;

public class RoverTests
{
    [Theory]
    [InlineData(CardinalDirection.North, 0, 1)]
    [InlineData(CardinalDirection.East, 1, 0)]
    [InlineData(CardinalDirection.South, 0, -1)]
    [InlineData(CardinalDirection.West, -1, 0)]
    public void Should_move_forward(CardinalDirection expectedDirection, int expectedX, int expectedY)
    {
        var startingPosition = new RoverPosition(expectedDirection, new(0, 0));
        var commandSequence = new RoverCommandSequence(RoverCommand.MoveForward);
        var expectedPosition = new RoverPosition(expectedDirection, new(expectedX, expectedY));

        ICommandRover command = new MarsRoverCommandHandler();
        var result = command.Execute(startingPosition, commandSequence);

        result.ShouldBe(expectedPosition);
    }

    [Theory]
    [InlineData(CardinalDirection.North, RoverCommand.RotateRight, CardinalDirection.East)]
    [InlineData(CardinalDirection.East, RoverCommand.RotateRight, CardinalDirection.South)]
    [InlineData(CardinalDirection.South, RoverCommand.RotateRight, CardinalDirection.West)]
    [InlineData(CardinalDirection.West, RoverCommand.RotateRight, CardinalDirection.North)]
    [InlineData(CardinalDirection.North, RoverCommand.RotateLeft, CardinalDirection.West)]
    [InlineData(CardinalDirection.West, RoverCommand.RotateLeft, CardinalDirection.South)]
    [InlineData(CardinalDirection.South, RoverCommand.RotateLeft, CardinalDirection.East)]
    [InlineData(CardinalDirection.East, RoverCommand.RotateLeft, CardinalDirection.North)]
    public void Should_rotate(CardinalDirection startingHeading, RoverCommand roverCommand, CardinalDirection expectedHeading)
    {
        var startingPosition = new RoverPosition(startingHeading, new(1, 1));
        var commandSequence = new RoverCommandSequence(roverCommand);
        var expectedPosition = new RoverPosition(expectedHeading, new(1, 1));

        ICommandRover command = new MarsRoverCommandHandler();
        var result = command.Execute(startingPosition, commandSequence);

        result.ShouldBe(expectedPosition);
    }
}