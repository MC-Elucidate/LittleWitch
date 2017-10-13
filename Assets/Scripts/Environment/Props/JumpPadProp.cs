using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadProp : MonoBehaviour {

    [SerializeField]
    private float JumpPadPower = 20f;
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
            other.gameObject.GetComponent<PlayerMovementManager>().SteppedOnJumpPad(JumpPadPower);
            audioSource.Play();
        }
    }
}
