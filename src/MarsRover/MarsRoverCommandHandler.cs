namespace MarsRover;

public record Position(int X, int Y);
public record RoverCommandSequence(params RoverCommand[] Commands);
public record RoverPosition(CardinalDirection Heading, Position Position);
public record Boundary(Position BottomLeft, Position TopRight);

public enum RoverCommand
{
    None,
    MoveForward,
    RotateRight,
    RotateLeft
}

public enum CardinalDirection
{
    None,
    North, 
    East, 
    South, 
    West
}

public interface ICommandRover
{
    RoverPosition Execute(RoverPosition startingPosition, RoverCommandSequence commandSequence);
}

public interface IParseInput<T>
{
    T Parse(string input);
}

public class RoverPositionParser : IParseInput<RoverPosition>
{
    public RoverPosition Parse(string input)
    {
        var position = new PositionParser().Parse(input);
        var heading = input?
            .Trim()?
            .Split(" ")?
            .Where(s => !string.IsNullOrWhiteSpace(s))?
            .LastOrDefault()?
            .Select(c => c switch
            {
                'N' => CardinalDirection.North,
                'E' => CardinalDirection.East,
                'S' => CardinalDirection.South,
                'W' => CardinalDirection.West,
                _ => CardinalDirection.None
            })
            .FirstOrDefault() ?? CardinalDirection.None;
        return new RoverPosition(heading, position);
    }
}

public class PositionParser : IParseInput<Position>
{
    public Position Parse(string input)
    {
        var positionParts = input?
            .Trim()
            .ToUpper()
            .Split(" ")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(ParseNumber)
            .ToArray();
        var xy = positionParts?.Count() > 1
            ? positionParts
            : new[] { 0, 0 };
        return new(xy[0], xy[1]);
    }

    private int ParseNumber(string numberString) => int.TryParse(numberString, out var number) ? number : 0;
}

public class BoundaryParser : IParseInput<Boundary>
{
    public Boundary Parse(string input)
    {
        var pos = new PositionParser().Parse(input);
        return new Boundary(new(0, 0), pos);
    }
}

public class RoverCommandParser : IParseInput<RoverCommandSequence>
{
    public RoverCommandSequence Parse(string input)
    {
        var commands = input?
            .Trim()
            .ToUpper()
            .ToCharArray()
            .Select(c => c switch
            {
                'M' => RoverCommand.MoveForward,
                'R' => RoverCommand.RotateRight,
                'L' => RoverCommand.RotateLeft,
                _ => RoverCommand.None,
            })
            .Where(c => c is not RoverCommand.None)
            .ToArray() ?? Array.Empty<RoverCommand>();
        return new RoverCommandSequence(commands);
    }
}

public class MarsRoverCommandHandler : ICommandRover
{
    public Boundary Boundary { get; } = new Boundary(new(0, 0), new(0, 0));

    public MarsRoverCommandHandler(Boundary boundary)
    {
        Boundary = boundary;
    }

    public RoverPosition Execute(RoverPosition startingPosition, RoverCommandSequence commandSequence)
    {
        var position = new RoverPosition(startingPosition.Heading, startingPosition.Position);
        foreach (var command in commandSequence.Commands)
        {
            position = HandleCommand(position, command);
        }
        return position;
    }

    private RoverPosition HandleCommand(RoverPosition startingPosition, RoverCommand command) =>
        command switch
        {
            RoverCommand.MoveForward => MoveForward(startingPosition),
            RoverCommand.RotateRight => RotateRight(startingPosition),
            RoverCommand.RotateLeft => RotateLeft(startingPosition),
            _ => startingPosition
        };

    private RoverPosition MoveForward(RoverPosition currentPosition)
    {
        var potentialPosition =
            currentPosition.Heading switch
            {
                CardinalDirection.North => new RoverPosition(currentPosition.Heading,
                    new(currentPosition.Position.X, currentPosition.Position.Y + 1)),
                CardinalDirection.East => new RoverPosition(currentPosition.Heading,
                    new(currentPosition.Position.X + 1, currentPosition.Position.Y)),
                CardinalDirection.South => new RoverPosition(currentPosition.Heading,
                    new(currentPosition.Position.X, currentPosition.Position.Y - 1)),
                CardinalDirection.West => new RoverPosition(currentPosition.Heading,
                    new(currentPosition.Position.X - 1, currentPosition.Position.Y)),
                _ => new RoverPosition(currentPosition.Heading, currentPosition.Position)
            };

        return potentialPosition.Position.X >= Boundary.BottomLeft.X
            && potentialPosition.Position.X <= Boundary.TopRight.X
            && potentialPosition.Position.Y >= Boundary.BottomLeft.Y
            && potentialPosition.Position.Y <= Boundary.TopRight.Y
            ? potentialPosition
            : currentPosition;
    }

    private RoverPosition RotateRight(RoverPosition currentPosition) =>
        currentPosition.Heading switch
        {
            CardinalDirection.North => new RoverPosition(CardinalDirection.East, currentPosition.Position),
            CardinalDirection.East => new RoverPosition(CardinalDirection.South, currentPosition.Position),
            CardinalDirection.South => new RoverPosition(CardinalDirection.West, currentPosition.Position),
            CardinalDirection.West => new RoverPosition(CardinalDirection.North, currentPosition.Position),
            _ => new RoverPosition(currentPosition.Heading, currentPosition.Position)
        };

    private RoverPosition RotateLeft(RoverPosition currentPosition) =>
       currentPosition.Heading switch
       {
           CardinalDirection.North => new RoverPosition(CardinalDirection.West, currentPosition.Position),
           CardinalDirection.East => new RoverPosition(CardinalDirection.North, currentPosition.Position),
           CardinalDirection.South => new RoverPosition(CardinalDirection.East, currentPosition.Position),
           CardinalDirection.West => new RoverPosition(CardinalDirection.South, currentPosition.Position),
           _ => new RoverPosition(currentPosition.Heading, currentPosition.Position)
       };
}