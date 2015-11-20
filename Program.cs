1 using System; 
2 using System.Collections.Generic; 
3 using System.Linq; 
4 using System.Threading; 
5 using AutoSharp.Auto; 
6 using AutoSharp.Utils; 
7 using LeagueSharp; 
8 using LeagueSharp.Common; 
9 using SharpDX; 
10 
 
11 // ReSharper disable ObjectCreationAsStatement 
12 
 
13 namespace AutoSharp 
14 { 
15     class Program 
16     { 
17         public static Utility.Map.MapType Map; 
18         public static Menu Config; 
19         public static MyOrbwalker.Orbwalker Orbwalker; 
20         private static bool _loaded = false; 
21 
 
22         public static void Init() 
23         { 
24             Map = Utility.Map.GetMap().Type;  
25             Config = new Menu("AutoSharp: " + ObjectManager.Player.ChampionName, "autosharp." + ObjectManager.Player.ChampionName, true); 
26             Config.AddItem(new MenuItem("autosharp.mode", "Mode").SetValue(new StringList(new[] {"AUTO", "SBTW"}))).ValueChanged += 
27                 (sender, args) => 
28                 { 
29                     if (Config.Item("autosharp.mode").GetValue<StringList>().SelectedValue == "AUTO") 
30                     { 
31                         Autoplay.Load(); 
32                     } 
33                     else 
34                     { 
35                         Autoplay.Unload(); 
36                         Orbwalker.SetOrbwalkingPoint(Game.CursorPos); 
37                     } 
38                 }; 
39             Config.AddItem(new MenuItem("autosharp.humanizer", "Humanize Movement by ").SetValue(new Slider(new Random().Next(125, 350), 125, 350))); 
40             Config.AddItem(new MenuItem("autosharp.quit", "Quit after Game End").SetValue(true)); 
41             Config.AddItem(new MenuItem("autosharp.shop", "AutoShop?").SetValue(true)); 
42             var options = Config.AddSubMenu(new Menu("Options: ", "autosharp.options")); 
43             options.AddItem(new MenuItem("autosharp.options.healup", "Take Heals?").SetValue(true)); 
44             options.AddItem(new MenuItem("onlyfarm", "Only Farm").SetValue(false)); 
45             if (Map == Utility.Map.MapType.SummonersRift) 
46             { 
47                 options.AddItem(new MenuItem("recallhp", "Recall if Health% <").SetValue(new Slider(30, 0, 100))); 
48             } 
49             var randomizer = Config.AddSubMenu(new Menu("Randomizer", "autosharp.randomizer")); 
50             var orbwalker = Config.AddSubMenu(new Menu("Orbwalker", "autosharp.orbwalker")); 
51             randomizer.AddItem(new MenuItem("autosharp.randomizer.minrand", "Min Rand By").SetValue(new Slider(0, 0, 90))); 
52             randomizer.AddItem(new MenuItem("autosharp.randomizer.maxrand", "Max Rand By").SetValue(new Slider(100, 100, 300))); 
53             randomizer.AddItem(new MenuItem("autosharp.randomizer.playdefensive", "Play Defensive?").SetValue(true)); 
54             randomizer.AddItem(new MenuItem("autosharp.randomizer.auto", "Auto-Adjust? (ALPHA)").SetValue(true)); 
55 
 
56             new PluginLoader(); 
57 
 
58                 Cache.Load();  
59                 Game.OnUpdate += Positioning.OnUpdate; 
60                 Autoplay.Load(); 
61                 Game.OnEnd += OnEnd; 
62                 Obj_AI_Base.OnIssueOrder += AntiShrooms; 
63                 Game.OnUpdate += AntiShrooms2; 
64                 Spellbook.OnCastSpell += OnCastSpell; 
65                 Obj_AI_Base.OnDamage += OnDamage; 
66 
 
67 
 
68             Orbwalker = new MyOrbwalker.Orbwalker(orbwalker); 
69 
 
70             Utility.DelayAction.Add( 
71                     new Random().Next(1000, 10000), () => 
72                     { 
73                         new LeagueSharp.Common.AutoLevel(Utils.AutoLevel.GetSequence().Select(num => num - 1).ToArray()); 
74                         LeagueSharp.Common.AutoLevel.Enable(); 
75                         Console.WriteLine("AutoLevel Init Success!"); 
76                     }); 
77         } 
78 
 
79         public static void OnDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args) 
80         { 
81             if (sender == null) return; 
82             if (args.TargetNetworkId == ObjectManager.Player.NetworkId && (sender is Obj_AI_Turret || sender is Obj_AI_Minion)) 
83             { 
84                     Orbwalker.SetOrbwalkingPoint(Heroes.Player.Position.Extend(Wizard.GetFarthestMinion().Position, 500).RandomizePosition()); 
85             } 
86         } 
87 
 
88         private static void AntiShrooms2(EventArgs args) 
89         { 
90             if (Map == Utility.Map.MapType.SummonersRift && !Heroes.Player.InFountain() && 
91                 Heroes.Player.HealthPercent < Config.Item("recallhp").GetValue<Slider>().Value) 
92             { 
93                 if (Heroes.Player.HealthPercent > 0 && Heroes.Player.CountEnemiesInRange(1800) == 0 && 
94                     !Turrets.EnemyTurrets.Any(t => t.Distance(Heroes.Player) < 950) && 
95                     !Minions.EnemyMinions.Any(m => m.Distance(Heroes.Player) < 950)) 
96                 { 
97                     Orbwalker.ActiveMode = MyOrbwalker.OrbwalkingMode.None; 
98                     if (!Heroes.Player.HasBuff("Recall")) 
99                     { 
100                         Heroes.Player.Spellbook.CastSpell(SpellSlot.Recall); 
101                     } 
102                 } 
103             } 
104 
 
105             var turretNearTargetPosition = 
106                     Turrets.EnemyTurrets.FirstOrDefault(t => t.Distance(Heroes.Player.ServerPosition) < 950); 
107             if (turretNearTargetPosition != null && turretNearTargetPosition.CountNearbyAllyMinions(950) < 3) 
108             { 
109                 Orbwalker.SetOrbwalkingPoint(Heroes.Player.Position.Extend(HeadQuarters.AllyHQ.Position, 950)); 
110             } 
111         } 
112 
 
113         private static void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args) 
114         { 
115             if (sender.Owner.IsMe) 
116             { 
117                 if (sender.Owner.IsDead) 
118                 { 
119                     args.Process = false; 
120                     return; 
121                 } 
122                 if (Map == Utility.Map.MapType.SummonersRift) 
123                 { 
124                     if (Config.Item("onlyfarm").GetValue<bool>() && args.Target.IsValid<Obj_AI_Hero>() && 
125                         args.Target.IsEnemy) 
126                     { 
127                         args.Process = false; 
128                         return; 
129                     } 
130                     if (Heroes.Player.InFountain() && args.Slot == SpellSlot.Recall) 
131                     { 
132                         args.Process = false; 
133                         return; 
134                     } 
135                     if (Heroes.Player.HasBuff("Recall")) 
136                     { 
137                         args.Process = false; 
138                         return; 
139                     } 
140                 } 
141                 if (Heroes.Player.UnderTurret(true) && args.Target.IsValid<Obj_AI_Hero>()) 
142                 { 
143                     args.Process = false; 
144                     return; 
145                 } 
146             } 
147         } 
148 
 
