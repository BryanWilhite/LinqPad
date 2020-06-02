<Query Kind="Program" />

void Main()
{
    SongComponent industrialMusic =
                new SongGroup("Industrial",
                        "is a style of experimental music that draws on transgressive and provocative themes");

    SongComponent heavyMetalMusic =
            new SongGroup("\nHeavy Metal",
                    "is a genre of rock that developed in the late 1960s, largely in the UK and in the US");

    SongComponent dubstepMusic =
            new SongGroup("\nDubstep",
                    "is a genre of electronic dance music that originated in South London, England");

    // Top level component that holds everything

    SongComponent everySong = new SongGroup("Song List", "Every Song Available");

    // Composite that holds individual groups of songs
    // This holds Songs plus a SongGroup with Songs

    everySong.Add(industrialMusic);

    industrialMusic.Add(new Song("Head Like a Hole", "NIN", 1990));
    industrialMusic.Add(new Song("Headhunter", "Front 242", 1988));

    industrialMusic.Add(dubstepMusic);

    dubstepMusic.Add(new Song("Centipede", "Knife Party", 2012));
    dubstepMusic.Add(new Song("Tetris", "Doctor P", 2011));

    // This is a SongGroup that just holds Songs

    everySong.Add(heavyMetalMusic);

    heavyMetalMusic.Add(new Song("War Pigs", "Black Sabath", 1970));
    heavyMetalMusic.Add(new Song("Ace of Spades", "Motorhead", 1980));

    DiscJockey crazyLarry = new DiscJockey(everySong);

    crazyLarry.GetSongList();
}

/*
    Derek Banas: Design Patterns
    Composite Design Pattern
    [ 📖 http://www.newthinktank.com/2012/10/composite-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=2HUnoKyC9l0 ]

    see also:
    [ 📖 https://blog.jonblankenship.com//2019/10/04/using-the-specification-pattern-to-build-a-data-driven-rules-engine/ ]
*/

// This acts as an interface for every Song (Leaf)
// and SongGroup (Composite) we create
public abstract class SongComponent
{

    // We throw InvalidOperationException so that if
    // it doesn't make sense for a song, or song group
    // to inherit a method they can just inherit the
    // default implementation

    // This allows me to add components
    public virtual void Add(SongComponent newSongComponent)
    {
        throw new InvalidOperationException();
    }

    // This allows me to remove components
    public virtual void Remove(SongComponent newSongComponent)
    {
        throw new InvalidOperationException();
    }

    // This allows me to get components
    public virtual SongComponent GetComponent(int componentIndex)
    {
        throw new InvalidOperationException();
    }

    // This allows me to retrieve song names
    public virtual String GetSongName()
    {
        throw new InvalidOperationException();
    }

    // This allows me to retrieve band names
    public virtual String GetBandName()
    {
        throw new InvalidOperationException();
    }

    // This allows me to retrieve release year
    public virtual int GetReleaseYear()
    {
        throw new InvalidOperationException();
    }

    // When this is called by a class object that extends
    // SongComponent it will print out information
    // specific to the Song or SongGroup
    public virtual void DisplaySongInfo()
    {
        throw new InvalidOperationException();
    }
}

public class SongGroup : SongComponent
{
    // Contains any Songs or SongGroups that are added
    // to this ArrayList

    List<SongComponent> songComponents = new List<SongComponent>();

    String groupName;
    String groupDescription;

    public SongGroup(String newGroupName, String newGroupDescription)
    {
        groupName = newGroupName;
        groupDescription = newGroupDescription;
    }

    public String getGroupName() { return groupName; }
    public String getGroupDescription() { return groupDescription; }

    public override void Add(SongComponent newSongComponent)
    {
        songComponents.Add(newSongComponent);
    }

    public override void Remove(SongComponent newSongComponent)
    {
        songComponents.Remove(newSongComponent);
    }

    public override SongComponent GetComponent(int componentIndex)
    {
        return (SongComponent)songComponents.ElementAt(componentIndex);
    }

    public override void DisplaySongInfo()
    {
        Console.WriteLine(
            string.Concat(getGroupName(),
            " ",
            getGroupDescription()
            ));

        // Cycles through and prints any Songs or SongGroups added
        // to this SongGroups ArrayList songComponents
        songComponents.ForEach(songInfo => songInfo.DisplaySongInfo());
    }
}

public class Song : SongComponent
{
    String songName;
    String bandName;
    int releaseYear;

    public Song(String newSongName, String newBandName, int newReleaseYear)
    {
        songName = newSongName;
        bandName = newBandName;
        releaseYear = newReleaseYear;
    }

    public override String GetSongName() { return songName; }
    public override String GetBandName() { return bandName; }
    public override int GetReleaseYear() { return releaseYear; }

    public override void DisplaySongInfo()
    {
        Console.WriteLine($"{GetSongName()} was recorded by {GetBandName()} in {GetReleaseYear()}");
    }
}

public class DiscJockey
{

    SongComponent songList;

    // newSongList contains every Song, SongGroup,
    // and any Songs saved in SongGroups
    public DiscJockey(SongComponent newSongList)
    {
        songList = newSongList;
    }

    // Calls the displaySongInfo() on every Song
    // or SongGroup stored in songList
    public void GetSongList()
    {
        songList.DisplaySongInfo();
    }
}
