using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform myEndPoint;

    public DoorManager other;

    // Start is called before the first frame update
    void Start()
    {
        LinkDoors();

        myEndPoint.name = gameObject.name + " endpoint";
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
