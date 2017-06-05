using UnityEngine;

public abstract class Trap: MonoBehaviour
{
    public bool trapCanRetrigger;
    public float trapResetDelay;

    protected bool trapSprung;
    protected bool trapCleared;
    protected bool trapWaitingForReset;

    public abstract void SpringTrap();
    public abstract void ResetTrap();
}