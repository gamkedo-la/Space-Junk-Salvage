using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitcher : MonoBehaviour
{
    public DoorManager[] myDoors;

    public GameObject ClosedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchDoor(int NewAlignment)
    {

        if(myDoors[NewAlignment] != null)
        {
            GetComponent<DoorManager>().other = myDoors[NewAlignment];
            GetComponent<DoorManager>().LinkDoors();
            ClosedDoor.SetActive((false));
        }
        else
        {
            //this door is closed because of the way the rings are aligned
            //do something about that here 
            //put a wall segemnt in or something

            ClosedDoor.SetActive(true);
        }


    }

}
