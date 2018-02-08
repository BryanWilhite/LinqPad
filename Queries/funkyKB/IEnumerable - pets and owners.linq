<Query Kind="Program" />

void Main()
{
    var owners = new[]{
        new Owner{ Name = "Elon Musk" },
        new Owner{ Name = "Jeff Bezos" },
        new Owner{ Name = "Steve Ballmer" },
        new Owner{ Name = "Bill Gross" },
    };

    var pets = new[]{
        new Pet { Name = "Sparky", Owner=owners[0] },
        new Pet { Name = "Bruiser", Owner=owners[2] },
        new Pet { Name = "Fido", Owner=owners[3] },
        new Pet { Name = "Sparkay", Owner=owners[1] },
        new Pet { Name = "Bruiser Deluxe", Owner=owners[1] },
        new Pet { Name = "Fido II", Owner=owners[1] },
    };

    //Get all pets owned by Jeff Bezos:
    var ownerName = owners[1].Name;

    pets
        .Where(i => i.Owner.Name == ownerName)
        .Dump($"pets owned by {ownerName}");
}

class Pet
{
    public string Name { get; set; }

    public Owner Owner { get; set; }
}

class Owner
{
    public string Name { get; set; }
}