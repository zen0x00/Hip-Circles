using System;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm = 15f;
    public float secondsPerBeat;
    public float timer = 0f;
    public static event Action OnBeat;

    public float minBpm = 15f;
    public float maxBpm = 25f;

    [SerializeField] AudioManager audioManager;
    public int currentSoundIndex = 0;



    private void Start()
    {
        secondsPerBeat = 60f / bpm;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= secondsPerBeat)
        {
            timer = 0;
            OnBeat?.Invoke();
        }
    }

    public void IncreaseBPM()
    {
        bpm += 1;
        bpm = Mathf.Clamp(bpm, minBpm, maxBpm);
        secondsPerBeat = 60f / bpm;


        currentSoundIndex += 1;
        if(currentSoundIndex >= audioManager.beatSounds.Length) currentSoundIndex = 0;
        audioManager.PlayBeatSound(currentSoundIndex);
    }


    public void DecreaseBPM()
    {
        currentSoundIndex = 0;
        bpm -= 1;
        bpm = Mathf.Clamp(bpm, minBpm, maxBpm);
        secondsPerBeat = 60f / bpm;
        audioManager.PlayFailSound();
    }

    public void UpdatePitch()
    {
        float pitch = bpm / minBpm;
        if (pitch < 0.5f) pitch = 0.5f;
        audioManager.SetMusicPitch(pitch);
    }


    public void Pause()
    {
        enabled = false;
        audioManager.PauseMusic();
    }

    public void Resume()
    {
        enabled = true;
        audioManager.ResumeMusic();
    }

    public void Stop()
    {
        enabled = false;
        audioManager.StopMusic();
    }
}
