using UnityEngine;

public class Fireball : MonoBehaviour, ISpell
{
    public float aoe { get { return 5.0f; } set { aoe = value; } }
    public float damage { get { return 10.0f; } set { damage = value; } }
    public Element element { get { return Element.Fire; } set { element = value; } }
    public string inputString { get { return "222"; } set { inputString = value; } }
    public float range { get { return 50.0f; } set { range = value; } }
    public SpellType type { get { return SpellType.Projectile; } set { type = value; } }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Trigger()
    {
        Debug.Log("Destroyed " + this.name);
        Destroy(this.gameObject, 2.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        this.Trigger();
    }
}
