using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseStatus))]
public class FreezableEnemy : FreezableObject {

    private EnemyBaseStatus status;

    protected override void Start () {
        base.Start();
        status = GetComponent<EnemyBaseStatus>();
	}

    protected override void Update () {
        base.Update();
	}

    protected override void Freeze()
    {
        base.Freeze();
        status.Freeze();
    }

    protected override void UnFreeze()
    {
        base.UnFreeze();
        status.UnFreeze();
    }
}
