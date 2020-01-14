<Query Kind="Statements">
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Songhay.Extensions</Namespace>
</Query>

var set = new Dictionary<char, int>
{
	{'I', 1},
	{'V', 5},
	{'X', 10},
	{'L', 50},
	{'C', 100},
	{'D', 500},
	{'M', 1000},
	{'.', 0},
};

var romanNumerals = "CLXVI";

//technique 1: load Roman chars first to look ahead to next char
var romanChars = romanNumerals.ToCharArray();

romanChars
	.Select((romanChar, index) => new { romanChar, index, length = romanChars.Length })
	.Aggregate(0, (aggregate, anon) =>
	 {
		 var nextIndex = anon.index + 1;

		 var currentN = set[anon.romanChar];
		 var nextN = (nextIndex < anon.length) ? set[romanChars[nextIndex]] : 0;
		 new { currentN, nextN }.Dump();

		 if (currentN < nextN) return aggregate - currentN;

		 return aggregate + currentN;
	 })
	.Dump("technique 1");

//technique 2: punctuate Roman chars to look back at previous char
$"{romanNumerals}."
	.ToCharArray()
	.SelectWithPrevious((previousChar, currentChar) => new { previousChar, currentChar })
	.Aggregate(0, (aggregate, anon) =>
	 {
		 var currentN = set[anon.previousChar];
		 var nextN = set[anon.currentChar];
		 new { currentN, nextN }.Dump();

		 if (nextN == 0) return aggregate + currentN;

		 if (currentN < nextN) return aggregate - currentN;

		 return aggregate + currentN;
	 })
	.Dump("technique 2");

//technique 3: change technique 2 to use the length of the string to see the end of the char array
romanNumerals
	.ToCharArray()
	.SelectWithPrevious((previousChar, currentChar) => new { previousChar, currentChar })
	.Select((anon, i) => //TODO: add index overload to SelectWithPrevious()
	new 
	{ 
		anon.previousChar,
		anon.currentChar,
		currentIndex = i
	})
	.Aggregate(0, (aggregate, anon) =>
	 {
		 var currentN = set[anon.previousChar];
		 var nextN = set[anon.currentChar];
		 new { currentN, nextN, anon.currentIndex }.Dump();

		 if (currentN < nextN) return aggregate - currentN;

		 if ((romanNumerals.Length - 2) == anon.currentIndex) aggregate += nextN;

		 return aggregate + currentN;
	 })
	.Dump("technique 3");

/*
	## Bibliography

	Loyce C. Gossage, Ed. D. (1972). _Business Mathematics_ p. 73

*/