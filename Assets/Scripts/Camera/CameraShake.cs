using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private GameObject Miss;
   Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    public void Shake()
    {
        StartCoroutine(Missed());
        transform.position = originalPos + new Vector3(Random.Range(-0.05f,0.05f), Random.Range(-0.05f,0.05f), 0);
        Invoke("ResetPosition", 0.05f); 
    }
    void ResetPosition()
    {
        transform.position = originalPos;
    }
    IEnumerator Missed()
    {
        Miss.SetActive(true);
        yield return new WaitForSeconds(1f);
        Miss.SetActive(false);
    }
}

