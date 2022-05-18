using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardColldier : MonoBehaviour
{
    string F = "Friendly";
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("123");
                break;
            case "Enemy":
                Debug.Log("enemy here");
                break;
            case "Finish":
                Debug.Log("you done");
                break;
            default:
                Debug.Log("no");
                break;

        }
    }
}
