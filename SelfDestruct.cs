using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 5f;
    void Update()
    {
        
            Destroy(gameObject,timeToDestroy);
        
    }
}
