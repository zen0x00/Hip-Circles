using System;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [SerializeField] private BeatManager beatManager;

    public static GameData currentSession { get; private set; }
    private float sessionTimer;
    private int currentStreak;
    private int bestStreak;
    private int totalHits;
    private float calPerRep = 0.05f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerControl.onCorrectCW += OnCorrectCW;
        PlayerControl.onCorrectCCW += OnCorrectCCW;
        PlayerControl.onWrongHit += OnWrongHit;
        PlayerControl.onMiss += OnMiss;
    }

    private void OnDisable()
    {
        PlayerControl.onCorrectCW -= OnCorrectCW;
        PlayerControl.onCorrectCCW -= OnCorrectCCW;
        PlayerControl.onWrongHit -= OnWrongHit;
        PlayerControl.onMiss -= OnMiss;
    }

    private void Start()
    {
        currentSession = new GameData();
        sessionTimer = 0f;
        currentStreak = 0;
        bestStreak = 0;
        totalHits = 0;
    }

    private void Update()
    {
        sessionTimer += Time.deltaTime;
    }

    private void OnCorrectCW()
    {
        currentSession.totalReps++;
        currentSession.CW++;
        totalHits++;
        currentStreak++;
        if (currentStreak > bestStreak)
            bestStreak = currentStreak;
    }

    private void OnCorrectCCW()
    {
        currentSession.totalReps++;
        currentSession.CCW++;
        totalHits++;
        currentStreak++;
        if (currentStreak > bestStreak)
            bestStreak = currentStreak;
    }

    private void OnWrongHit()
    {
        currentSession.totalWrongHits++;
        totalHits++;
        currentStreak = 0;
    }

    private void OnMiss()
    {
        currentStreak = 0;
    }

    public void SaveSession(float finalScore)
    {
        currentSession.finalScore = (int)finalScore;
        currentSession.sessionDuration = FormatDuration(sessionTimer);
        currentSession.longestStreak = bestStreak;
        currentSession.maxBPMReached = (int)beatManager.bpm;
        currentSession.calories = currentSession.totalReps * calPerRep;
        currentSession.accuracy = totalHits > 0 ? (float)currentSession.totalReps / totalHits * 100f: 0f;


    }

    private string FormatDuration(float seconds)
    {
        int mins = (int)(seconds / 60);
        int secs = (int)(seconds % 60);
        return $"{mins:00}:{secs:00}";
    }
}