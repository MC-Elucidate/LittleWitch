using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : Pickup {

    [SerializeField]
    private int Value;
    [SerializeField]
    private AudioClip collectionSound;

    private AudioSource audioSource;
    private Animator animator;

    void Start () {
        destroyTime = 1f;
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
    }
	
	void Update () {
		
	}

    public override void PickupEffect(GameObject player)
    {
        player.GetComponent<PlayerStatus>().AddGems(Value);
        audioSource.PlayOneShot(collectionSound);
        animator.SetTrigger("Collected");
    }
}
