using MarsRover;

Console.WriteLine("Welcome to the Rover Controller (q to quit)");
var input = string.Empty;
var boundaryParser = new BoundaryParser();
var roverPosParser = new RoverPositionParser();
var roverCommadParser = new RoverCommandParser();

//todo: needs a refac
while (true)
{
    Console.Write("What is the current plateau rize? (ex: '5 5'):");
    input = Console.ReadLine() ?? string.Empty;
    if (input?.ToUpper() is "Q") { break; }

    var boundary = boundaryParser.Parse(input);

    Console.Write("What is the current state of rover 1? (ex: '1 2 N'):");
    input = Console.ReadLine() ?? string.Empty;
    if (input?.ToUpper() is "Q") { break; }

    var rover1Pos = roverPosParser.Parse(input);

    Console.Write("What actions should rover 1 perform? (ex: 'LLM'):");
    input = Console.ReadLine() ?? string.Empty;
    if (input?.ToUpper() is "Q") { break; }

    var rover1Commands = roverCommadParser.Parse(input);

    Console.Write("What is the current state of rover 2? (ex: '1 2 N'):");
    input = Console.ReadLine() ?? string.Empty;
    if (input?.ToUpper() is "Q") { break; }

    var rover2Pos = roverPosParser.Parse(input);

    Console.Write("What actions should rover 2 perform? (ex: 'LLM'):");
    input = Console.ReadLine() ?? string.Empty;
    if (input?.ToUpper() is "Q") { break; }

    var rover2Commands = roverCommadParser.Parse(input);

    var commandHandler = new MarsRoverCommandHandler(boundary);
    var result1 = commandHandler.Execute(rover1Pos, rover1Commands);
    var result2 = commandHandler.Execute(rover2Pos, rover2Commands);
    Console.WriteLine($"{result1.Position.X} {result1.Position.Y} {result1.Heading}");
    Console.WriteLine($"{result2.Position.X} {result2.Position.Y} {result2.Heading}");

}

Console.WriteLine("Goodbye!");