<Query Kind="Program" />

class Animal { }
class Dog : Animal { }
class Cat : Animal { }

void Main()
{
	var cats = new List<Cat> { new Cat() };	
	var dogs = new List<Dog> { new Dog() };	
	
	Enumerable.Concat<Animal> (cats, dogs).Dump();
}