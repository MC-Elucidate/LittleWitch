using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour {

    private GameObject LockOnIndicator;
    private GameObject LockOnTarget;
    public GameObject LockOnIndicatorPrefab;
    public LayerMask LockOnLayer;

    private float Radius = 20f;
    private bool LockedOn = false;
    
    private MagicManager magicManager;

    void Start () {
        magicManager = gameObject.GetComponent<MagicManager>();
    }
	
	void Update () {

        if (LockedOn && LockOnIndicator == null)
            TurnOffLockOn();
    }

    public void LockOnPressed()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position + transform.forward * Radius, Radius, LockOnLayer);
        Collider closestObject = null;

        if (objectsInRange.Length == 0)
            return;

        float closestObjectDistance = float.MaxValue;
        foreach (Collider objectInRange in objectsInRange)
        {
            if (objectInRange.isTrigger)
                continue;
            if (Mathf.Abs((objectInRange.transform.position - transform.position).sqrMagnitude) < closestObjectDistance)
                closestObject = objectInRange;
        }

        LockOnTarget = closestObject.gameObject;
        LockedOn = true;

        Transform LockOnIndicatorTransform = Helpers.FindObjectInChildren(LockOnTarget, "LockOnIndicatorPosition").transform;
        LockOnIndicator = Instantiate(LockOnIndicatorPrefab, LockOnIndicatorTransform.position, Quaternion.identity);
        LockOnIndicator.transform.parent = LockOnIndicatorTransform;
    }

    public void LockOnReleased()
    {
        if (!LockedOn)
            return;

        magicManager.CastSpellOnTarget(LockOnTarget);
        TurnOffLockOn();
    }

    private void TurnOffLockOn()
    {
        LockedOn = false;
        Destroy(LockOnIndicator);
        LockOnTarget = null;
    }
    
    //Draw LockOn Range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * Radius, Radius);
    }
}
