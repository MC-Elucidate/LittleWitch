using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour {

    private AudioSource audioSource;

    public AudioClip FireSpellSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayFireSpellSound()
    {
        PlayCharacterSound(FireSpellSound);
    }

    private void PlayCharacterSound(AudioClip clip)
    {
        if (clip == null)
            return;
        audioSource.PlayOneShot(clip);
    }
}
