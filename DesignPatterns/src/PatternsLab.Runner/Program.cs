using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;
using System.Diagnostics;

Console.WriteLine("=== SINGLETON: BEFORE ===\n");


var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: BEFORE ===\n");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();
for (int i = 1; i <= 5; i++)
{
    var orc = new Orc();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");

//Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

//// --- 1) Clone a single enemy ---
//Console.WriteLine("Creating the prototype (slow, once):");
//Enemy proto = new Orc();
//proto.Name = "Boss Orc";

//sw.Restart();
//Enemy clone = proto.Clone();   // must print NO loading line
//Console.WriteLine($"Clone took {sw.ElapsedMilliseconds} ms");

//Console.WriteLine($"\nPrototype model id: {proto.ModelId}");
//Console.WriteLine($"Clone model id:     {clone.ModelId}");
//Console.WriteLine($"Same model id?      {proto.ModelId == clone.ModelId}");           // True

//clone.Weapon.Damage = 999;
//clone.Abilities.Add("Fire Breath");
//Console.WriteLine($"\nChanged the CLONE's damage to 999 and added Fire Breath.");
//Console.WriteLine($"Prototype damage:    {proto.Weapon.Damage}");                      // 25
//Console.WriteLine($"Prototype abilities: {string.Join(", ", proto.Abilities)}");       // Rage
//Console.WriteLine($"Same weapon object?  {ReferenceEquals(proto.Weapon, clone.Weapon)}"); // False
//Console.WriteLine($"Same abilities list? {ReferenceEquals(proto.Abilities, clone.Abilities)}"); // False

//// --- 2) Registry (R5) ---
//Console.WriteLine("\nRegistering prototypes (slow, once each):");
//var registry = new EnemyRegistry();
//registry.Register("orc", new Orc());
//registry.Register("elf", new Elf());

//sw.Restart();
//var newArmy = new List<Enemy>();
//for (int i = 1; i <= 5; i++)
//{
//    var e = registry.Create("orc");   // must print NO loading line
//    e.Name = $"Orc-{i}";
//    newArmy.Add(e);
//}
//Console.WriteLine($"Created 5 orcs from the registry in {sw.ElapsedMilliseconds} ms");  // ~0 ms

//var elf = registry.Create("elf");
//Console.WriteLine($"Elf from registry: {elf.Name}, weapon {elf.Weapon.Name}");
//Console.WriteLine($"Orc-1 and Orc-2 same object? {ReferenceEquals(newArmy[0], newArmy[1])}"); // False
//Console.WriteLine($"Orc-1 and Orc-2 same model id? {newArmy[0].ModelId == newArmy[1].ModelId}"); // True

Console.WriteLine("\n=== BUILDER: BEFORE ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());
