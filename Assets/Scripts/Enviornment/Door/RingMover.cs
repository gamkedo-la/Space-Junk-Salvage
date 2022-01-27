using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RingMover : MonoBehaviour
{
    [SerializeField]
    private int CurrentAlignment;

    public Button[] UIButtons;

    public GameObject myUI;

    public List<DoorSwitcher> MovingDoors = new List<DoorSwitcher>();

    public List<RoomMover> MovingRooms = new List<RoomMover>();

    public TextMeshProUGUI num;

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
        CurrentAlignment = NewAlignment;

        num.text = (CurrentAlignment + 1).ToString();
        foreach(DoorSwitcher d in MovingDoors)
        {
            d.SwitchDoor(NewAlignment);
        }

        foreach(RoomMover r in MovingRooms)
        {
            r.MoveRoom(NewAlignment);
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
