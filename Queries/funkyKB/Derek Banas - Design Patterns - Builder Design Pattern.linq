<Query Kind="Program" />

void Main()
{
    // Get a RobotBuilder of type OldRobotBuilder
    RobotBuilder oldStyleRobot = new OldRobotBuilder();

    // Pass the OldRobotBuilder specification to the engineer
    RobotEngineer robotEngineer = new RobotEngineer(oldStyleRobot);

    // Tell the engineer to make the Robot using the specifications
    // of the OldRobotBuilder class
    robotEngineer.makeRobot();

    // The engineer returns the right robot based off of the spec
    // sent to it on line 11
    Robot firstRobot = robotEngineer.getRobot();

    Console.WriteLine("Robot Built");

    Console.WriteLine("Robot Head Type: " + firstRobot.getRobotHead());

    Console.WriteLine("Robot Torso Type: " + firstRobot.getRobotTorso());

    Console.WriteLine("Robot Arm Type: " + firstRobot.getRobotArms());

    Console.WriteLine("Robot Leg Type: " + firstRobot.getRobotLegs());
}

/*
    Derek Banas: Design Patterns
    Builder Design Pattern
    [ 📖 http://www.newthinktank.com/2012/09/builder-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=9XnsOpjclUg ]
*/

// This is the interface that will be returned from the builder
public interface RobotPlan
{
    void setRobotHead(String head);

    void setRobotTorso(String torso);

    void setRobotArms(String arms);

    void setRobotLegs(String legs);
}

// The concrete Robot class based on the RobotPlan interface

public class Robot : RobotPlan
{
    private String robotHead;
    private String robotTorso;
    private String robotArms;
    private String robotLegs;

    public void setRobotHead(String head)
    {
        robotHead = head;
    }

    public String getRobotHead() { return robotHead; }

    public void setRobotTorso(String torso)
    {
        robotTorso = torso;
    }

    public String getRobotTorso() { return robotTorso; }

    public void setRobotArms(String arms)
    {
        robotArms = arms;
    }

    public String getRobotArms() { return robotArms; }

    public void setRobotLegs(String legs)
    {
        robotLegs = legs;
    }

    public String getRobotLegs() { return robotLegs; }
}

// Defines the methods needed for creating parts 
// for the robot
public interface RobotBuilder
{
    void buildRobotHead();

    void buildRobotTorso();

    void buildRobotArms();

    void buildRobotLegs();

    Robot getRobot();
}

// The concrete builder class that assembles the parts 
// of the finished Robot object
public class OldRobotBuilder : RobotBuilder
{
    private Robot robot;

    public OldRobotBuilder()
    {
        this.robot = new Robot();
    }

    public void buildRobotHead()
    {
        robot.setRobotHead("Tin Head");
    }

    public void buildRobotTorso()
    {
        robot.setRobotTorso("Tin Torso");
    }

    public void buildRobotArms()
    {
        robot.setRobotArms("Blowtorch Arms");
    }

    public void buildRobotLegs()
    {
        robot.setRobotLegs("Rollar Skates");
    }

    public Robot getRobot()
    {
        return this.robot;
    }
}

// The director / engineer class creates a Robot using the
// builder interface that is defined (OldRobotBuilder)
public class RobotEngineer
{
    private RobotBuilder robotBuilder;

    // OldRobotBuilder specification is sent to the engineer
    public RobotEngineer(RobotBuilder robotBuilder)
    {
        this.robotBuilder = robotBuilder;
    }

    // Return the Robot made from the OldRobotBuilder spec
    public Robot getRobot()
    {
        return this.robotBuilder.getRobot();
    }

    // Execute the methods specific to the RobotBuilder 
    // that implements RobotBuilder (OldRobotBuilder)
    public void makeRobot()
    {
        this.robotBuilder.buildRobotHead();
        this.robotBuilder.buildRobotTorso();
        this.robotBuilder.buildRobotArms();
        this.robotBuilder.buildRobotLegs();
    }
}
