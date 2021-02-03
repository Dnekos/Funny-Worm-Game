using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // press escape to exit
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
