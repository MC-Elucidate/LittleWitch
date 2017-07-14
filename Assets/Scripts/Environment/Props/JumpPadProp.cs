using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadProp : MonoBehaviour {

    public float JumpPadPower = 20f;
    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Helpers.Tags.Player)
        {
            other.gameObject.GetComponent<PlayerMovementScript>().SteppedOnJumpPad(JumpPadPower);
            audioSource.Play();
        }
    }
}
