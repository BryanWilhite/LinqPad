<Query Kind="Program" />

void Main()
{
	Base b1 = new LevelOne();
	nameof(b1).Dump();
	b1.PropertyOne.Dump();
	b1.PropertyTwo.Dump();

	var b2 = new LevelOne();
	string.Concat(Environment.NewLine, nameof(b2)).Dump();
	b2.PropertyOne.Dump();
	b2.PropertyTwo.Dump();

	LevelOne b2_5 = new LevelTwo();
	string.Concat(Environment.NewLine, nameof(b2_5)).Dump();
	b2_5.PropertyOne.Dump();
	b2_5.PropertyTwo.Dump(); // i cannot explain this dump

	var b3 = new LevelTwo();
	string.Concat(Environment.NewLine, nameof(b3)).Dump();
	b3.PropertyOne.Dump();
	b3.PropertyTwo.Dump();
}

class Base
{
	public string PropertyOne { get; set; }

	public virtual string PropertyTwo { get { return "base"; } set { } }
}

class LevelOne : Base
{
	public new string PropertyOne { get { return "LevelOne"; } set { } }

	public override string PropertyTwo { get { return "base-property-one"; } set { } }
}

class LevelTwo : LevelOne
{
	public override string PropertyTwo { get { return "base-property-two"; } set { } }
}