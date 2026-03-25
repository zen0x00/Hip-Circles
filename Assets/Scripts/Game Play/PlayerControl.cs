using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private RotateDirDisplay rotateDirDisplay;
    private float animSpeed = 1f;
    private const float MAX_ANIM_SPEED = 5f;
    private const float MIN_ANIM_SPEED = 0f;


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

        animSpeed += isCorrectDir ? 1 : -1;
        Mathf.Clamp(animSpeed, MIN_ANIM_SPEED, MAX_ANIM_SPEED);

        rotateDirDisplay.AssignNewRandomDir();
    }

    private void ApplyAnimations()
    {
        Debug.Log($"Animation Speed: {animSpeed}");
        playerAnim.speed = animSpeed;
    }

}
