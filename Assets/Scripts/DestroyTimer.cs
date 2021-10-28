using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float End;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, End);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
