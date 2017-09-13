using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour {

    private PlayerSoundManager soundManager;

	void Start () {
        soundManager = gameObject.GetComponentInParent<PlayerSoundManager>();	
	}

    public void PlayFootstepSound()
    {
        soundManager.PlayFootstepSound();
    }
}
