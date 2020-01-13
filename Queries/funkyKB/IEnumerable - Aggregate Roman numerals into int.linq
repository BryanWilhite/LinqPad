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
};

var romanChars = "MDCXV".ToCharArray();

romanChars
	.Select((romanChar, index) => new { romanChar, index, length = romanChars.Length })
	.Aggregate(0, (aggregate, anon) =>
	 {
		 var nextIndex = anon.index + 1;

		 var currentN = set[anon.romanChar];
		 var nextN = (nextIndex < anon.length) ? set[romanChars[nextIndex]] : 0;

		 if (currentN < nextN) return aggregate - currentN;

		 return aggregate + currentN;
	 })
	.Dump();