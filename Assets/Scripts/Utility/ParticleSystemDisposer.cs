using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDisposer : MonoBehaviour {

    private ParticleSystem particleSystem;
	// Use this for initialization
	void Start ()
    {
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!particleSystem.IsAlive())
            Destroy(this.gameObject);
	}
}
