using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {

    private AudioSource audioSource;

    public AudioClip JumpSound;
    public AudioClip DamageReceivedSound;
    public AudioClip FireSpellSound;
    public AudioClip FootstepSound;

    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}

    public void PlayJumpSound()
    {
        PlayCharacterSound(JumpSound);
    }

    public void PlayDamageReceivedSound()
    {
        PlayCharacterSound(DamageReceivedSound);
    }

    public void PlayFireSpellSound()
    {
        PlayCharacterSound(FireSpellSound);
    }

    public void PlayFootstepSound()
    {
        PlayCharacterSound(FootstepSound);
    }

    private void PlayCharacterSound(AudioClip clip)
    {
        if (clip == null)
            return;
        audioSource.PlayOneShot(clip);
    }
}
