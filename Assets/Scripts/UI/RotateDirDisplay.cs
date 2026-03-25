using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateDirDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rotateDirText;
    public enum RotationDir { ClockWise, CounterClockWise};
    public RotationDir currentDir { get; private set; }

    private void Start()
    {
        AssignNewRandomDir();
    }

    public void AssignNewRandomDir()
    {
        int i = Random.Range(0, 2);
        currentDir = i == 0 ? RotationDir.CounterClockWise : RotationDir.ClockWise;

        UpdateDisplay();
    }


    public void UpdateDisplay()
    {
        rotateDirText.text = "Rotate " + currentDir;
    }
}
