<Query Kind="Program" />

void Main()
{
    Hoagie italian = new ItalianHoagie();
    italian.MakeSandwich(nameof(ItalianHoagie));
    
    Debug.Print(Environment.NewLine);

    Hoagie veggie = new VeggieHoagie();
    veggie.MakeSandwich(nameof(VeggieHoagie));
}

/*
    Derek Banas: Design Patterns
    Template Method Design Pattern
    [ 📽 https://www.youtube.com/watch?v=aR1B8MlwbRI ]
    C# version: https://github.com/marwie/Design-Patterns-in-Unity-Example/blob/master/UnityDesignPatternsExampleProject/Assets/_PATTERNS/Behavioral%20Patterns/Template%20Method%20Design%20Pattern/TemplateMethodDesignPattern.cs
*/

public abstract class Hoagie
{
    public void MakeSandwich(string sandwichName)
    {
        Debug.Print($"Making new `{sandwichName}` Sandwich");

        CutBun();

        if (CustomerWantsMeat())
        {
            AddMeat();
        }

        if (CustomerWantsCheese())
        {
            AddCheese();
        }

        if (CustomerWantsVegetables())
        {
            AddVegetables();
        }

        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }

        WrapTheHoagie();
    }

    protected abstract void AddMeat();
    protected abstract void AddCheese();
    protected abstract void AddVegetables();
    protected abstract void AddCondiments();

    protected virtual bool CustomerWantsMeat() { return true; } // << called Hook
    protected virtual bool CustomerWantsCheese() { return true; }
    protected virtual bool CustomerWantsVegetables() { return true; }
    protected virtual bool CustomerWantsCondiments() { return true; }

    protected void CutBun()
    {
        Debug.Print("Bun is Cut");
    }

    protected void WrapTheHoagie()
    {
        Debug.Print("Hoagie is wrapped.");
    }
}

public class ItalianHoagie : Hoagie
{
    protected override void AddMeat()
    {
        Debug.Print("Adding the Meat: Salami");
    }

    protected override void AddCheese()
    {
        Debug.Print("Adding the Cheese: Provolone");
    }

    protected override void AddVegetables()
    {
        Debug.Print("Adding the Vegetables: Tomatoes");
    }

    protected override void AddCondiments()
    {
        Debug.Print("Adding the Condiments: Vinegar");
    }
}

public class VeggieHoagie : Hoagie
{
    protected override void AddMeat()
    {
    }

    protected override void AddCheese()
    {
    }

    protected override void AddVegetables()
    {
        Debug.Print("Adding the Vegetables: Tomatoes");
    }

    protected override void AddCondiments()
    {
        Debug.Print("Adding the Condiments: Vinegar");
    }

    protected override bool CustomerWantsMeat() { return false; }
    protected override bool CustomerWantsCheese() { return false; }

}

