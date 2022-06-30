using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitapplication : MonoBehaviour
{

     void Update()
    {
        OnApplicationQuit();
    }




    void OnApplicationQuit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("test");
            Application.Quit();
        }
    }
}
