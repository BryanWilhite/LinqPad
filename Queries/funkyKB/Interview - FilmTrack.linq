<Query Kind="Program" />

void Main()
{
    /*
    
    Light Bulbs
    
    There are 100 light bulbs lined up in a row in a long room.
    Each bulb has its own switch and is currently switched off.
    The room has an entry door and an exit door.
    There are 100 people lined up outside the entry door.
    Each bulb is numbered consecutively from 1 to 100.
    
    So is each person.
    Person No. 1 enters the room, switches on every bulb, and exits.
    Person No. 2 enters and flips the switch on every second bulb (turning off bulbs 2, 4, 6, ...).
    Person No. 3 enters and flips the switch on every third bulb (changing the state on bulbs 3, 6, 9, ...).
    
    This continues until all 100 people have passed through the room.
    
    How many of the light bulbs are illuminated after the 100th person has passed through the room?
    More specifically, which light bulbs are still illuminated, and why?
    
    Parameters:
    Please code the solution to the above problem in any language you see fit
    using any technologies you like.
    
    The solution should generalize the problem above to N persons and N lightbulbs.
    The end result should have a GUI to input the number of people and the number of light bulbs.
    It should output which light bulbs are on at the end and how many.
    You have up to two weeks to complete the exercise.
    Please keep in mind that this is a showcase of your development talents.
    The problem itself is not difficult on purpose.
    We will be looking for code organization, useful patterns,
    and robustness of the solution.
    
    If we like the solution:
    Please come prepared to show off your solution.
    We will ask you to debug and possibly extend the solution with us
    in a "code review" type of environment. Bring with you a laptop you'd like to work on.
    If you do not have one, we will provide a machine for you.
    
    */
    var room = new Room();
    room.SwitchWithAllPersons();
    room.LightBulbs.Dump("light bulbs");

}

class LightBulb
{
    public LightBulb(int ordinal, bool isOn)
    {
        this.Ordinal = ordinal;
        this.IsOn = isOn;
    }

    public int Ordinal { get; private set; }

    public bool IsOn { get; set; }
}

struct Person
{
    public Person(int ordinal, int moduloDivisor)
    {
        this.Ordinal = ordinal;
        this.ModuloDivisor = moduloDivisor;
    }

    public int Ordinal { get; private set; }

    public int ModuloDivisor { get; private set; }
}

class Room
{
    public Room(int numberOfLightBulbs = 90, int numberOfPersons = 9, bool bulbsOnByDefault = true)
    {
        this.LightBulbs = Enumerable.Range(1, numberOfLightBulbs)
            .Select(i => new LightBulb(i, bulbsOnByDefault))
            .ToArray();
        this.Persons = Enumerable.Range(1, numberOfPersons)
            .Select(i => new Person(i, i));
    }

    public LightBulb[] LightBulbs { get; private set; }

    public IEnumerable<Person> Persons { get; private set; }

    public void SwitchWithAllPersons()
    {
        foreach (var person in this.Persons.Skip(1))
        {
            var bulbs = this.LightBulbs
                .Where(i => ((i.Ordinal % person.Ordinal) == 0));
            foreach (var bulb in bulbs) bulb.IsOn = !bulb.IsOn;
        }
    }
}