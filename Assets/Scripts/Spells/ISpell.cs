public interface ISpell
{
    string spellName { get; set; }
    string inputString { get; set; }
    Element element { get; set; }
    SpellType type { get; set; }
    float damage { get; set; }
    float range { get; set; }
    float aoe { get; set; }
    float speed { get; set; }

    //How the spell affects the world
    void Trigger();
}

//This should be an interface itself or something to inherit from
public enum Element
{
    Earth,
    Fire,
    Water,
    Wind
}

public enum SpellType
{
    AoE,
    Cone,
    Projectile,
    Ray,
    Self
}