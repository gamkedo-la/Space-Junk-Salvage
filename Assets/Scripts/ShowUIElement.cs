using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIElement : MonoBehaviour
{
    public CanvasGroup CG;

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
        CG.alpha = 1;
        CG.interactable = true;
        CG.blocksRaycasts = true;


    }

    public void Hide()
    {
        CG.alpha = 0;
        CG.interactable = false;
        CG.blocksRaycasts = false;
    }

}
