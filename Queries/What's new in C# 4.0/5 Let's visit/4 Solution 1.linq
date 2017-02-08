<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

class ToXElementPersonVisitor
{
	public XElement DynamicVisit (Person p)  
	{
		return Visit ((dynamic)p);
	}
	
	XElement Visit (Person p)
	{
		return new XElement ("Person",
			new XAttribute ("Type", p.GetType().Name),
			new XElement ("FirstName", p.FirstName),
			new XElement ("LastName", p.LastName),
			p.Friends.Select (f => DynamicVisit (f))
		);
	}
	
	XElement Visit (Customer c)   // Specialized logic for customers
	{
		XElement xe = Visit ((Person)c);   // Call "base" method
		xe.Add (new XElement ("CreditLimit", c.CreditLimit));
		return xe;
	}
	
	XElement Visit (Employee e)   // Specialized logic for employees
	{
		XElement xe = Visit ((Person)e);   // Call "base" method
		xe.Add (new XElement ("Salary", e.Salary));
		return xe;
	}
}

void Main()
{
	var cust = new Customer { FirstName = "Joe", LastName = "Bloggs", CreditLimit = 123 };
	cust.Friends.Add (new Employee { FirstName = "Sue", LastName = "Brown", Salary = 50000 });
	new ToXElementPersonVisitor().DynamicVisit (cust).Dump();
}

class Person
{
	public string FirstName { get; set; }
	public string LastName  { get; set; }
	
	// The Friends collection may contain Customers & Employees:
	public readonly IList<Person> Friends = new Collection<Person> ();
}

class Customer : Person { public decimal CreditLimit { get; set; } }
class Employee : Person { public decimal Salary      { get; set; } }
