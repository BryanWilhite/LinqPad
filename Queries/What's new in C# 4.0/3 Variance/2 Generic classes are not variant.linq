<Query Kind="Program" />

class Animal { }
class Dog : Animal { }
class Cat : Animal { }

void Write (List<Animal> a)
{
	a.Dump();
}

void Main()
{
	Write (new List<Animal> { new Animal() } );	
	Write (new List<Cat>    { new Cat()    } );	
	Write (new List<Dog>    { new Dog()    } );	
}