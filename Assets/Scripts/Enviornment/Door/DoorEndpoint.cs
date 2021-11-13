using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEndpoint : MonoBehaviour
{

    public Transform otherDoorEndpoint;

    public Transform myDoorThreshold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerMovement>().jumped == false)
            {
                other.gameObject.GetComponent<PlayerMovement>().jumped = true;
                other.transform.position = otherDoorEndpoint.position;
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().OtherRoom = myDoorThreshold;
            }

        }
    }

}
