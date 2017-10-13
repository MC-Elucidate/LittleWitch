using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventForestFiresCreature : MonoBehaviour {

    [SerializeField]
    private TreeProp[] TreesToMonitor;
    private int TargetTreeIndex = -1;
    private State state = State.Idle;
    private float lerpRate = 0.01f;
    private int stoppingRange = 25;

    private enum State
    {
        Idle,
        Working
    }

	void Start () {
	}
	
	void Update () {
        if (state == State.Idle)
            CheckTrees();

        else if (state == State.Working)
            MoveToTarget();
	}

    private void CheckTrees()
    {
        for(int i=0; i<TreesToMonitor.Length; i++)
        {
            if (TreesToMonitor[i].elementState == Element.Fire)
            {
                TargetTreeIndex = i;
                state = State.Working;
                return;
            }
        }
    }

    private void MoveToTarget()
    {
        Vector3 destination = TreesToMonitor[TargetTreeIndex].transform.position;
        destination.y = transform.position.y;

        if ((transform.position - destination).sqrMagnitude < stoppingRange)
        {
            state = State.Idle;
            TreesToMonitor[TargetTreeIndex].ChemistryInteraction(0, Element.Water);
            TargetTreeIndex = -1;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, destination, lerpRate);
        }
    }
}
