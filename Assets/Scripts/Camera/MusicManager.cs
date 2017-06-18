using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    AudioSource source;

	void Start () {
        source = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
        if (source.time >= 60.9f)
            source.time = 0.9f;
	}
}
