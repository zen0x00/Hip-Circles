using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource sfxSource;


    [SerializeField]private AudioClip bgMusic;
    [SerializeField]private AudioClip btnClickSound;
    [SerializeField]private AudioClip[] beatSounds;



    private void Start()
    {
        audioSource.clip = bgMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(btnClickSound);
    }

    public void PlayBeatSound(int i)
    {
        sfxSource.PlayOneShot(beatSounds[i]);
    }

    public void SetMusicPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

}