149         private static void OnEnd(GameEndEventArgs args) 
150         { 
151             if (Config.Item("autosharp.quit").GetValue<bool>()) 
152             { 
153                 Thread.Sleep(30000); 
154                 Game.Quit(); 
155             } 
156         } 
157 
 
158         public static void AntiShrooms(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args) 
159         { 
160             if (sender != null && sender.IsMe) 
161             { 
162                 if (sender.IsDead) 
163                 { 
164                     args.Process = false; 
165                     return; 
166                 } 
167                 var turret = Turrets.ClosestEnemyTurret; 
168                 if (Map == Utility.Map.MapType.SummonersRift && Heroes.Player.HasBuff("Recall") && Heroes.Player.CountEnemiesInRange(1800) == 0 && 
169                     turret.Distance(Heroes.Player) > 950 && !Minions.EnemyMinions.Any(m => m.Distance(Heroes.Player) < 950)) 
170                 { 
171                     args.Process = false; 
172                     return; 
173                 } 
174 
 
175                 if (args.Order == GameObjectOrder.MoveTo) 
176                 { 
177                     if (args.TargetPosition.IsZero) 
178                     { 
179                         args.Process = false; 
180                         return; 
181                     } 
182                     if (!args.TargetPosition.IsValid()) 
183                     { 
184                         args.Process = false; 
185                         return; 
186                     } 
187                     if (Map == Utility.Map.MapType.SummonersRift && Heroes.Player.InFountain() && 
188                         Heroes.Player.HealthPercent < 100) 
189                     { 
190                         args.Process = false; 
191                         return; 
192                     } 
193                     if (turret != null && turret.Distance(args.TargetPosition) < 950 && 
194                         turret.CountNearbyAllyMinions(950) < 3) 
195                     { 
196                         args.Process = false; 
197                         return; 
198                     } 
199                 } 
200 
 
201                 #region BlockAttack 
202 
 
203                 if (args.Target != null && args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo) 
204                 { 
205                     if (Config.Item("onlyfarm").GetValue<bool>() && args.Target.IsValid<Obj_AI_Hero>()) 
206                     { 
207                         args.Process = false; 
208                         return; 
209                     } 
210                     if (args.Target.IsValid<Obj_AI_Hero>()) 
211                     { 
212                         if (Minions.AllyMinions.Count(m => m.Distance(Heroes.Player) < 900) < 
213                             Minions.EnemyMinions.Count(m => m.Distance(Heroes.Player) < 900)) 
214                         { 
215                             args.Process = false; 
216                             return; 
217                         } 
218                         if (((Obj_AI_Hero) args.Target).UnderTurret(true)) 
219                         { 
220                             args.Process = false; 
221                             return; 
222                         } 
223                     } 
224                     if (Heroes.Player.UnderTurret(true) && args.Target.IsValid<Obj_AI_Hero>()) 
225                     { 
226                         args.Process = false; 
227                         return; 
228                     } 
229                     if (turret != null && turret.Distance(ObjectManager.Player) < 950 && turret.CountNearbyAllyMinions(950) < 3) 
230                     { 
231                         args.Process = false; 
232                         return; 
233                     } 
234                     if (Heroes.Player.HealthPercent < Config.Item("recallhp").GetValue<Slider>().Value) 
235                     { 
236                         args.Process = false; 
237                         return; 
238                     } 
239                 } 
240 
 
241                 #endregion 
242             } 
243             if (sender != null && args.Target != null && args.Target.IsMe) 
244             { 
245                 if (sender is Obj_AI_Turret || sender is Obj_AI_Minion) 
246                 { 
247                     var minion = Wizard.GetClosestAllyMinion(); 
248                     if (minion != null) 
249                     { 
250                         Orbwalker.SetOrbwalkingPoint( 
251                             Heroes.Player.Position.Extend(Wizard.GetClosestAllyMinion().Position, Heroes.Player.Distance(minion) + 100)); 
252                     } 
253                 } 
254             } 
255         } 
256 
 
257         public static void Main(string[] args) 
258         { 
259             Game.OnUpdate += AdvancedLoading; 
260         } 
261 
 
262         private static void AdvancedLoading(EventArgs args) 
263         { 
264             if (!_loaded) 
265             { 
266                 if (ObjectManager.Player.Gold > 0) 
267                 { 
268                     _loaded = true; 
269                     Utility.DelayAction.Add(new Random().Next(3000, 25000), Init); 
270                 } 
271             } 
272         } 
273     } 
274 } 
