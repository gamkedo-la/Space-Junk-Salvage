using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public Transform OtherEnd;


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
        Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerMovement>().SwitchingRooms == false)
        {
            other.gameObject.GetComponent<PlayerMovement>().SwitchingRooms = true;
            other.gameObject.GetComponent<PlayerMovement>().OtherRoom = OtherEnd;
        }
    }
}
