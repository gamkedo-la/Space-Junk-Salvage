using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{

    public Image image;

    public float c;
    public float cc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(c >= 0)
        {
            c -= Time.deltaTime;

            float temp = c / cc;

            temp *= .9f;

            Color color = image.color;

            color.a = 1 - temp;

            image.color = color;
        }
    }

    public void Dash(float cooldown)
    {
        c = cooldown;
        cc = cooldown;
    }
}
