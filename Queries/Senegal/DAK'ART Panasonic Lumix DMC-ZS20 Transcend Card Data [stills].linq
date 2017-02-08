<Query Kind="Statements" />

var text = @"
1.5/6 4:40 shot out of Pullman hotel balcony
2.5/7 1:21 MLK Blvd mosque
3.5/7 still MLK Blvd mosque
4.5/7 still MLK Blvd traffic, palms ocean
5.5/7 still Universite Cheikh Anta Diop sign
6.5/7 00:00:12 Universite Cheikh Anta Diop chemistry classroom
7.5/7 still Universite Cheikh Anta Diop chemistry classroom
8.5/7 still Universite Cheikh Anta Diop chemistry classroom
9.5/7 01:39 Universite Cheikh Anta Diop chemistry classroom
10.5/7 still Universite Cheikh Anta Diop chemistry classroom, chalk board
11.5/7 00:50 Universite Cheikh Anta Diop lecture hall
12.5/7 still Margo Crawford and conference organizers, lunchtime
13.5/7 still Margo Crawford looking away
14.5/7 still Margo Crawford with group, lunchtime
15.5/7 still Koko and Richard Powell 
16.5/7 still Margo Crawford with group, lunchtime
17.5/7 00:45 Hotel Sokhamon and ocean
18.5/7 still Hotel Sokhamon interior
19.5/7 still Hotel Sokhamon interior
20.5/8 03:28 Dakar street from taxi with women speaking English in background
21.5/8 still Koko on Goree ferry
22.5/8 still tanker from Goree ferry
22.5/8 still tanker, docks, cranes from Goree ferry
23.5/8 2:13 tanker from Goree ferry
24.5/8 docks from Goree ferry
25.5/8 01:51 Dakar from Goree ferry
26.5/8 01:19 Goree from ferry
27.5/8 00:17 detail: ferry bow at dock 
28.5/8 still Goree Island street sign
29.5/8 still Goree Island wall and window in holding cell 
30.5/8 still Goree Island graffiti in holding cell 
31.5/8 still Goree Island window in holding cell 
32.5/8 still Goree Island dark corner in holding cell 
33.5/8 00:43 Goree Island windows in holding cell
34.5/8 still Goree Island windows in holding cell
35.5/8 00:35 Goree Island upper floor interior move to exterior 
36.5/8 00:58 Dakar coast, people on sandy beach
37.5/9 still chi-wara male, Galerie Antenna
38.5/9 still chi-wara male, card, Galerie Antenna 
39.5/9 still chi-wara male, Galerie Antenna 
40.5/9 still chi-wara male, Galerie Antenna 
41.5/9 still chi-wara male, Galerie Antenna 
42.5/9 still chi-wara male, detail, Galerie Antenna 
43.5/9 still chi-wara female, Galerie Antenna 
44.5/9 still chi-wara female, Galerie Antenna
45.5/9 still chi-wara female, Galerie Antenna
46.5/9 still chi-wara female, Galerie Antenna
47.5/9 still chi-wara small male, Galerie Antenna
48.5/9 still chi-wara small male, Galerie Antenna
49.5/9 still chi-wara female, Galerie Antenna
50.5/9 still chi-wara small male, Galerie Antenna
51.5/9 still chi-wara small male, Galerie Antenna
52.5/9 still chi-wara small male, Galerie Antenna
53.5/9 still MUSÉE THÉODORE MONOD interior
54.5/9 03:49 MUSÉE THÉODORE MONOD interior (shoot prohibited)
55.5/9 01:01 Galo Bopp (Senegal), “Mendicité précoce,” MUSÉE THÉODORE MONOD exterior
56.5/9 01:27 Gabirel Kemzou Malou (Senegal), “Transition,” MUSÉE THÉODORE MONOD exterior
57.5/9 still “Diversité Culturelle” signage MUSÉE THÉODORE MONOD exterior
58.5/9 still signage MUSÉE THÉODORE MONOD rear building interior
59.5/9 still signage MUSÉE THÉODORE MONOD rear building interior
60.5/9 01:11 Momar Seck “Emboutellage” 2013 MUSÉE THÉODORE MONOD rear building interior
61.5/9 00:13 preparation of exhibition, workers with scaffold MUSÉE THÉODORE MONOD rear building interior
62.5/9 01:17 painting [too much shake] MUSÉE THÉODORE MONOD rear building interior
63.5/9 still Fatma Benkirane “Sans titre 5” 2013 MUSÉE THÉODORE MONOD rear building interior
64.5/9 still Fatma Benkirane “Sans titre 5” (detail) 2013 MUSÉE THÉODORE MONOD rear building interior
65.5/9 still Fatma Benkirane “Sans titre 5” (detail) 2013 MUSÉE THÉODORE MONOD rear building interior
66.5/9 still  preparation of exhibition, blueprint on floor MUSÉE THÉODORE MONOD rear building interior
67.5/9 03:00 preparation of exhibition, artist preparing musical installation with Arduino [forced interview not flowing well] MUSÉE THÉODORE MONOD rear building interior
68.5/9 still [unidentified, colored sand in pots] MUSÉE THÉODORE MONOD rear building interior
69.5/9 still Jin Jiangbo & Hongli Zhao “Silk Road” 2014 MUSÉE THÉODORE MONOD rear building interior
70.5/9 01:38 Jin Jiangbo & Hongli Zhao “Silk Road” 2014 MUSÉE THÉODORE MONOD rear building interior
71.5/9 01:44 [unidentified sculpture, female figure over plastic waste, significant shake] INSTITUT FRANÇAIS exterior
72.5/9 00:53 [unidentified sculpture, log] INSTITUT FRANÇAIS exterior
73.5/9 00:20 [unidentified assemblage, shoot prohibited by artist] INSTITUT FRANÇAIS exterior
74.5/9 still photos curated by Luc daSilva INSTITUT FRANÇAIS exterior, restaurant
75.5/9 still photos curated by Luc daSilva INSTITUT FRANÇAIS exterior, restaurant
76.5/9 still photos curated by Luc daSilva, detail of Josephine Baker INSTITUT FRANÇAIS exterior, restaurant
77.5/9 still XARITUFOTO catalog INSTITUT FRANÇAIS exterior, restaurant
78.5/9 still XARITUFOTO catalog INSTITUT FRANÇAIS exterior, restaurant
79.5/9 still XARITUFOTO catalog INSTITUT FRANÇAIS exterior, restaurant
80.5/9 still XARITUFOTO catalog INSTITUT FRANÇAIS exterior, restaurant
81.5/9 still XARITUFOTO catalog INSTITUT FRANÇAIS exterior, restaurant
82.5/9 01:37 restaurant table and ceiling fan INSTITUT FRANÇAIS exterior, restaurant
83.5/9 still Hotel Baraka
84.5/9 still Hotel Baraka, restaurant menu
85.5/9 still Hotel Baraka
86.5/9 still Hotel Baraka
87.5/10 still MUSÉE THÉODORE MONOD exterior
88.5/10 still card: Gabirel Kemzou Malou MUSÉE THÉODORE MONOD exterior
89.5/10 still card: Galo Bopp MUSÉE THÉODORE MONOD exterior
90.5/10 01:56 Momar Seck (Switzerland) “Diversité singuliére et unité plurielle” MUSÉE THÉODORE MONOD exterior
91.5/10 01:00 Guibril Andre Diop (Senegal) “Le mythe paradoxal du fer” MUSÉE THÉODORE MONOD exterior
92.5/10 01:22 Ray Agbo (Ghana) “Gender Balance” MUSÉE THÉODORE MONOD exterior
93.5/10 01:46 Henry Sagna (Senega) “Témoins de notre temps” MUSÉE THÉODORE MONOD exterior
94.5/10 01:42 Paulin Momine (Cote D’Ivoire) “Lourdeur” MUSÉE THÉODORE MONOD exterior
95.5/10 01:33 Daniel Bamigbade (Cote D’Ivoire) “Grand Masque” MUSÉE THÉODORE MONOD exterior
96.5/10 02:33 Mamady Seydi (Senegal) “Prendre tout ce que l’on désire n’est-ce pas du vol? (proverbe peulh)” MUSÉE THÉODORE MONOD exterior
97.5/10 still card: Mamady Seydi MUSÉE THÉODORE MONOD exterior
98.5/10 02:08 Nayo Afi (Togo) “Stéle” MUSÉE THÉODORE MONOD exterior
99.5/10 01:28 Cheikh Diouf (Senegal) “Le Masque” MUSÉE THÉODORE MONOD exterior
100.5/10 01:40 Abdoulaye Diakite (Mali) “Le Nommo ou (dieu d’eau)” MUSÉE THÉODORE MONOD exterior
101.5/10 still Mamady Seydi (Senegal) “Prendre tout ce que l’on désire n’est-ce pas du vol? (proverbe peulh)” MUSÉE THÉODORE MONOD exterior
102.5/10 still “Diversité Culturelle” signage MUSÉE THÉODORE MONOD exterior
103.5/10 still “Diversité Culturelle” signage MUSÉE THÉODORE MONOD exterior
104.5/10 still card Momar Seck MUSÉE THÉODORE MONOD rear building interior
105.5/10 still card Momar Seck MUSÉE THÉODORE MONOD rear building interior
106.5/10 still card Fatma Benkirane MUSÉE THÉODORE MONOD rear building interior
107.5/10 still signature Fatma Benkirane MUSÉE THÉODORE MONOD rear building interior
108.5/10 still card Fatma Benkirane MUSÉE THÉODORE MONOD rear building interior
109.5/10 still card Jin Jiangbo & Hongli Zhao MUSÉE THÉODORE MONOD rear building interior
110.5/10 01:38 Jin Jiangbo & Hongli Zhao “Silk Road” detail MUSÉE THÉODORE MONOD rear building interior
111.5/10 still detail Jin Jiangbo & Hongli Zhao “Silk Road” detail MUSÉE THÉODORE MONOD rear building interior
112.5/10 still detail Jin Jiangbo & Hongli Zhao “Silk Road” detail MUSÉE THÉODORE MONOD rear building interior
113.5/10 still detail, vase Jin Jiangbo & Hongli Zhao “Silk Road” detail MUSÉE THÉODORE MONOD rear building interior
113.5/10 still detail Jin Jiangbo & Hongli Zhao “Silk Road” detail MUSÉE THÉODORE MONOD rear building interior
114.5/10 01:46 Richard V. S. Cayol “Grands Crus Appelation Controlée” MUSÉE THÉODORE MONOD rear building interior
115.5/10 still card: Ulrike Arnold MUSÉE THÉODORE MONOD rear building interior
116.5/10 still Ulrike Arnold “Popenguine” MUSÉE THÉODORE MONOD rear building interior
117.5/10 still Ulrike Arnold “Popenguine” detail MUSÉE THÉODORE MONOD rear building interior
118.5/10 still Ulrike Arnold “Popenguine” detail MUSÉE THÉODORE MONOD rear building interior
119.5/10 still card: Ulrike Arnold MUSÉE THÉODORE MONOD rear building interior
120.5/10 still Ulrike Arnold “Popenguine” MUSÉE THÉODORE MONOD rear building interior
121.5/10 still Ulrike Arnold “Popenguine” detail MUSÉE THÉODORE MONOD rear building interior
122.5/10 still Ulrike Arnold “Popenguine” detail MUSÉE THÉODORE MONOD rear building interior
123.5/10 01:34 Julien Grossmann “Tropical Race” MUSÉE THÉODORE MONOD rear building interior
124.5/10 still card: Thomas Zipp MUSÉE THÉODORE MONOD rear building interior
125.5/10 still Thomas Zip “A.B. Fetichism” MUSÉE THÉODORE MONOD rear building interior
126.5/10 still Thomas Zip “A.B. Fetichism” detail MUSÉE THÉODORE MONOD rear building interior
127.5/10 still Thomas Zip “A.B. Fetichism” detail MUSÉE THÉODORE MONOD rear building interior
128.5/10 still Sainsily Cayal detail MUSÉE THÉODORE MONOD rear building
129.5/10 still Sainsily Cayal full MUSÉE THÉODORE MONOD rear building
130.5/10 still Sainsily Cayal detail MUSÉE THÉODORE MONOD rear building
131.5/10 still Sainsily Cayal detail MUSÉE THÉODORE MONOD rear building
132.5/10 still Sainsily Cayal detail MUSÉE THÉODORE MONOD rear building
133.5/10 still Sainsily Cayal detail MUSÉE THÉODORE MONOD rear building
134.5/10 still Sainsily Cayal full MUSÉE THÉODORE MONOD rear building
135.5/10 still card: Daouda Ndiaye MUSÉE THÉODORE MONOD rear building interior
136.5/10 00:47 Daouda Ndiaye detail [glass reflections] MUSÉE THÉODORE MONOD rear building interior
137.5/10 00:45 Momar Seck “United Colors of Africa” [battery failure] MUSÉE THÉODORE MONOD rear building interior
138.5/10 still Cheikhou Ba full VILLA RACINE
139.5/10 still Cheikhou Ba full VILLA RACINE
140.5/10 still Cheikhou Ba full VILLA RACINE
141.5/10 still Cheikhou Ba full VILLA RACINE
142.5/10 still Cheikhou Ba detail VILLA RACINE
143.5/10 still Cheikhou Ba detail VILLA RACINE
144.5/10 still Cheikhou Ba full VILLA RACINE
145.5/10 still Cheikhou Ba full VILLA RACINE
146.5/10 still Cheikhou Ba full VILLA RACINE
147.5/10 still Cheikhou Ba full VILLA RACINE
148.5/10 still Cheikhou Ba full VILLA RACINE
149.5/10 still Cheikhou Ba detail VILLA RACINE
150.5/10 still Cheikhou Ba full VILLA RACINE
151.5/10 still Cheikhou Ba prose VILLA RACINE
152.5/10 still Cheikhou Ba full VILLA RACINE
153.5/10 still Cheikhou Ba full VILLA RACINE
154.5/10 still Cheikhou Ba detail VILLA RACINE
155.5/10 still Cheikhou Ba full VILLA RACINE
156.5/10 still Cheikhou Ba detail VILLA RACINE
157.5/10 still Cheikhou Ba full VILLA RACINE
158.5/10 still Cheikhou Ba full VILLA RACINE
159.5/10 still Cheikhou Ba full VILLA RACINE
160.5/10 still Cheikhou Ba detail VILLA RACINE
161.5/10 still Cheikhou Ba detail VILLA RACINE
162.5/10 still Cheikhou Ba detail VILLA RACINE
163.5/10 still Cheikhou Ba full VILLA RACINE
164.5/10 still Cheikhou Ba full VILLA RACINE
165.5/10 still Cheikhou Ba full VILLA RACINE
166.5/10 still Cheikhou Ba detail VILLA RACINE
167.5/10 still Cheikhou Ba detail VILLA RACINE
168.5/10 still Cheikhou Ba full VILLA RACINE
169.5/10 still Cheikhou Ba full VILLA RACINE
170.5/10 02:00 Cheikhou Ba sculpture VILLA RACINE
171.5/10 00:53 Cheikhou Ba sculpture VILLA RACINE
172.5/10 00:28 Cheikhou Ba sculpture VILLA RACINE
173.5/10 00:26 Cheikhou Ba sculpture VILLA RACINE
174.5/10 00:35 Cheikhou Ba sculpture VILLA RACINE
175.5/10 06:32 Cheikhou Ba interview VILLA RACINE
176.5/10 still VILLA RACINE brochure
177.5/10 still VILLA RACINE brochure
178.5/10 02:32 VILLA RACINE accommodations
179.5/10 01:35 VILLA RACINE accommodations
180.5/10 02:19 VILLA RACINE accommodations
181.5/10 02:53 VILLA RACINE accommodations
182.5/10 02:15 GALERIE ANTENNA traditional sculpture [shoot of festival paintings prohibited]
183.5/10 03:06 GALERIE ANTENNA traditional sculpture
184.5/11 still “Global Black Consciousness” banner HÔTEL SOKHAMON
185.5/11 still “Global Black Consciousness” lecture detail HÔTEL SOKHAMON
186.5/11 still “Global Black Consciousness” panelists detail HÔTEL SOKHAMON
187.5/11 still “Global Black Consciousness” Bingo Magazine slide HÔTEL SOKHAMON
188.5/11 still “Global Black Consciousness” Bingo Magazine slide HÔTEL SOKHAMON
189.5/11 still “Global Black Consciousness” Bingo Magazine slide HÔTEL SOKHAMON
190.5/11 still “Global Black Consciousness” Bingo Magazine slide HÔTEL SOKHAMON
191.5/11 02:43 “Global Black Consciousness” Richard Powell on John Biggers and Charles White HÔTEL SOKHAMON
192.5/11 01:30 “Global Black Consciousness” Mayo Okediji on John Biggers HÔTEL SOKHAMON
193.5/11 00:39 streets of Dakar on Sunday
194.5/12 04:55 Mamady Seydi, sculpture, LE TOUKOULEUR restaurant
195.5/12 00:55 Mamady Seydi, sculpture, LE TOUKOULEUR restaurant
196.5/12 still Barkinado Bocoum prose AGENCIE DE VOYAGES

