using UnityEngine;

public abstract class Trap: MonoBehaviour
{
    public bool trapSprung;
    public bool trapCleared;
    public bool trapCanRetrigger;

    public abstract void SpringTrap();
    public abstract void ResetTrap();
}