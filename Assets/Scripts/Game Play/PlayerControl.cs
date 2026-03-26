using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private RotateDirDisplay rotateDirDisplay;
    [SerializeField] private BeatManager beatManager;
    [SerializeField] private ComboText comboText;
    [SerializeField] private Lifes lifesScript;
    [SerializeField] private CameraShake cameraShake;
    private float animSpeed = 1f;
    private const float MAX_ANIM_SPEED = 5f;
    private const float MIN_ANIM_SPEED = 1f;
    public float scoreStep = 15f;
    public float animSpeedStep = 0.5f;
    private float score = 0f;
    private bool inputGiven = false;
    public int failCount = 0;

    public bool inputWindowOpen = false;
    private float inputWindowTimer = 0f;
    private float inputWindowDur = 2f;

    public static event Action onCorrectCW;
    public static event Action onCorrectCCW;
    public static event Action onWrongHit;
    public static event Action onMiss;

    public bool Pause() => enabled = false;
    public bool Resume() => enabled = true;


    private void OnEnable()
    {
        BeatManager.OnBeat += OpenWindow;
    }

    private void OnDisable()
    {
        BeatManager.OnBeat -= OpenWindow;
    }

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleRotationInput();
        ApplyAnimations();
        HandleWindow();
    }

    private void OpenWindow()
    {
        inputWindowOpen = true;
        inputWindowTimer = inputWindowDur;
        inputGiven = false;
    }

    private void HandleWindow()
    {
        if (!inputWindowOpen) return;

        inputWindowTimer -= Time.deltaTime;

        if (inputWindowTimer <= 0f)
        {
            inputWindowOpen = false;
            if(!inputGiven) onMiss?.Invoke();

        }
    }

    private void HandleRotationInput()
    {
        if (!inputWindowOpen) return;

        bool isPlayerRotatedCW = Input.GetKeyDown(KeyCode.D);
        bool isPlayerRotatedCCW = Input.GetKeyDown(KeyCode.A);


        if (!isPlayerRotatedCW && !isPlayerRotatedCCW) return;

        bool isCorrectDir = (isPlayerRotatedCW && rotateDirDisplay.currentDir == RotateDirDisplay.RotationDir.ClockWise) ||
            (isPlayerRotatedCCW && rotateDirDisplay.currentDir == RotateDirDisplay.RotationDir.CounterClockWise);


        if (isCorrectDir)
        {
            OnCorrectDir();
            if (isPlayerRotatedCW)
                onCorrectCW?.Invoke();
            else
                onCorrectCCW?.Invoke();
        }
        else
        {
            OnWrongDir();
            onWrongHit?.Invoke();
        }


        animSpeed = Mathf.Clamp(animSpeed, MIN_ANIM_SPEED, MAX_ANIM_SPEED);
        UIManager.Instance.UpdateScore(score);
        if (failCount >= 3)
        {
            EndSession();
        }

        inputWindowOpen = false;
        inputGiven = true;

    }

    private void EndSession()
    {
        SessionManager.Instance.SaveSession(score);
        UIManager.Instance.ShowGameOver();
        beatManager.Stop();
        enabled = false;
    }

    private void ApplyAnimations()
    {
        Debug.Log($"Animation Speed: {animSpeed}");
        playerAnim.speed = animSpeed;
    }

    private void OnCorrectDir()
    {
        animSpeed += animSpeedStep;
        comboText.increaseCount();
        score += scoreStep;
        beatManager.IncreaseBPM();
        beatManager.UpdatePitch();
        rotateDirDisplay.ClearDir();
    }

    private void OnWrongDir()
    {
        score -= scoreStep;
        if (score < 0) score = 0;
        cameraShake.Shake();
        lifesScript.lifes();
        comboText.resetCount();
        animSpeed -= animSpeedStep;
        failCount += 1;
        beatManager.DecreaseBPM();
        beatManager.UpdatePitch();
        rotateDirDisplay.ClearDir();
    }
}
