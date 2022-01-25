using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float HP;

    public float MaxHP;

    public GameObject DT;

    GameObject Camera;

    public EnemyHPBar HPBar;

    public BackstabChecker BSC;

    public GameObject Player;

    public GameObject TookDamageEffectPrefab;

    [Min(1)]
    public float BackStabMultiplier;

    public bool hit;

    [Tooltip("Destroy GameObject on death")]
    public bool destroyOnDeath = true;

    public UnityEvent onDeath;

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
        // Early exit in case we've already died before, so not to trigger death multiple times
        if (HP <= 0)
        {
            return;
        }
        
        hit = true;
        int Damage = d;

        if (BSC != null)
        {
            if (BSC.PlayerIsBehind == true)
            {

                Damage = (int)(d * BackStabMultiplier);
            }
        }

        HP -= Damage;

        if (TookDamageEffectPrefab)
        {
            GameObject.Instantiate(TookDamageEffectPrefab, transform.position, Quaternion.identity);
        }

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

        if(gameObject.GetComponent<BossMovement>() != null)
        {
            if(HP/MaxHP <= .75 && (HP+Damage)/MaxHP > .75)
            {
                BroadcastMessage("Retreat");
            }
            else if (HP / MaxHP <= .5 && (HP + Damage) / MaxHP > .5)
            {
                BroadcastMessage("Retreat");
            }
            else if (HP / MaxHP <= .25 && (HP + Damage) / MaxHP > .25)
            {
                BroadcastMessage("Retreat");
            }
                                                  
        }

    }

    public void Death()
    {
        onDeath.Invoke();
        if (destroyOnDeath)
        {
            //do other death management stuff
            Destroy(gameObject);
        }

    }
}
