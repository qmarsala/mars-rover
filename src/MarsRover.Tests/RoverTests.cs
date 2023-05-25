namespace MarsRover.Tests;

public class RoverTests
{
    [Fact]
    public void Should_move_forward()
    {
        var startingPosition = new RoverPosition(CardinalDirection.North, new(0,0));
        var commandSequence = new RoverCommandSequence(RoverCommand.MoveForward);
        var expectedPosition = new RoverPosition(CardinalDirection.North, new(0,1));

        ICommandRover command = new MarsRoverCommandHandler();
        var result = command.Execute(startingPosition, commandSequence);

        result.ShouldBe(expectedPosition);
    }
}