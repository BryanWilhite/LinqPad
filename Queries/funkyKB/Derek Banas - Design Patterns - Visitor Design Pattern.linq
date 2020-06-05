<Query Kind="Program" />

void Main()
{
    TaxVisitor taxCalc = new TaxVisitor();
    TaxHolidayVisitor taxHolidayCalc = new TaxHolidayVisitor();

    Necessity milk = new Necessity(3.47);
    Liquor vodka = new Liquor(11.99);
    Tobacco cigars = new Tobacco(19.99);

    Console.WriteLine(milk.Accept(taxCalc));
    Console.WriteLine(vodka.Accept(taxCalc));
    Console.WriteLine(cigars.Accept(taxCalc));

    Console.WriteLine("\nTAX HOLIDAY PRICES");

    Console.WriteLine(milk.Accept(taxHolidayCalc));
    Console.WriteLine(vodka.Accept(taxHolidayCalc));
    Console.WriteLine(cigars.Accept(taxHolidayCalc));
}

/*
    Derek Banas: Design Patterns
    Visitor Design Pattern
    [ 📖 http://www.newthinktank.com/2012/11/visitor-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=pL4mOUDi54o ]

    What is the difference between Strategy pattern and Visitor Pattern?

    One Strategy for many classes; many visitors for many classes…

    [ 📖 https://stackoverflow.com/questions/8665295/what-is-the-difference-between-strategy-pattern-and-visitor-pattern ]
*/

// The visitor pattern is used when you have to perform
// the same action on many objects of different types

interface Visitor
{
    // Created to automatically use the right 
    // code based on the Object sent
    // Method Overloading

    double Visit(Liquor liquorItem);

    double Visit(Tobacco tobaccoItem);

    double Visit(Necessity necessityItem);
}

interface Visitable
{
    // Allows the Visitor to pass the object so
    // the right operations occur on the right 
    // type of object.

    // accept() is passed the same visitor object
    // but then the method visit() is called using 
    // the visitor object. The right version of visit()
    // is called because of method overloading

    double Accept(Visitor visitor);
}

class Liquor : Visitable
{
    private double price;

    public Liquor(double item)
    {
        price = item;
    }

    public double Accept(Visitor visitor)
    {
        return visitor.Visit(this);
    }

    public double GetPrice()
    {
        return price;
    }
}

class Necessity : Visitable
{
    private double price;

    public Necessity(double item)
    {
        price = item;
    }

    public double Accept(Visitor visitor)
    {
        return visitor.Visit(this);
    }

    public double GetPrice()
    {
        return price;
    }
}

class Tobacco : Visitable
{
    private double price;

    public Tobacco(double item)
    {
        price = item;
    }

    public double Accept(Visitor visitor)
    {
        return visitor.Visit(this);
    }

    public double GetPrice()
    {
        return price;
    }
}

// Concrete Visitor Class

class TaxVisitor : Visitor
{
    // This is created so that each item is sent to the
    // right version of visit() which is required by the
    // Visitor interface and defined below
    public TaxVisitor()
    {
    }

    // Calculates total price based on this being taxed
    // as a liquor item

    public double Visit(Liquor liquorItem)
    {
        Console.WriteLine("Liquor Item: Price with Tax");
        return Double.Parse($"{(liquorItem.GetPrice() * .18) + liquorItem.GetPrice():#.##}");
    }

    // Calculates total price based on this being taxed
    // as a tobacco item

    public double Visit(Tobacco tobaccoItem)
    {
        Console.WriteLine("Tobacco Item: Price with Tax");
        return Double.Parse($"{(tobaccoItem.GetPrice() * .32) + tobaccoItem.GetPrice():#.##}");
    }

    // Calculates total price based on this being taxed
    // as a necessity item

    public double Visit(Necessity necessityItem)
    {
        Console.WriteLine("Necessity Item: Price with Tax");
        return Double.Parse($"{necessityItem.GetPrice():#.##}");
    }
}

// Concrete Visitor Class
class TaxHolidayVisitor : Visitor
{
    // This is created so that each item is sent to the
    // right version of visit() which is required by the
    // Visitor interface and defined below
    public TaxHolidayVisitor()
    {
    }

    // Calculates total price based on this being taxed
    // as a liquor item
    public double Visit(Liquor liquorItem)
    {
        Console.WriteLine("Liquor Item: Price with Tax");
        return Double.Parse($"{(liquorItem.GetPrice() * .10) + liquorItem.GetPrice():#.##}");
    }

    // Calculates total price based on this being taxed
    // as a tobacco item
    public double Visit(Tobacco tobaccoItem)
    {
        Console.WriteLine("Tobacco Item: Price with Tax");
        return Double.Parse($"{(tobaccoItem.GetPrice() * .30) + tobaccoItem.GetPrice():#.##}");
    }

    // Calculates total price based on this being taxed
    // as a necessity item
    public double Visit(Necessity necessityItem)
    {
        Console.WriteLine("Necessity Item: Price with Tax");
        return Double.Parse($"{necessityItem.GetPrice():#.##}");
    }
}