using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogWindow : MonoBehaviour
{
    public TextMeshProUGUI text;

    public string[] currentDialog;

    public int bookmark = -1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
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

        Time.timeScale = 0;

        NextLine();               
    }

    public void NextLine()
    {
        bookmark++;

        if (bookmark >= currentDialog.Length)
        {
            GetComponent<CanvasGroup>().alpha = 0;
            Time.timeScale = 1;
        }
        else
        {
            text.text = currentDialog[bookmark];
        }
    }

}
