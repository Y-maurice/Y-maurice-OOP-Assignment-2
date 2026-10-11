namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }

    public Weapon() { }
    public Weapon(Weapon other) //copy constructor for weapon
    {
        Name = other.Name;
        Damage = other.Damage;
    }
}

//prototype
public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy other)
    {
        _modelData = other._modelData;
        Name = other.Name;
        Health = other.Health;

        Weapon = new Weapon(other.Weapon);

        foreach (var item in other.Abilities)
        {
            Abilities.Add(item);
        }
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc other) : base(other)
    {
    }
    public override Enemy Clone()
    {
        return new Orc(this);
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }



    private Elf(Elf other) : base(other)
    {
    }

    public override Enemy Clone()
    {
        return new Elf(this);
    }
}




// stores named prototypes and returns clones on demand
public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes =
        new(StringComparer.OrdinalIgnoreCase);

    public void Register(string key, Enemy prototype)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be empty.", nameof(key));
        if (prototype is null)
            throw new ArgumentNullException(nameof(prototype));

        _prototypes[key] = prototype;            // registering the same key again replaces it
    }

    public Enemy Create(string key)
    {
        if (!_prototypes.TryGetValue(key, out var prototype))
            throw new KeyNotFoundException($"No prototype registered with key '{key}'.");

        return prototype.Clone();
    }
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e) => e.Clone();
}
