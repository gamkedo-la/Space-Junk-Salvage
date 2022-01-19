using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogWindow : MonoBehaviour
{
    public TextMeshProUGUI text;

    public string[] currentDialog;

    public int bookmark = -1;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialog(string[] lines)
    {
        GetComponent<CanvasGroup>().alpha = 1;

        bookmark = -1;

        currentDialog = lines;

        Debug.Log("start dialog");

        Player.GetComponent<PlayerMovement>().Actionable = false;
        Player.GetComponent<PlayerAttacks>().paused = true;
        Player.GetComponent<Inventory>().Actionable = false;
        Time.timeScale = 0;



        NextLine();               
    }

    public void NextLine()
    {
        if (GetComponent<CanvasGroup>().alpha != 0)
        {
            bookmark++;

            if (bookmark >= currentDialog.Length)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                Time.timeScale = 1;

                Debug.Log("end dialog");
                Player.GetComponent<PlayerMovement>().Actionable = true;
                Player.GetComponent<PlayerAttacks>().paused = false;
                Player.GetComponent<Inventory>().Actionable = true;
            }
            else
            {
                text.text = currentDialog[bookmark];
            }
        }
    }

    public void OnNextLineCall(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            NextLine();
        }
    }

}
