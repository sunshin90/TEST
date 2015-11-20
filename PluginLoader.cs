1 using AutoSharp.Plugins; 
2 using LeagueSharp; 
3 
 
4 namespace AutoSharp 
5 { 
6     public class PluginLoader 
7     { 
8         private static bool _loaded; 
9         public PluginLoader() 
10         { 
11             if (!_loaded) 
12             { 
13                 switch (ObjectManager.Player.ChampionName.ToLower()) 
14                 { 
15                     case "aatrox": 
16                         new Aatrox(); 
17                         _loaded = true; 
18                         break; 
19                     case "ahri": 
20                         new Ahri(); 
21                         _loaded = true; 
22                         break; 
23                     case "akali": 
24                         new Akali(); 
25                         _loaded = true; 
26                         break; 
27                     case "alistar": 
28                         new Alistar(); 
29                         _loaded = true; 
30                         break; 
31                     case "amumu": 
32                         new Amumu(); 
33                         _loaded = true; 
34                         break; 
35                     case "anivia": 
36                         new Anivia(); 
37                         _loaded = true; 
38                         break; 
39                     case "annie": 
40                         new Annie(); 
41                         _loaded = true; 
42                         break; 
43                     case "ashe": 
44                         new Ashe(); 
45                         _loaded = true; 
46                         break; 
47                     case "bard": 
48                         new Bard(); 
49                         _loaded = true; 
50                         break; 
51                     case "blitzcrank": 
52                         new Blitzcrank(); 
53                         _loaded = true; 
54                         break; 
55                     case "brand": 
56                         new Brand(); 
57                         _loaded = true; 
58                         break; 
59                     case "braum": 
60                         new Braum(); 
61                         _loaded = true; 
62                         break; 
63                     case "caitlyn": 
64                         new Caitlyn(); 
65                         _loaded = true; 
66                         break; 
67                     case "cassiopeia": 
68                         new Cassiopeia(); 
69                         _loaded = true; 
70                         break; 
71                     case "chogath": 
72                         new Chogath(); 
73                         _loaded = true; 
74                         break; 
75                     case "corki": 
76                         new Corki(); 
77                         _loaded = true; 
78                         break; 
79                     case "darius": 
80                         new Darius(); 
81                         _loaded = true; 
82                         break; 
83                     case "diana": 
84                         new Diana(); 
85                         _loaded = true; 
86                         break; 
87                     case "draven": 
88                         new Draven(); 
89                         _loaded = true; 
90                         break; 
91                     case "evelynn": 
92                         new Evelynn(); 
93                         _loaded = true; 
94                         break; 
95                     case "ezreal": 
96                         new Ezreal(); 
97                         _loaded = true; 
98                         break; 
99                     case "fiddlesticks": 
100                         new FiddleSticks(); 
101                         _loaded = true; 
102                         break; 
103                     case "fiora": 
104                         new Fiora(); 
105                         _loaded = true; 
106                         break; 
107                     case "fizz": 
108                         new Fizz(); 
109                         _loaded = true; 
110                         break; 
111                     case "galio": 
112                         new Galio(); 
113                         _loaded = true; 
114                         break; 
115                     case "gangplank": 
116                         new Gangplank(); 
117                         _loaded = true; 
118                         break; 
119                     case "garen": 
120                         new Garen(); 
121                         _loaded = true; 
122                         break; 
123                     case "gragas": 
124                         new Gragas(); 
125                         _loaded = true; 
126                         break; 
127                     case "graves": 
128                         new Graves(); 
129                         _loaded = true; 
130                         break; 
131                     case "heimerdinger": 
132                         new Heimerdinger(); 
133                         _loaded = true; 
134                         break; 
135                     case "irelia": 
136                         new Irelia(); 
137                         _loaded = true; 
138                         break; 
139                     case "kalista": 
140                         new Kalista(); 
141                         _loaded = true; 
142                         break; 
143                     case "karma": 
144                         new Karma(); 
145                         _loaded = true; 
146                         break; 
147                     case "karthus": 
148                         new Karthus(); 
149                         _loaded = true; 
150                         break; 
151                     case "katarina": 
152                         new Katarina(); 
153                         _loaded = true; 
154                         break; 
155                     case "kayle": 
156                         new Kayle(); 
157                         _loaded = true; 
158                         break; 
159                     case "kogmaw": 
160                         new Kayle(); 
161                         _loaded = true; 
162                         break; 
163                     case "leblanc": 
164                         new Leblanc(); 
165                         _loaded = true; 
166                         break; 
167                     case "leona": 
168                         new Leona(); 
169                         _loaded = true; 
170                         break; 
171                     case "lucian": 
172                         new Lucian(); 
173                         _loaded = true; 
174                         break; 
175                     case "lulu": 
176                         new Lulu(); 
177                         _loaded = true; 
178                         break; 
179                     case "lux": 
180                         new Lux(); 
181                         _loaded = true; 
182                         break; 
183                     case "malzahar": 
184                         new Malzahar(); 
185                         _loaded = true; 
186                         break; 
187                     case "masteryi": 
188                         new Masteryi(); 
189                         _loaded = true; 
190                         break; 
191                     case "morgana": 
192                         new Morgana(); 
193                         _loaded = true; 
194                         break; 
195                     case "nami": 
196                         new Nami(); 
197                         _loaded = true; 
198                         break; 
199                     case "nunu": 
200                         new Nunu(); 
201                         _loaded = true; 
202                         break; 
203                     case "poppy": 
204                         new Poppy(); 
205                         _loaded = true; 
206                         break; 
207                     case "riven": 
208                         new Riven(); 
209                         _loaded = true; 
210                         break; 
211                     case "shaco": 
212                         new Shaco(); 
213                         _loaded = true; 
214                         break; 
215                     case "sivir": 
216                         new Sivir(); 
217                         _loaded = true; 
218                         break; 
219                     case "skarner": 
220                         new Skarner(); 
221                         _loaded = true; 
222                         break; 
223                     case "sona": 
224                         new Sona(); 
225                         _loaded = true; 
226                         break; 
227                     case "soraka": 
228                         new Soraka(); 
229                         _loaded = true; 
230                         break; 
231                     case "taric": 
232                         new Taric(); 
233                         _loaded = true; 
234                         break; 
235                     case "teemo": 
236                         new Teemo(); 
237                         _loaded = true; 
238                         break; 
239                     case "thresh": 
240                         new Thresh(); 
241                         _loaded = true; 
242                         break; 
243                     case "tristana": 
244                         new Tristana(); 
245                         _loaded = true; 
246                         break; 
247                     case "vayne": 
248                         new Vayne(); 
249                         _loaded = true; 
250                         break; 
251                     case "veigar": 
252                         new Veigar(); 
253                         _loaded = true; 
254                         break; 
255                     case "vladimir": 
256                         new Vladimir(); 
257                         _loaded = true; 
258                         break; 
259                     case "warwick": 
260                         new Warwick(); 
261                         _loaded = true; 
262                         break; 
263                     case "xerath": 
264                         new Xerath(); 
265                         _loaded = true; 
266                         break; 
267                     case "zilean": 
268                         new Zilean(); 
269                         _loaded = true; 
270                         break; 
271                     case "zyra": 
272                         new Zyra(); 
273                         _loaded = true; 
274                         break; 
275                     default: 
276                         new Default(); 
277                         _loaded = true; 
278                         break; 
279                 } 
280             } 
281         } 
282 
 
283     } 
284 } 
