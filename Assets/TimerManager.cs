using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimerManager : MonoBehaviour
{
    public float MaxLevelTime = 240;
    float currenttime;
    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        transform.localScale = new Vector3(Mathf.Lerp(1, 0, currenttime / MaxLevelTime), 1, 1); 

        if (currenttime > MaxLevelTime)
            SceneManager.LoadScene("GameOver Menu");
    }
}
