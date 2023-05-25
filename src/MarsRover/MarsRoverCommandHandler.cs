namespace MarsRover;
public record Position(int X, int Y);
public record RoverCommandSequence(params RoverCommand[] Commands);
public record RoverPosition(CardinalDirection Heading, Position Position);
public record Plateau(Position BottomLeft, Position TopRight);

public enum RoverCommand
{
    None,
    MoveForward,
    RotateRight,
    RotateLeft
}

public enum CardinalDirection
{
    North, East, South, West
}

public interface ICommandRover
{
    RoverPosition Execute(RoverPosition startingPosition, RoverCommandSequence commandSequence);
}

public class MarsRoverCommandHandler : ICommandRover
{
    public RoverPosition Execute(RoverPosition startingPosition, RoverCommandSequence commandSequence)
    {
        var position = new RoverPosition(startingPosition.Heading, startingPosition.Position);
        foreach (var command in commandSequence.Commands)
        {
            position = HandleCommand(position, command);
        }
        return position;
    }

    private RoverPosition HandleCommand(RoverPosition startingPosition, RoverCommand command)
    {
        return command switch
        {
            RoverCommand.MoveForward => new RoverPosition(CardinalDirection.North, new(0, 1)),
            RoverCommand.RotateRight => new RoverPosition(CardinalDirection.East, startingPosition.Position),
            _ => new RoverPosition(CardinalDirection.North, new(0, 0))
        };
    }
}