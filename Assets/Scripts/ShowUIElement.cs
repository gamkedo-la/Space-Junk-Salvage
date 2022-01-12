using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIElement : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject NewWindow;

    public Button myButton;
    public Button NewWindowStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        NewWindow.SetActive(true);
        Buttons.SetActive(false);
        NewWindowStart.Select();

    }

    public void Hide()
    {
        NewWindow.SetActive(false);
        Buttons.SetActive(true);
        myButton.Select();
    }

}
