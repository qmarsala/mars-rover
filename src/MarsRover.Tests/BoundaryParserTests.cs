namespace MarsRover.Tests;

public class BoundaryParserTests
{
    [Fact]
    public void Should_parse_boundary()
    {
        var inputString = "5 5";
        var expectedBoundary = new Boundary(new(0, 0), new(5, 5));

        IParseInput<Boundary> boundaryParser = new BoundaryParser();
        var result = boundaryParser.Parse(inputString);

        result.ShouldBe(expectedBoundary);
    }
}
