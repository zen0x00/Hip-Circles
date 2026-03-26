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

    [SerializeField] AudioManager audioSource;
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
        currentSoundIndex += 1;
        bpm = Mathf.Clamp(bpm, minBpm, maxBpm);
        secondsPerBeat = 60 / bpm;
        audioSource.PlayBeatSound(currentSoundIndex);
    }


    public void DecreaseBPM()
    {
        currentSoundIndex = 0;
        bpm -= 1;
        bpm = Mathf.Clamp(bpm, minBpm, maxBpm);
        secondsPerBeat = 60 / bpm;
    }

    public void UpdatePitch()
    {
        float pitch = bpm / minBpm;
        audioSource.SetMusicPitch(pitch);
    }

}
