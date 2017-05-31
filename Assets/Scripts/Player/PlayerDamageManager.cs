using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageManager : MonoBehaviour {


    private Vulnerability vulnerability;
    public float invulnerablePeriod = 1.5f;
    public float flashRate = 0.1f;
    public Material material;
    private PlayerStatus status;
    private Color defaultColor = Color.white;
    public Color flashColor = Color.red;

    private enum Vulnerability
    {
        Vulnerable,
        Invulnerable
    }
    void Start () {
        status = gameObject.GetComponent<PlayerStatus>();
        vulnerability = Vulnerability.Vulnerable;
        material.color = defaultColor;
	}
	
	void Update () {

    }

    public void BumpedEnemy()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        if (vulnerability == Vulnerability.Invulnerable)
            return;

        print("ouch");
        status.Health -= damage;
        if (status.Health <= 0)
            status.Respawn();
        else
            ActivateInvulnerablility();
    }

    private void ActivateInvulnerablility()
    {
        vulnerability = Vulnerability.Invulnerable;
        Invoke("EndInvulnerability", invulnerablePeriod);
        StartCoroutine(FlashBody());
    }

    private void EndInvulnerability()
    {
        vulnerability = Vulnerability.Vulnerable;
        material.color = Color.white;
    }

    private IEnumerator FlashBody()
    {
        while (vulnerability == Vulnerability.Invulnerable)
        {
            material.color = flashColor;
            yield return new WaitForSecondsRealtime(flashRate);
            material.color = defaultColor;
            yield return new WaitForSecondsRealtime(flashRate);
        }

    }
}
