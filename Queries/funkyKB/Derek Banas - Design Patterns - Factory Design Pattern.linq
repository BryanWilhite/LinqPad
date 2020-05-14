<Query Kind="Program" />

void Main()
{
    // Create the factory object
    EnemyShipFactory shipFactory = new EnemyShipFactory();

    // Enemy ship object
    EnemyShip theEnemy = null;
    
    String typeOfShip = "B"; //What type of ship? (U / R / B)?
    theEnemy = shipFactory.makeEnemyShip(typeOfShip);
    DoStuffEnemy(theEnemy);
}

public static void DoStuffEnemy(EnemyShip anEnemyShip)
{
    anEnemyShip.displayEnemyShip();
    anEnemyShip.followHeroShip();
    anEnemyShip.enemyShipShoots();
}

/*
    Derek Banas: Design Patterns
    Factory Design Pattern
    [ 📖 http://www.newthinktank.com/2012/09/factory-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=ub0DXaeV6hA ]
*/

// This is a factory thats only job is creating ships
// By encapsulating ship creation, we only have one
// place to make modifications
public class EnemyShipFactory
{

    // This could be used as a static method if we
    // are willing to give up subclassing it
    public EnemyShip makeEnemyShip(String newShipType)
    {
        if (newShipType.Equals("U"))
        {
            return new UFOEnemyShip();
        }
        else if (newShipType.Equals("R"))
        {
            return new RocketEnemyShip();
        }
        else if (newShipType.Equals("B"))
        {
            return new BigUFOEnemyShip();
        }
        else throw new NotSupportedException($"`{nameof(newShipType)}: {newShipType}`");
    }
}

public abstract class EnemyShip
{

    private String name;
    private double amtDamage;

    public String getName() { return name; }
    public void setName(String newName) { name = newName; }

    public double getDamage() { return amtDamage; }
    public void setDamage(double newDamage) { amtDamage = newDamage; }

    public void followHeroShip()
    {
        Console.WriteLine(getName() + " is following the hero");
    }

    public void displayEnemyShip()
    {
        Console.WriteLine(getName() + " is on the screen");
    }

    public void enemyShipShoots()
    {
        Console.WriteLine(getName() + " attacks and does " + getDamage() + " damage to hero");
    }
}

public class RocketEnemyShip : EnemyShip
{

    public RocketEnemyShip()
    {
        setName("Rocket Enemy Ship");
        setDamage(10.0);
    }
}

public class UFOEnemyShip : EnemyShip
{
    public UFOEnemyShip()
    {
        setName("UFO Enemy Ship");
        setDamage(20.0);
    }
}

public class BigUFOEnemyShip : UFOEnemyShip
{
    public BigUFOEnemyShip()
    {
        setName("Big UFO Enemy Ship");
        setDamage(40.0);
    }
}
