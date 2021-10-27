using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    private int[] Items = new int[2];
    // index 0: keys
    // index 2: health potions

    public int PotionHealAmount = 25;

    public TextMeshProUGUI Keys;
    public TextMeshProUGUI Potions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup(int index, int amount)
    {
        Items[index] += amount;
        UIUpdate();
    }

    public bool Drop(int index, int amount)
    {
        if (Items[index] >= amount)
        {
            Items[index] -= amount;
            UIUpdate();
            return true;
        }

        else
        {
            return false;
        }
    }

    public void UsePotion()
    {
        if(Drop(1,1) == true)
        {

            GetComponent<PlayerHP>().TakeDamage(-PotionHealAmount, 0, Vector3.zero);

        }

    }

    public void UIUpdate()
    {
        Keys.text = Items[0].ToString();
        Potions.text = Items[1].ToString();
    }

}
