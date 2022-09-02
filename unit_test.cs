using Xunit;
using ToyRobotChallenge;

public class testclass
{
    TRCommand testtrcommand = new TRCommand();
    TROperations testtroperations = new TROperations();
    ToyRobot testtoyrobot;
    [Fact]
    public void validateInputPlaceValid(){
        Assert.Equal("PLACE",testtroperations.validateInput("place,2,2,North").command);
        Assert.Equal("PLACE",testtroperations.validateInput("place 3 2 South").command);
    }
    [Fact]
    public void validateInputPlaceInvalid(){
        Assert.Equal("INVALID",testtroperations.validateInput("place,2,2,2").command);
        Assert.Equal("INVALID",testtroperations.validateInput("place,2").command);
        Assert.Equal("INVALID",testtroperations.validateInput("place,2...WEST").command);
        Assert.Equal("INVALID",testtroperations.validateInput("place,2,5").command);
    }
    [Fact]
    public void performCommandValid(){
        GlobalVars.TrList = new List<ToyRobot>();
        testtoyrobot = new ToyRobot(50,0,4,"South");
        GlobalVars.TrList.Add(testtoyrobot);
        testtrcommand.command = "PLACE";
        Assert.Equal("PLACE",testtroperations.performCommand(testtrcommand,testtoyrobot));
        testtrcommand.command = "MOVE";
        Assert.Equal("MOVE",testtroperations.performCommand(testtrcommand,testtoyrobot));
        testtrcommand.command = "INVALID";
        Assert.Equal("INVALID",testtroperations.performCommand(testtrcommand,testtoyrobot));
    }

}
