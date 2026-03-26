using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateDirDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rotateDirText;
    public enum RotationDir { ClockWise, CounterClockWise};
    public RotationDir currentDir { get; private set; }

    private void OnEnable()
    {
        BeatManager.OnBeat += AssignNewRandomDir;
    }

    private void OnDisable()
    {
        BeatManager.OnBeat -= AssignNewRandomDir;
    }

    public void AssignNewRandomDir()
    {
        int i = Random.Range(0, 2);
        currentDir = i == 0 ? RotationDir.CounterClockWise : RotationDir.ClockWise;

        UpdateDisplay();
    }


    public void UpdateDisplay()
    {
        Color[] colors = { Color.red, Color.green, Color.blue , Color.cyan, Color.magenta, Color.black, Color.white, Color.white};


        rotateDirText.text = "Rotate " + currentDir;
        rotateDirText.color = colors[Random.Range(0, 6)];
    }

    public void ClearDir()
    {
        rotateDirText.text = "Get Ready...";
        rotateDirText.color = Color.black;
    }
}
