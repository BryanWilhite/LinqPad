<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

class Person
{
	public string FirstName { get; set; }
	public string LastName  { get; set; }
	
	// The Friends collection may contain Customers & Employees:
	public readonly IList<Person> Friends = new Collection<Person> ();
}

class Customer : Person { public decimal CreditLimit { get; set; } }
class Employee : Person { public decimal Salary      { get; set; } }


void Main() { }
