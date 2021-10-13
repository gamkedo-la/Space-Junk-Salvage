using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : MonoBehaviour
{

    public GameObject HitBox;

    public Transform hitboxLocation;

    public float WindUp;
    public float Active;
    public float Ending;

    float fullTime;
    bool W = false;
    bool A = false;
    bool E = false;

    bool attacking = false;

    float timer = 0;

    public int Damage;

    public float KnockbackStrength;

    // Start is called before the first frame update
    void Start()
    {
        fullTime = WindUp + Active + Ending;
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking == true)
        {
            timer -= Time.deltaTime;


            if(A == false)
            {
                A = true;
                GameObject H = Instantiate(HitBox, hitboxLocation.position + (Vector3.up * 1.5f), Quaternion.identity, transform);
                H.GetComponent<BasicEnemyHitbox>().ActiveTime = Active;
                H.GetComponent<BasicEnemyHitbox>().windup = WindUp;
                H.GetComponent<BasicEnemyHitbox>().Damage = Damage;
                H.GetComponent<BasicEnemyHitbox>().Anchor = hitboxLocation;
                H.GetComponent<BasicEnemyHitbox>().Timer = WindUp + Active;
                H.GetComponent<BasicEnemyHitbox>().KnockbackPower = KnockbackStrength;
                H.GetComponent<BasicEnemyHitbox>().Owner = transform;
            }

            if(timer <= 0)
            {
                attacking = false;
                GetComponent<BasicEnemyMovement>().Attacking = false;
            }

        }



    }

    public void Attack()
    {
        timer = fullTime;
        W = true;
        A = false;
        E = false;
        attacking = true;
    }
}
