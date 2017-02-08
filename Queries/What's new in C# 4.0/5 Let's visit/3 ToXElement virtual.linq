<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

class Person
{
	public string FirstName { get; set; }
	public string LastName  { get; set; }
	
	// The Friends collection may contain Customers & Employees:
	public readonly IList<Person> Friends = new Collection<Person> ();
	
	public virtual XElement ToXElement()
	{
		return new XElement ("Person",
			new XAttribute ("Type", GetType().Name),
			new XElement ("FirstName", FirstName),
			new XElement ("LastName", LastName),
			Friends.Select (f => f.ToXElement())
		);
	}
}

class Customer : Person
{
	public decimal CreditLimit { get; set; }
	
	public override XElement ToXElement()
	{
		XElement xe = base.ToXElement ();
		xe.Add (new XElement ("CreditLimit", CreditLimit));
		return xe;
	}
}

class Employee : Person
{
	public decimal Salary { get; set; } 
	
	public override XElement ToXElement()
	{
		XElement xe = base.ToXElement ();
		xe.Add (new XElement ("Salary", Salary));
		return xe;
	}
}

void Main()
{
	var cust = new Customer { FirstName = "Joe", LastName = "Bloggs", CreditLimit = 123 };
	cust.Friends.Add (new Employee { FirstName = "Sue", LastName = "Brown", Salary = 50000 });
	cust.ToXElement().Dump();
}
