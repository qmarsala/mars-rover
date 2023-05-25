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