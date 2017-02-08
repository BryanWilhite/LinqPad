<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

class Person
{
	public string FirstName { get; set; }
	public string LastName  { get; set; }
	
	// The Friends collection may contain Customers & Employees:
	public readonly IList<Person> Friends = new Collection<Person> ();
	
	public XElement ToXElement()
	{
		return new XElement ("Person",
			new XElement ("FirstName", FirstName),
			new XElement ("LastName", LastName),
			Friends.Select (f => f.ToXElement())
		);
	}
}

class Customer : Person { public decimal CreditLimit { get; set; } }
class Employee : Person { public decimal Salary      { get; set; } }

void Main()
{
	var cust = new Customer { FirstName = "Joe", LastName = "Bloggs", CreditLimit = 123 };
	cust.Friends.Add (new Employee { FirstName = "Sue", LastName = "Brown", Salary = 50000 });
	cust.ToXElement().Dump();
}
