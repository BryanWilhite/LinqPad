<Query Kind="Program" />

void Main()
{
    // How you create a new instance of Singleton
    ScrabbleSingleton newInstance = ScrabbleSingleton.GetInstance();

    // Get unique id for instance object
    Debug.Print($"1st Instance ID: {newInstance.GetHashCode()}"); //see https://stackoverflow.com/a/4854325/22944

    // Get all of the letters stored in the List
    Debug.Print(newInstance.LetterList.Aggregate((a, i) => $"{a}, {i}"));

    List<String> playerOneTiles = newInstance.GetTiles(7);
    Debug.Print(string.Concat(Environment.NewLine, "Player 1: ",
        playerOneTiles.Aggregate((a, i) => $"{a}, {i}")));
    Debug.Print(newInstance.LetterList.Aggregate((a, i) => $"{a}, {i}"));

    // Try to make another instance of Singleton
    // This doesn't work because the constructor is private
    // ScrabbleSingleton instanceTwo = new ScrabbleSingleton();

    // Try getting a new instance using getInstance
    ScrabbleSingleton instanceTwo = ScrabbleSingleton.GetInstance();

    // Get unique id for the new instance object
    Debug.Print($"2nd Instance ID: {instanceTwo.GetHashCode()}");

    // This returns the value of the first instance created
    Debug.Print(instanceTwo.LetterList.Aggregate((a, i) => $"{a}, {i}"));

    // Player 2 draws 7 tiles
    List<String> playerTwoTiles = newInstance.GetTiles(7);
    Debug.Print(string.Concat(Environment.NewLine, "Player 2: ",
        playerTwoTiles.Aggregate((a, i) => $"{a}, {i}")));
    Debug.Print(newInstance.LetterList.Aggregate((a, i) => $"{a}, {i}"));
}

/*
    Derek Banas: Design Patterns
    Singleton Design Pattern
    [ 📖 http://www.newthinktank.com/2012/09/singleton-design-pattern-tutorial/ ]
    [ 📖 https://stackoverflow.com/questions/541194/c-sharp-version-of-javas-synchronized-keyword ]
    [ 📖 https://stackoverflow.com/a/4010615/22944 ]
    [ 📽 https://www.youtube.com/watch?v=NZaXM67fxbs ]
*/

public class ScrabbleSingleton
{
    private static ScrabbleSingleton firstInstance = null;

    // Used to slow down 1st thread
    private static bool firstThread = true;

    private static readonly object syncLock = new object();

    string[] scrabbleLetters = { "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "b", "b", "c", "c", "d", "d", "d", "d", "e", "e", "e", "e", "e",
            "e", "e", "e", "e", "e", "e", "e", "f", "f", "g", "g", "g", "h",
            "h", "i", "i", "i", "i", "i", "i", "i", "i", "i", "j", "k", "l",
            "l", "l", "l", "m", "m", "n", "n", "n", "n", "n", "n", "o", "o",
            "o", "o", "o", "o", "o", "o", "p", "p", "q", "r", "r", "r", "r",
            "r", "r", "s", "s", "s", "s", "t", "t", "t", "t", "t", "t", "u",
            "u", "u", "u", "v", "v", "w", "w", "x", "y", "y", "z", };

    private List<string> letterList;

    // Created to keep users from instantiation
    // Only Singleton will be able to instantiate this class
    private ScrabbleSingleton()
    {
        this.letterList = new List<string>(scrabbleLetters);
    }

    /*
        We could make `GetInstance` a synchronized method to force
        every thread to wait its turn. That way only one thread
        can access a method at a time. This can really slow everything
        down though…
    */
    public static ScrabbleSingleton GetInstance()
    {
        if (firstInstance == null)
        {
            // This is here to test what happens if threads try
            // to create instances of this class
            if (firstThread)
            {
                firstThread = false;
                try
                {
                    Thread.Sleep(1000);
                }
                catch (InvalidOperationException e)
                {
                    Debug.Print(e.StackTrace);
                }
            }

            // Here we just use synchronized when the first object
            // is created
            lock (syncLock)
            {
                if (firstInstance == null)
                {
                    // If the instance isn't needed it isn't created
                    // This is known as lazy instantiation
                    firstInstance = new ScrabbleSingleton();
                    // Shuffle the letters in the list
                    firstInstance.letterList.Shuffle();
                }
            }
        }

        // Under either circumstance this returns the instance
        return firstInstance;
    }

    public List<string> LetterList => firstInstance.letterList;

    public List<string> GetTiles(int howManyTiles)
    {
        // Tiles to be returned to the user
        List<String> tilesToSend = new List<string>();

        // Cycle through the List while adding the starting
        // Strings to the to be returned List while deleting
        // them from letterList
        for (int i = 0; i <= howManyTiles; i++)
        {
            tilesToSend.Add(firstInstance.letterList.First());
            firstInstance.letterList.RemoveAt(0);
        }

        return tilesToSend;
    }
}

public static class IListExtensions
{
    // also see: https://stackoverflow.com/a/5383519/22944
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = (new Random()).Next(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}