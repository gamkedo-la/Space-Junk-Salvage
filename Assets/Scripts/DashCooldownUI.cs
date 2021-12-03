using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{

    public Image image;

    float total;
    float counter;
    float alpha;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if(counter >= total)
        {

            alpha = 1;

        }
        else
        {
            alpha = .1f + (counter / (total * 2));

        }

        Color c = image.color;
        c.a = alpha;
        image.color = c;


    }

    public void Dash(float cooldown)
    {
        total = cooldown;
        counter = 0;
    }
}
