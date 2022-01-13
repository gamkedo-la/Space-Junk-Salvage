using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingMover : MonoBehaviour
{
    [SerializeField]
    private int CurrentAlignment;

    public Button[] UIButtons;

    public GameObject myUI;

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

    public void ActivateUI()
    {
        myUI.SetActive(true);
        UIButtons[CurrentAlignment].Select();
        Time.timeScale = 0;
    }

    public void DeactivateUI()
    {
        myUI.SetActive(false);
        Time.timeScale = 1;
    }


}
