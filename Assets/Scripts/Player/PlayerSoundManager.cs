using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip JumpSound;
    [SerializeField]
    private AudioClip DamageReceivedSound;
    [SerializeField]
    private AudioClip FireSpellSound;
    [SerializeField]
    private AudioClip FootstepSound;

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
