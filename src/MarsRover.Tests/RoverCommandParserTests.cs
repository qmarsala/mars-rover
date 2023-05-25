using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests;

public class RoverCommandParserTests
{
    [Fact]
    public void Should_parse_out_rover_commands()
    {
        var inputString = "MLR";
        var expectedCommandSequence = new RoverCommandSequence(RoverCommand.MoveForward, RoverCommand.RotateLeft, RoverCommand.RotateRight);

        IParseInput<RoverCommandSequence> parser = new RoverCommandParser();
        var result = parser.Parse(inputString);

        result.Commands[0].ShouldBe(expectedCommandSequence.Commands[0]);
        result.Commands[1].ShouldBe(expectedCommandSequence.Commands[1]);
        result.Commands[2].ShouldBe(expectedCommandSequence.Commands[2]);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Should_parse_bad_input(string inputString)
    {
        var expectedCommandSequence = new RoverCommandSequence(RoverCommand.MoveForward, RoverCommand.RotateLeft, RoverCommand.RotateRight);

        IParseInput<RoverCommandSequence> parser = new RoverCommandParser();
        var result = parser.Parse(inputString);

        result.Commands.Length.ShouldBe(0);
    }

    [Fact]
    public void Should_parse_goodinput_outof_badinput()
    {
        var expectedCommandSequence = new RoverCommandSequence(RoverCommand.MoveForward, RoverCommand.RotateRight);
        var inputString = " M R ";

        IParseInput<RoverCommandSequence> parser = new RoverCommandParser();
        var result = parser.Parse(inputString);

        result.Commands[0].ShouldBe(expectedCommandSequence.Commands[0]);
        result.Commands[1].ShouldBe(expectedCommandSequence.Commands[1]);
    }
}
