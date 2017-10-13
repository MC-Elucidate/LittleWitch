using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDisposer : MonoBehaviour {

    private ParticleSystem particlesystem;
	
	void Start ()
    {
        particlesystem = this.gameObject.GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
        if (!particlesystem.IsAlive())
            Destroy(this.gameObject);
	}
}
