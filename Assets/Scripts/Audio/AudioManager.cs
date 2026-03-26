using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;


    [SerializeField]private AudioClip bgMusic;
    [SerializeField]private AudioClip btnClickSound;
    [SerializeField]public AudioClip[] beatSounds;
    [SerializeField]private AudioClip failSound;



    private void Start()
    {
        musicSource.clip = bgMusic;
        musicSource.loop = true;
    }

    public void StartMusic()
    {
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(btnClickSound, 5f);
    }

    public void PlayBeatSound(int i)
    {
        sfxSource.PlayOneShot(beatSounds[i]);
    }

    public void SetMusicPitch(float pitch)
    {
        musicSource.pitch = pitch;
    }

    public void PlayFailSound()
    {
        sfxSource.PlayOneShot(failSound);
    }

}
