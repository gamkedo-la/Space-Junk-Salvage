using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    private int[] Items = new int[2];
    // index 0: keys
    // index 1: health potions

    public int PotionHealAmount = 25;

    public TextMeshProUGUI Keys;
    public TextMeshProUGUI Potions;

    public float HealCooldown = 3.0f;
    float HCReset;

    public GameObject HealEffect;

    public DialogWindow dialog;

    public string[] pickupDialogs;

    private string[] dialogString = new string[1];

    private bool[] firstPickup = new bool[2];

    // Start is called before the first frame update
    void Start()
    {
        HCReset = HealCooldown;
        HealCooldown = 0;

        for (int i = 0; i < firstPickup.Length; i++)
        {
            firstPickup[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealCooldown -= Time.deltaTime;

    }

    public void Pickup(int index, int amount)
    {
        Items[index] += amount;
        UIUpdate();

        if(firstPickup[index] == true)
        {
            firstPickup[index] = false;
            dialogString[0] = pickupDialogs[index];
            dialog.DisplayDialog(dialogString);
        }
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
        if (HealCooldown <= 0)
        {
            if (Drop(1, 1) == true)
            {
                GetComponent<PlayerHP>().TakeDamage(-PotionHealAmount, 0, Vector3.zero);
                HealCooldown = HCReset;
                Instantiate(HealEffect, transform.position, Quaternion.identity, gameObject.transform);
            }
        }

    }

    public void UIUpdate()
    {
        Keys.text = Items[0].ToString();
        Potions.text = Items[1].ToString();
    }

}
