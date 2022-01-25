using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float expireAfter = 5.0f;

    void Start()
    {
        Destroy(gameObject, expireAfter);    
    }
}
