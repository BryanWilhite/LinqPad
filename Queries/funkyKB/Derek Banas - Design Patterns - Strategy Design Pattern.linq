<Query Kind="Program" />

void Main()
{
    Animal sparky = new Dog();
    Animal tweety = new Bird();

    Console.WriteLine("Dog: " + sparky.tryToFly());

    Console.WriteLine("Bird: " + tweety.tryToFly());

    // This allows dynamic changes for flyingType
    sparky.setFlyingAbility(new ItFlys());

    Console.WriteLine("Dog: " + sparky.tryToFly());
}

/*
    Derek Banas: Design Patterns
    Strategy Design Pattern
    [ 📖 http://www.newthinktank.com/2012/08/strategy-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=-NCgRD9-C6o ]
    
    What is the difference between Strategy pattern and Visitor Pattern?

    One Strategy for many classes; many visitors for many classes…

    [ 📖 https://stackoverflow.com/questions/8665295/what-is-the-difference-between-strategy-pattern-and-visitor-pattern ]
*/

public class Animal
{
    private String name;
    private double height;
    private int weight;
    private String favFood;
    private double speed;
    private String sound;

    // Instead of using an interface in a traditional way
    // we use an instance variable that is a subclass
    // of the Flys interface.

    // Animal doesn't care what flyingType does, it just
    // knows the behavior is available to its subclasses

    // This is known as Composition : Instead of inheriting
    // an ability through inheritance the class is composed
    // with Objects with the right ability

    // Composition allows you to change the capabilities of 
    // objects at run time!
    public Flys flyingType;

    // yes, kid: in C# these `get*`/`set*` pairs can be properties
    public void setName(String newName) { name = newName; }
    public String getName() { return name; }

    public void setHeight(double newHeight) { height = newHeight; }
    public double getHeight() { return height; }

    public void setWeight(int newWeight)
    {
        if (newWeight > 0)
        {
            weight = newWeight;
        }
        else
        {
            Console.WriteLine("Weight must be bigger than 0");
        }
    }

    public double getWeight() { return weight; }

    public void setFavFood(String newFavFood) { favFood = newFavFood; }
    public String getFavFood() { return favFood; }

    public void setSpeed(double newSpeed) { speed = newSpeed; }
    public double getSpeed() { return speed; }

    public void setSound(String newSound) { sound = newSound; }
    public String getSound() { return sound; }

    /* BAD
	* You don't want to add methods to the super class.
	* You need to separate what is different between subclasses
	* and the super class
	public void fly(){
		
		Console.WriteLine("I'm flying");
		
	}
	*/

    // Animal pushes off the responsibility for flying to flyingType
    public String tryToFly()
    {
        return flyingType.fly();
    }

    // If you want to be able to change the flyingType dynamically
    // add the following method
    public void setFlyingAbility(Flys newFlyType)
    {
        flyingType = newFlyType;
    }
}

public class Dog : Animal
{
    public void digHole()
    {

        Console.WriteLine("Dug a hole");

    }

    public Dog() : base()
    {
        setSound("Bark");

        // We set the Flys interface polymorphically
        // This sets the behavior as a non-flying Animal

        flyingType = new CantFly();
    }

    /* BAD
	* You could override the fly method, but we are breaking
	* the rule that we need to abstract what is different to 
	* the subclasses
	* 
	public void fly(){
		
		Console.WriteLine("I can't fly");
		
	}
	*/
}

public class Bird : Animal
{
    // The constructor initializes all objects
    public Bird() : base()
    {

        setSound("Tweet");

        // We set the Flys interface polymorphically
        // This sets the behavior as a non-flying Animal
        flyingType = new ItFlys();
    }
}

// The interface is implemented by many other
// subclasses that allow for many types of flying
// without effecting Animal, or Flys.

// Classes that implement new Flys interface
// subclasses can allow other classes to use
// that code eliminating code duplication

// I'm decoupling : encapsulating the concept that varies
public interface Flys
{
    String fly();
}

// Class used if the Animal can fly
class ItFlys : Flys
{
    public String fly()
    {
        return "Flying High";
    }
}

//Class used if the Animal can't fly
class CantFly : Flys
{
    public String fly()
    {
        return "I can't fly";
    }
}