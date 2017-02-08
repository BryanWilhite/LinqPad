<Query Kind="Program" />

class WaterFowl
{
	public void Walk() { "Waddle".Dump(); }
	public void Quack() { "Quack".Dump(); }
}

class Homeopathist
{
	public void Walk() { "Come visit me".Dump(); }
	public void Quack() { "Heal your shakras".Dump(); }
}

void Main()
{
	DoYourThing (new WaterFowl());
	DoYourThing (new Homeopathist());
}

void DoYourThing (dynamic duck)
{
	duck.Walk();
	duck.Quack();
}