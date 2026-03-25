using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private RotateDirDisplay rotateDirDisplay;
    private float animSpeed = 1f;
    private const float MAX_ANIM_SPEED = 5f;
    private const float MIN_ANIM_SPEED = 0f;
    private const float SCORE_STEP = 0.1f;
    private float score = 0f;


    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleRotationInput();
        ApplyAnimations();
    }

    private void HandleRotationInput()
    {
        bool isPlayerRotatedCW = Input.GetKeyDown(KeyCode.D);
        bool isPlayerRotatedCCW = Input.GetKeyDown(KeyCode.A);

        if (!isPlayerRotatedCW && !isPlayerRotatedCCW) return;

        bool isCorrectDir = (isPlayerRotatedCW && rotateDirDisplay.currentDir == RotateDirDisplay.RotationDir.ClockWise) ||
            (isPlayerRotatedCCW && rotateDirDisplay.currentDir == RotateDirDisplay.RotationDir.CounterClockWise);

        animSpeed += isCorrectDir ? SCORE_STEP : -SCORE_STEP;
        animSpeed = Mathf.Clamp(animSpeed, MIN_ANIM_SPEED, MAX_ANIM_SPEED);

        score += isCorrectDir ? 15f : 0f;
        UIManager.Instance.UpdateScore(score);

        rotateDirDisplay.AssignNewRandomDir();
    }

    private void ApplyAnimations()
    {
        Debug.Log($"Animation Speed: {animSpeed}");
        playerAnim.speed = animSpeed;
    }

}
