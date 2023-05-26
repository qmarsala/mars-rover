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

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(null)]
    [InlineData(" 5  ")]
    public void Should_parse_boundary_outof_bad_input(string inputString)
    {
        var expectedBoundary = new Boundary(new(0, 0), new(0, 0));

        IParseInput<Boundary> boundaryParser = new BoundaryParser();
        var result = boundaryParser.Parse(inputString);

        result.ShouldBe(expectedBoundary);
    }
}
