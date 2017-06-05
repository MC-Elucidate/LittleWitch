using System;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public const float PLATFORM_DROP_DELAY_TIME = 2.5f;
    public const float PLATFORM_DROP_RESET_TIME = 10f;
    public const float PLATFORM_MOVE_DELAY_TIME = 1f;

    public PlatformType platformType = PlatformType.None;
    public bool markedForDeactivate = false;

    public Trap[] trapComponents;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void StoodOn()
    {
        switch (platformType)
        {
            case PlatformType.Trigger:
                StoodOnSpringTrap();
                break;
            case PlatformType.Drop:
                StoodOnDropPlatform();
                break;
            case PlatformType.Move:
                StoodOnMovePlatform();
                break;
        }
    }

    protected abstract void StoodOnSpringTrap();

    protected abstract void StoodOnDropPlatform();

    protected abstract void StoodOnMovePlatform();

    public enum PlatformType { Trigger, Drop, Move, None }
}