197.5/12 still Sylvia Kummer prose AGENCIE DE VOYAGES
198.5/12 01:22 Sylvia Kummer parchment AGENCIE DE VOYAGES
199.5/12 01:36 Barkinado Bocoum painting AGENCIE DE VOYAGES
200.5/12 01:02 Sylvia Kummer installation AGENCIE DE VOYAGES
201.5/12 01:25 Sylvia Kummer assemblage/collage AGENCIE DE VOYAGES
202.5/12 00:15 Sylvia Kummer parchment AGENCIE DE VOYAGES
203.5/12 00:46 Sylvia Kummer parchment, circular pair AGENCIE DE VOYAGES
204.5/12 00:28 Sylvia Kummer parchment, circular AGENCIE DE VOYAGES
205.5/12 01:34 Sylvia Kummer installation AGENCIE DE VOYAGES
206.5/12 still Zinkpe and Tafsir GALERIE ARTE
207.5/12 still Zinkpe and Tafsir GALERIE ARTE
208.5/12 02:58 Zinkpe GALERIE ARTE
209.5/12 01:12 Zinkpe GALERIE ARTE
210.5/12 00:44 Zinkpe GALERIE ARTE
211.5/12 00:48 Zinkpe GALERIE ARTE
212.5/12 00:58 Zinkpe GALERIE ARTE
213.5/12 00:54 Zinkpe GALERIE ARTE
214.5/12 00:56 Zinkpe GALERIE ARTE
215.5/12 01:06 Zinkpe GALERIE ARTE
216.5/12 00:48 assistant desk GALERIE ARTE
217.5/12 01:52 Tchif GALERIE ARTE
218.5/12 01:50 Tchif GALERIE ARTE
219.5/12 01:49 Tchif GALERIE ARTE
220.5/12 01:17 Tchif GALERIE ARTE
221.5/12 00:29 artists in talks GALERIE ARTE
222.5/12 still DAK’ART 2014 catalog GALERIE NATIONALE D’ARTE
223.5/12 still Moustapha Dimé “La voix divine” GALERIE NATIONALE D’ARTE
224.5/12 00:51 Moustapha Dimé “La voix divine” and “Les lances croisées” GALERIE NATIONALE D’ARTE
225.5/12 00:48 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
226.5/12 00:32 Moustapha Dimé “Sans titre 2” GALERIE NATIONALE D’ARTE
227.5/12 still card: Moustapha Dimé GALERIE NATIONALE D’ARTE
228.5/12 still card: Moustapha Dimé GALERIE NATIONALE D’ARTE
229.5/12 01:03 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
230.5/12 01:26 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
231.5/12 00:35 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
232.5/12 00:51 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
233.5/12 still chronology Moustapha Dimé GALERIE NATIONALE D’ARTE
234.5/12 still chronology Moustapha Dimé GALERIE NATIONALE D’ARTE
235.5/12 00:31 Moustapha Dimé “Sans titre” 1994 GALERIE NATIONALE D’ARTE
236.5/12 00:33 Moustapha Dimé “L’envoi des oiseaux” 1994 GALERIE NATIONALE D’ARTE
237.5/12 00:31 Moustapha Dimé sculpture GALERIE NATIONALE D’ARTE
238.5/12 still card: Moustapha Dimé GALERIE NATIONALE D’ARTE
239.5/12 00:39 Moustapha Dimé “Hermaphrodite” 1997 GALERIE NATIONALE D’ARTE
240.5/12 00:56 Moustapha Dimé “Le bateau” 1995 GALERIE NATIONALE D’ARTE
241.5/12 00:44 Moustapha Dimé “Femme nue” 1991 GALERIE NATIONALE D’ARTE
242.5/12 02:42 Moustapha Dimé “La croix” 1995 GALERIE NATIONALE D’ARTE
243.5/12 01:41 Moustapha Dimé “Le cheval” 1993 GALERIE NATIONALE D’ARTE
244.5/12 01:26 Moustapha Dimé video GALERIE NATIONALE D’ARTE
245.5/12 00:20 Moustapha Dimé video GALERIE NATIONALE D’ARTE
246.5/12 00:28 Moustapha Dimé video GALERIE NATIONALE D’ARTE
247.5/12 00:41 Moustapha Dimé video GALERIE NATIONALE D’ARTE
248.5/12 01:37 Moustapha Dimé video GALERIE NATIONALE D’ARTE
249.5/12 04:16 Abdoulaye Konaté (Mali) textiles GALERIE LE MANÈGE
250.5/14 01:25 Gatien Dardenne, Oussama Hassan, Ali Brouz PASSAGE ALF LEYMOON’S
251.5/14 01:57 Ali Brouz, SKK [can’t read signature, missed card], Galien Dardenne PASSAGE ALF LEYMOON’S
252.5/14 00:53 Ahmed Kleig PASSAGE ALF LEYMOON’S
253.5/14 00:51 Ghassan Ali PASSAGE ALF LEYMOON’S
254.5/14 00:53 Agnès Theureau PASSAGE ALF LEYMOON’S
255.5/14 01:11 Agnès Theureau PASSAGE ALF LEYMOON’S
256.5/14 01:08 Agnès Theureau PASSAGE ALF LEYMOON’S
257.5/14 00:26 Agnès Theureau PASSAGE ALF LEYMOON’S
258.5/14 00:34 Awal Fawaz PASSAGE ALF LEYMOON’S
259.5/14 01:23 SKK [can’t read signature, missed card] PASSAGE ALF LEYMOON’S
260.5/14 still Agnès Theureau contact information PASSAGE ALF LEYMOON’S
261.5/14 still Agnès Theureau price sheet PASSAGE ALF LEYMOON’S
262.5/14 03:12 Mona Hajjar PASSAGE ALF LEYMOON’S
263.5/14 00:36 Gatien Dardenne PASSAGE ALF LEYMOON’S
264.5/14 00:30 Clémentine PASSAGE ALF LEYMOON’S
265.5/14 still Bryan Wilhite PASSAGE ALF LEYMOON’S
266.5/14 still Tafsir PASSAGE ALF LEYMOON’S
267.5/14 00:15 Nasrine Jafa PASSAGE ALF LEYMOON’S
268.5/14 00:24 Nasrine Jafa PASSAGE ALF LEYMOON’S
269.5/14 00:29 Mouna PASSAGE ALF LEYMOON’S
270.5/14 00:18 Mouna PASSAGE ALF LEYMOON’S
271.5/14 01:16 Soly Cissé “Warrior 3” VILLE DE DAKAR exterior court
272.5/14 01:04 Soly Cissé “Warrior 2” VILLE DE DAKAR exterior court
273.5/14 00:54 Soly Cissé “Warrior 1” VILLE DE DAKAR exterior court
274.5/14 00:33 Soly Cissé “Bear Man” VILLE DE DAKAR exterior court
275.5/14 01:10 Soly Cissé “Cobra” VILLE DE DAKAR exterior court
276.5/14 01:26 Soly Cissé “Guerriers” VILLE DE DAKAR exterior court
277.5/14 01:11 Soly Cissé “Bear Cub” VILLE DE DAKAR exterior court
278.5/14 00:31 Soly Cissé “Couple Sindax” VILLE DE DAKAR exterior court
279.5/14 00:47 Soly Cissé “Couple Fox” VILLE DE DAKAR exterior court
280.5/14 00:55 Soly Cissé “Ndoket” VILLE DE DAKAR exterior court
281.5/14 01:04 Soly Cissé “Sans Titre” VILLE DE DAKAR exterior court
282.5/14 00:47 Soly Cissé “The Motorcyclist” VILLE DE DAKAR exterior court
";

var data = text
    .Split('\n')
    .Where(i => i.Trim().Length > 0)
    .Select(i => i.Substring(i.IndexOf('.') + 1))
    .Where(i => i.Trim().Length > 0)
    ;

Func<int,string> getIndex = (index) =>
{
    var start = 5;
    var result = (start + index);
    if(result>7) result+=2;
    if(result>106) result++;
    if(result>134) result++;
    if(result>150) result++;
    if(result>158) result++;
    if(result>161) result++;
    return result.ToString("P1000000: ");
};

data
    .Where(i => i.Contains("still"))
    .Select((i,index) => getIndex(index) + i)
    .Dump(@"\Transcend SDXC 64GB\DCIM\100_PANA");