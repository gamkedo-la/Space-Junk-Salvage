using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTest : MonoBehaviour
{
    public GameObject Alert;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {

            Instantiate(Alert, transform.position, transform.rotation);
        }


    }
}
