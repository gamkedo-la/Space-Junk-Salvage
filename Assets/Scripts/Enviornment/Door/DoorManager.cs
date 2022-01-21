using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform myEndPoint;

    public DoorManager other;

    public GameObject MyRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (other != null)
        {
            LinkDoors();
        }

        myEndPoint.name = gameObject.name + " endpoint";

        myEndPoint.GetComponent<DoorEndpoint>().Room = MyRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LinkDoors()
    {
        myEndPoint.GetComponent<DoorEndpoint>().otherDoorEndpoint = other.myEndPoint;


    }


}
