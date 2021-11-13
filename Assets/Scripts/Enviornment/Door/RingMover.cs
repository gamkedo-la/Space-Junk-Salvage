using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMover : MonoBehaviour
{
    [SerializeField]
    private int CurrentAlignment;

    public List<DoorSwitcher> MovingDoors = new List<DoorSwitcher>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRing(int NewAlignment)
    {

        foreach(DoorSwitcher d in MovingDoors)
        {
            d.SwitchDoor(NewAlignment);
        }


    }




}
