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

    [Fact]
    public void Should_rotate_right()
    {
        var startingPosition = new RoverPosition(CardinalDirection.North, new(0, 0));
        var commandSequence = new RoverCommandSequence(RoverCommand.RotateRight);
        var expectedPosition = new RoverPosition(CardinalDirection.East, new(0, 0));

        ICommandRover command = new MarsRoverCommandHandler();
        var result = command.Execute(startingPosition, commandSequence);

        result.ShouldBe(expectedPosition);
    }
}