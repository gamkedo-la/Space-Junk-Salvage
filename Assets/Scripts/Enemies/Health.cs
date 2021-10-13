using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP;

    public float MaxHP;

    public GameObject DT;

    GameObject Camera;

    public EnemyHPBar HPBar;

    public BackstabChecker BSC;

    public GameObject Player;

    [Min(1)]
    public float BackStabMultiplier;

    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int d)
    {
        hit = true;
        int Damage = d;

        if (BSC.PlayerIsBehind == true)
        {

            Damage = (int)(d * BackStabMultiplier);
        }

        HP -= Damage;

        HPBar.gameObject.SetActive(true);

        Vector3 DTSP = transform.position + new Vector3(2, 1.5f, -2);

        Quaternion Q = new Quaternion();
        Q.eulerAngles =new Vector3(0, -45, 0);

        GameObject DamTXT = Instantiate(DT, DTSP, Q);
        DamTXT.GetComponent<DamageText>().dam = Damage;

        HPBar.UpdateHP(HP / MaxHP);

        if(HP <= 0)
        {
            Death();
        }

    }

    public void Death()
    {
        //do other death management stuff
        Destroy(gameObject);

    }
}
