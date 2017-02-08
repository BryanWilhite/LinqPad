<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

abstract class PersonVisitor<T>
{
	public T DynamicVisit (Person p) { return Visit ((dynamic)p); }
	
	protected abstract T Visit (Person p);
	protected virtual T Visit (Customer c) { return Visit ((Person) c); }
	protected virtual T Visit (Employee e) { return Visit ((Person) e); }
}

class ToXElementPersonVisitor : PersonVisitor<XElement>
{
	protected override XElement Visit (Person p)
	{
		return new XElement ("Person",
			new XElement ("FirstName", p.FirstName),
			new XElement ("LastName", p.LastName),
			p.Friends.Select (f => DynamicVisit (f))
		);
	}
	
	protected override XElement Visit (Customer c)
	{
		XElement xe = base.Visit (c);
		xe.Add (new XElement ("CreditLimit", c.CreditLimit));
		return xe;
	}
	
	protected override XElement Visit (Employee e)
	{
		XElement xe = base.Visit (e);
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