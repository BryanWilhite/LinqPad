<Query Kind="Statements" />

var text = @"
1.5/14 00:51 installation, white flags COMPTOIR COMMERCIAL DU SÉNÉGAL 
2.5/14 00:59 Justine Gaga “Indignation” COMPTOIR COMMERCIAL DU SÉNÉGAL 
3.5/14 still card: Justine Gaga COMPTOIR COMMERCIAL DU SÉNÉGAL 
4.5/14 still card: Faten Rouissi COMPTOIR COMMERCIAL DU SÉNÉGAL 
5.5/14 01:32 Faten Rouissi “Le fantôme de la liberté (Malla Ghassara)” COMPTOIR COMMERCIAL DU SÉNÉGAL 
6.5/14 still card: Radcliffe Bailey COMPTOIR COMMERCIAL DU SÉNÉGAL 
7.5/14 still Radcliffe Bailey “Storm at Sea” installation detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
8.5/14 still Radcliffe Bailey “Storm at Sea” installation frontal COMPTOIR COMMERCIAL DU SÉNÉGAL 
9.5/14 still Radcliffe Bailey “Storm at Sea” installation detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
10.5/14 still Radcliffe Bailey “Storm at Sea” installation detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
11.5/14 still card: Sidy Diallo COMPTOIR COMMERCIAL DU SÉNÉGAL 
12.5/14 still Sidy Diallo “Not for sale” COMPTOIR COMMERCIAL DU SÉNÉGAL 
13.5/14 still cards: Tam Joseph COMPTOIR COMMERCIAL DU SÉNÉGAL 
14.5/14 still Tam Joseph “Crows mate for life” COMPTOIR COMMERCIAL DU SÉNÉGAL 
15.5/14 still Tam Joseph “Laughing legend with Stratocaster” COMPTOIR COMMERCIAL DU SÉNÉGAL 
16.5/14 still Tam Joseph “Laughing legend with Stratocaster” detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
17.5/14 still Tam Joseph “Laughing legend with Stratocaster” detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
18.5/14 still card: Eric Pina COMPTOIR COMMERCIAL DU SÉNÉGAL 
19.5/14 still Eric Pina “L’announce, d’un soupir rouge” COMPTOIR COMMERCIAL DU SÉNÉGAL 
20.5/14 still card: Kamel Yahiaoui COMPTOIR COMMERCIAL DU SÉNÉGAL 
21.5/14 01:18 Kamel Yahiaoui “Les Poids des Ori” COMPTOIR COMMERCIAL DU SÉNÉGAL 
22.5/14 still card: Arlene Wandera COMPTOIR COMMERCIAL DU SÉNÉGAL 
23.5/14 04:10 Arlene Wandera “I’ve always wanted a (dolls) house” COMPTOIR COMMERCIAL DU SÉNÉGAL 
24.5/14 still Arlene Wandera “I’ve always wanted a (dolls) house” detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
25.5/14 still Arlene Wandera “I’ve always wanted a (dolls) house” detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
26.5/14 still Arlene Wandera “I’ve always wanted a (dolls) house” detail COMPTOIR COMMERCIAL DU SÉNÉGAL 
27.5/14 still card: Nomusa Makhubu COMPTOIR COMMERCIAL DU SÉNÉGAL 
28.5/14 00:32 Nomusa Makhubu “Self Portrait” COMPTOIR COMMERCIAL DU SÉNÉGAL 
29.5/14 00:43 Emeka Ogboh “LOSlantic” COMPTOIR COMMERCIAL DU SÉNÉGAL 
30.5/14 still card: Emeka Ogboh COMPTOIR COMMERCIAL DU SÉNÉGAL 
31.5/14 still card: Emeka Ogboh COMPTOIR COMMERCIAL DU SÉNÉGAL 
32.5/14 02:26 Dakar from another taxi
";

var data = text
    .Split('\n')
    .Where(i => i.Trim().Length > 0)
    .Select(i => i.Split('.').Last())
    .Where(i => i.Trim().Length > 0)
    ;

Func<int,string> getIndex = (index) =>
{
    var start = 165;
    var result = (start + index);
    if(result>178) result++;
    if(result>187) result++;
    if(result>189) result+=2;
    return result.ToString("P1000000: ");
};

data
    .Where(i => i.Contains("still"))
    .Select((i,index) => getIndex(index) + i)
    .Dump(@"SanDisk SDCX 64GB\DCIM\100_PANA");
