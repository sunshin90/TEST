1 using System; 
2 using System.Drawing; 
3 using AutoSharp.Utils; 
4 using LeagueSharp; 
5 using LeagueSharp.Common; 
6 
 
7 namespace AutoSharp 
8 { 
9 
 
10     #region 
11 
 
12     #endregion 
13 
 
14     /// <summary> 
15     ///     PluginBase class 
16     /// </summary> 
17     public abstract class PluginBase 
18     { 
19         /// <summary> 
20         ///     Init BaseClass 
21         /// </summary> 
22         protected PluginBase() 
23         { 
24             InitConfig(); 
25             InitOrbwalker(); 
26             InitPluginEvents(); 
27         } 
28 
 
29         /// <summary> 
30         ///     Champion Name 
31         /// </summary> 
32         public string ChampionName { get; set; } 
33 
 
34         /// <summary> 
35         ///     Orbwalker 
36         /// </summary> 
37         public MyOrbwalker.Orbwalker Orbwalker { get; set; } 
38 
 
39         /// <summary> 
40         ///     ActiveMode 
41         /// </summary> 
42         public static Orbwalking.OrbwalkingMode ActiveMode { get; set; } 
43 
 
44         /// <summary> 
45         ///     TargetSelector 
46         /// </summary> 
47         public TargetSelector TargetSelector { get; set; } 
48 
 
49         /// <summary> 
50         ///     ComboMode 
51         /// </summary> 
52         public bool ComboMode 
53         { 
54             get { return true; } 
55         } 
56 
 
57         /// <summary> 
58         ///     HarassMode 
59         /// </summary> 
60         public bool HarassMode 
61         { 
62             get { return false; } 
63         } 
64 
 
65         /// <summary> 
66         ///     HarassMana 
67         /// </summary> 
68         public bool HarassMana 
69         { 
70             get { return Player.ManaPercent > ConfigValue<Slider>("HarassMana").Value; } 
71         } 
72 
 
73         /// <summary> 
74         ///     UsePackets 
75         /// </summary> 
76         public bool UsePackets 
77         { 
78             get { return false; /* 4.21 ConfigValue<bool>("UsePackets"); */ } 
79         } 
80 
 
81         /// <summary> 
82         ///     Player Object 
83         /// </summary> 
84         public Obj_AI_Hero Player 
85         { 
86             get { return ObjectManager.Player; } 
87         } 
88 
 
89         /// <summary> 
90         ///     AttackRange 
91         /// </summary> 
92         public float AttackRange 
93         { 
94             get { return Orbwalking.GetRealAutoAttackRange(Target); } 
95         } 
96 
 
97         /// <summary> 
98         ///     Target 
99         /// </summary> 
100         public Obj_AI_Hero Target 
101         { 
102             get { return TargetSelector.GetTarget(1200, TargetSelector.DamageType.Magical); } 
103         } 
104 
 
105         /// <summary> 
106         ///     OrbwalkerTarget 
107         /// </summary> 
108         public AttackableUnit OrbwalkerTarget 
109         { 
110             get { return Orbwalker.GetTarget(); } 
111         } 
112 
 
113         /// <summary> 
114         ///     Q 
115         /// </summary> 
116         public Spell Q { get; set; } 
117 
 
118         /// <summary> 
119         ///     W 
120         /// </summary> 
121         public Spell W { get; set; } 
122 
 
123         /// <summary> 
124         ///     E 
125         /// </summary> 
126         public Spell E { get; set; } 
127 
 
128         /// <summary> 
129         ///     R 
130         /// </summary> 
131         public Spell R { get; set; } 
132 
 
133         /// <summary> 
134         ///     Config 
135         /// </summary> 
136         public static Menu Config { get; set; } 
137 
 
138         /// <summary> 
139         ///     ComboConfig 
140         /// </summary> 
141         public Menu ComboConfig { get; set; } 
142 
 
143         /// <summary> 
144         ///     HarassConfig 
145         /// </summary> 
146         public Menu HarassConfig { get; set; } 
147 
 
148         /// <summary> 
149         ///     MiscConfig 
150         /// </summary> 
151         public Menu MiscConfig { get; set; } 
152 
 
153         /// <summary> 
154         ///     ManaConfig 
155         /// </summary> 
156         public Menu ManaConfig { get; set; } 
157 
 
158         /// <summary> 
159         ///     DrawingConfig 
160         /// </summary> 
161         public Menu DrawingConfig { get; set; } 
162 
 
163         /// <summary> 
164         ///     InterruptConfig 
165         /// </summary> 
166         public Menu InterruptConfig { get; set; } 
167 
 
168         /// <summary> 
169         ///     ConfigValue 
170         /// </summary> 
171         /// <typeparam name="T">Type</typeparam> 
172         /// <param name="item">string</param> 
173         /// <remarks> 
174         ///     Helper for 
175         /// </remarks> 
176         /// <returns></returns> 
177         public T ConfigValue<T>(string item) 
178         { 
179             return Config.Item("autosharp." + ObjectManager.Player.ChampionName + "." + item).GetValue<T>(); 
180         } 
181 
 
182         /// <summary> 
183         ///     OnProcessPacket 
184         /// </summary> 
185         /// <remarks> 
186         ///     override to Implement OnProcessPacket logic 
187         /// </remarks> 
188         /// <param name="args"></param> 
189         public virtual void OnProcessPacket(GamePacketEventArgs args) { } 
190 
 
191         /// <summary> 
192         ///     OnSendPacket 
193         /// </summary> 
194         /// <remarks> 
195         ///     override to Implement OnSendPacket logic 
196         /// </remarks> 
197         /// <param name="args"></param> 
198         public virtual void OnSendPacket(GamePacketEventArgs args) { } 
199 
 
200         /// <summary> 
201         ///     OnPossibleToInterrupt 
202         /// </summary> 
203         /// <remarks> 
204         ///     override to Implement SpellsInterrupt logic 
205         /// </remarks> 
206         /// <param name="unit">Obj_AI_Base</param> 
207         /// <param name="spell">InterruptableSpell</param> 
208         public virtual void OnPossibleToInterrupt(Obj_AI_Hero unit, Interrupter2.InterruptableTargetEventArgs spell) { } 
209 
 
210         /// <summary> 
211         ///     OnEnemyGapcloser 
212         /// </summary> 
213         /// <remarks> 
214         ///     override to Implement AntiGapcloser logic 
215         /// </remarks> 
216         /// <param name="gapcloser">ActiveGapcloser</param> 
217         public virtual void OnEnemyGapcloser(ActiveGapcloser gapcloser) { } 
218 
 
219         /// <summary> 
220         ///     OnUpdate 
221         /// </summary> 
222         /// <remarks> 
223         ///     override to Implement Update logic 
224         /// </remarks> 
225         /// <param name="args">EventArgs</param> 
226         public virtual void OnUpdate(EventArgs args) { } 
227 
 
228         /// <summary> 
229         ///     OnBeforeAttack 
230         /// </summary> 
231         /// <remarks> 
232         ///     override to Implement OnBeforeAttack logic 
233         /// </remarks> 
234         /// <param name="args">Orbwalking.BeforeAttackEventArgs</param> 
235         public virtual void OnBeforeAttack(Orbwalking.BeforeAttackEventArgs args) { } 
236 
 
237         /// <summary> 
238         ///     OnAfterAttack 
239         /// </summary> 
240         /// <remarks> 
241         ///     override to Implement OnAfterAttack logic 
242         /// </remarks> 
243         /// <param name="unit">unit</param> 
244         /// <param name="target">target</param> 
245         public virtual void OnAfterAttack(AttackableUnit unit, AttackableUnit target) { } 
246 
 
247         /// <summary> 
248         ///     OnLoad 
249         /// </summary> 
250         /// <remarks> 
251         ///     override to Implement class Initialization 
252         /// </remarks> 
253         /// <param name="args">EventArgs</param> 
254         public virtual void OnLoad(EventArgs args) { } 
255 
 
256         /// <summary> 
257         ///     OnDraw 
258         /// </summary> 
259         /// <remarks> 
260         ///     override to Implement Drawing 
261         /// </remarks> 
262         /// <param name="args">EventArgs</param> 
263         public virtual void OnDraw(EventArgs args) { } 
264 
 
265         /// <summary> 
266         ///     ComboMenu 
267         /// </summary> 
268         /// <remarks> 
269         ///     override to Implement ComboMenu Config 
270         /// </remarks> 
271         /// <param name="config">Menu</param> 
272         public virtual void ComboMenu(Menu config) { } 
273 
 
274         /// <summary> 
275         ///     HarassMenu 
276         /// </summary> 
277         /// <remarks> 
278         ///     override to Implement HarassMenu Config 
279         /// </remarks> 
280         /// <param name="config">Menu</param> 
281         public virtual void HarassMenu(Menu config) { } 
282 
 
283         /// <summary> 
284         ///     ManaMenu 
285         /// </summary> 
286         /// <remarks> 
287         ///     override to Implement ManaMenu Config 
288         /// </remarks> 
289         /// <param name="config">Menu</param> 
290         public virtual void ManaMenu(Menu config) { } 
291 
 
292         /// <summary> 
293         ///     MiscMenu 
294         /// </summary> 
295         /// <remarks> 
296         ///     override to Implement MiscMenu Config 
297         /// </remarks> 
298         /// <param name="config">Menu</param> 
299         public virtual void MiscMenu(Menu config) { } 
300 
 
301         /// <summary> 
302         ///     MiscMenu 
303         /// </summary> 
304         /// <remarks> 
305         ///     override to Implement Interrupt Config 
306         /// </remarks> 
307         /// <param name="config">Menu</param> 
308         public virtual void InterruptMenu(Menu config) { } 
309 
 
310         /// <summary> 
311         ///     DrawingMenu 
312         /// </summary> 
313         /// <remarks> 
314         ///     override to Implement DrawingMenu Config 
315         /// </remarks> 
316         /// <param name="config">Menu</param> 
317         public virtual void DrawingMenu(Menu config) { } 
318 
 
319         #region Private Stuff 
320 
 
321         /// <summary> 
322         ///     PluginEvents Initialization 
323         /// </summary> 
324         private void InitPluginEvents() 
325         { 
326             Game.OnUpdate += OnUpdate; 
327             Drawing.OnDraw += OnDraw; 
328             Orbwalking.BeforeAttack += OnBeforeAttack; 
329             Orbwalking.AfterAttack += OnAfterAttack; 
330             AntiGapcloser.OnEnemyGapcloser += OnEnemyGapcloser; 
331             Interrupter2.OnInterruptableTarget += OnPossibleToInterrupt; 
332             //Game.OnGameSendPacket += OnSendPacket; 
333             //Game.OnGameProcessPacket += OnProcessPacket; 
334             OnLoad(new EventArgs()); 
335         } 
336 
 
337         /// <summary> 
338         ///     Config Initialization 
339         /// </summary> 
340         private void InitConfig() 
341         { 
342             Config = Program.Config; 
343             TargetSelector.AddToMenu(Config.AddSubMenu(new Menu("Target Selector", "Target Selector"))); 
344 
 
345             ComboConfig = Config.AddSubMenu(new Menu("Combo", "Combo")); 
346             HarassConfig = Config.AddSubMenu(new Menu("Harass", "Harass")); 
347             ManaConfig = Config.AddSubMenu(new Menu("Mana Limiter", "Mana Limiter")); 
348             MiscConfig = Config.AddSubMenu(new Menu("Misc", "Misc")); 
349             InterruptConfig = Config.AddSubMenu(new Menu("Interrupt", "Interrupt")); 
350             DrawingConfig = Config.AddSubMenu(new Menu("Drawings", "Drawings")); 
351 
 
352             // mana 
353             ManaConfig.AddSlider("HarassMana", "Harass Mana %", 1, 1, 100); 
354 
 
355             // misc 
356             MiscConfig.AddList("AttackMinions", "Attack Minions?", new[] { "Smart", "Never", "Always" }); 
357             MiscConfig.AddBool("AttackChampions", "Attack Champions?", true); 
358 
 
359             // drawing 
360             DrawingConfig.AddItem( 
361                 new MenuItem("Target" + ChampionName, "Target").SetValue(new Circle(true, Color.DodgerBlue))); 
362             DrawingConfig.AddItem( 
363                 new MenuItem("QRange" + ChampionName, "Q Range").SetValue( 
364                     new Circle(false, Color.FromArgb(150, Color.DodgerBlue)))); 
365             DrawingConfig.AddItem( 
366                 new MenuItem("WRange" + ChampionName, "W Range").SetValue( 
367                     new Circle(false, Color.FromArgb(150, Color.DodgerBlue)))); 
368             DrawingConfig.AddItem( 
369                 new MenuItem("ERange" + ChampionName, "E Range").SetValue( 
370                     new Circle(false, Color.FromArgb(150, Color.DodgerBlue)))); 
371             DrawingConfig.AddItem( 
372                 new MenuItem("RRange" + ChampionName, "R Range").SetValue( 
373                     new Circle(false, Color.FromArgb(150, Color.DodgerBlue)))); 
374 
 
375             // plugins 
376             ComboMenu(ComboConfig); 
377             HarassMenu(HarassConfig); 
378             ManaMenu(ManaConfig); 
379             MiscMenu(MiscConfig); 
380             InterruptMenu(InterruptConfig); 
381             DrawingMenu(DrawingConfig); 
382 
 
383             Config.AddToMainMenu(); 
384         } 
385 
 
386         /// <summary> 
387         ///     Orbwalker Initialization 
388         /// </summary> 
389         private void InitOrbwalker() 
390         { 
391             Orbwalker = Program.Orbwalker; 
392         } 
393 
 
394         #endregion 
395     } 
396 } 
