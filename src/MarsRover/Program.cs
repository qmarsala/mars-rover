record Position(int X, int Y);
record RoverCommandSequence(string commandString);
record RoverPosition(CardinalDirection Heading, Position Position);
record Plateau(Position BottomLeft, Position TopRight);

enum CardinalDirection
{
    North, East, South, West
}

interface ICommandRover
{
    RoverPosition Execute(RoverPosition startingPosition, RoverCommandSequence commandSequence);
}
