using UnityEngine;
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

    public Transform cellHand;
    public Transform cellReceptacle;
    public GameObject HealEffect;

    public DialogWindow dialog;

    public string[] pickupDialogs;

    private string[] dialogString = new string[1];

    private bool[] firstPickup = new bool[2];

    private EnergyCellRecharge spawnedCell;

    public bool Actionable = true;

    public CooldownUI rechargeCooldownUI;

    // Start is called before the first frame update
    void Start()
    {
        HCReset = HealCooldown;
        HealCooldown = 0;

        for (int i = 0; i < firstPickup.Length; i++)
        {
            firstPickup[i] = true;
        }

        UIUpdate();
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

        if (firstPickup[index] == true)
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
        if (Actionable == true)
        {
            if (HealCooldown <= 0)
            {
                if (Drop(1, 1) == true)
                {
                    HealCooldown = HCReset;
                    GetComponentInChildren<Animator>().SetTrigger("Recharge");
                    // If we have more energy cells left, run the cooldown sequence
                    if (Items[1] > 0)
                    {
                        rechargeCooldownUI.ActivateAndCooldown(HealCooldown, 0);
                    }
                }
            }
        }
    }

    public void UIUpdate()
    {
        Keys.text = Items[0].ToString();
        Potions.text = Items[1].ToString();
        if (Items[1] <= 0)
        {
            rechargeCooldownUI.gameObject.SetActive(false);
        }
        else
        {
            rechargeCooldownUI.gameObject.SetActive(true);
        }
    }

    public void OnTakeCell()
    {
        spawnedCell = Instantiate(HealEffect, cellHand).GetComponent<EnergyCellRecharge>();
    }

    public void OnDropCell()
    {
        GetComponent<PlayerHP>().TakeDamage(-PotionHealAmount, 0, Vector3.zero, Vector3.zero);
        spawnedCell.transform.parent = cellReceptacle;
        spawnedCell.Deplete(HealCooldown);
    }
}