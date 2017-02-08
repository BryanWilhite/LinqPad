<Query Kind="Program" />

class Animal { }

class Dog : Animal
{
	public void Woof() { Console.WriteLine ("woof!"); }
}

void Main()
{
	var x = new Dog();
}