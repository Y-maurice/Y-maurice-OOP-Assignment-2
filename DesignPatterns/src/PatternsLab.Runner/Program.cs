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

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var registry = new EnemyRegistry();
registry.Register("orc", new Orc());               // slow, happens once

sw.Restart();
var clones = new List<Enemy>();
for (int i = 1; i <= 5; i++) clones.Add(registry.Create("orc"));
Console.WriteLine($"Created 5 orcs from the registry in {sw.ElapsedMilliseconds} ms\n");

clones[1].Weapon.Damage = 999;
Console.WriteLine($"Same model id? {clones[0].ModelId == clones[1].ModelId}");
Console.WriteLine($"Clone 0 damage: {clones[0].Weapon.Damage}, clone 1 damage: {clones[1].Weapon.Damage}");

Console.WriteLine("\n=== BUILDER: BEFORE ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());

Console.WriteLine("\n=== BUILDER: AFTER ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudent());
Console.WriteLine(RegistrationCallSites.CreateVideosOnly());

try { CourseRegistrationBuilder.For("x@mail.com", "SEF-101").LiveGroup().Build(); }
catch (InvalidOperationException ex) { Console.WriteLine($"Invalid: {ex.Message}"); }

try { CourseRegistrationBuilder.For("x@mail.com", "SEF-101").VideosOnly().WithGroupCode("G1").Build(); }
catch (InvalidOperationException ex) { Console.WriteLine($"Invalid: {ex.Message}"); }
