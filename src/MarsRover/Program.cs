using MarsRover;

Console.WriteLine("Welcome to the Rover Controller (q to quit)");
var boundaryParser = new BoundaryParser();
var roverPosParser = new RoverPositionParser();
var roverCommadParser = new RoverCommandParser();

while (true)
{
    var boundary = GetBoundary(boundaryParser);
    if (boundary.quit) break;

    var rover1Pos = GetRoverPosition(roverPosParser, "rover 1");
    if (rover1Pos.quit) break;

    var rover1Commands = GetCommandSequence(roverCommadParser, "rover 1");
    if (rover1Commands.quit) break;

    var rover2Pos = GetRoverPosition(roverPosParser, "rover 2");
    if (rover2Pos.quit) break;

    var rover2Commands = GetCommandSequence(roverCommadParser, "rover 2");
    if (rover2Commands.quit) break;

    var commandHandler = new MarsRoverCommandHandler(boundary.boundary);
    var result1 = commandHandler.Execute(rover1Pos.roverPos, rover1Commands.commands);
    var result2 = commandHandler.Execute(rover2Pos.roverPos, rover2Commands.commands);
    Console.WriteLine($"{result1.Position.X} {result1.Position.Y} {result1.Heading}");
    Console.WriteLine($"{result2.Position.X} {result2.Position.Y} {result2.Heading}");

}

Console.WriteLine("Goodbye!");

bool GetShouldQuit(string input) => input?.ToUpper() is "Q";

(Boundary boundary, bool quit) GetBoundary(BoundaryParser parser)
{
    Console.Write("What is the current plateau rize? (ex: '5 5'):");
    var input = Console.ReadLine() ?? string.Empty;
    var shouldQuit = GetShouldQuit(input);
    return (shouldQuit 
        ? new Boundary(new(0, 0), new(0, 0)) 
        : parser.Parse(input), shouldQuit);
}

(RoverPosition roverPos, bool quit) GetRoverPosition(RoverPositionParser parser, string roverName)
{
    Console.Write($"What is the current state of {roverName}? (ex: '1 2 N'):");
    var input = Console.ReadLine() ?? string.Empty;
    var shouldQuit = GetShouldQuit(input);

    return (shouldQuit 
        ? new RoverPosition(CardinalDirection.None, new(0, 0)) 
        : parser.Parse(input), shouldQuit);
}

(RoverCommandSequence commands, bool quit) GetCommandSequence(RoverCommandParser parser, string roverName)
{
    Console.Write("What actions should rover 1 perform? (ex: 'LLM'):");
    var input = Console.ReadLine() ?? string.Empty;
    var shouldQuit = GetShouldQuit(input);

    return (shouldQuit 
        ? new RoverCommandSequence() 
        : parser.Parse(input), shouldQuit);
}