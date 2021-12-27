using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyAttack : MonoBehaviour
{
    public GameObject HitBox;

    //public Transform hitboxLocation;

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

    public float HitBoxSpeed = 250;

    public float Arc = 75;

    // Start is called before the first frame update
    void Start()
    {
        fullTime = WindUp + Active + Ending;
    }

    public void CheckAttack(EnemyState state)
    {
        if (attacking == true)
        {
            timer -= Time.deltaTime;


            if (A == false)
            {
                
                A = true;

                Quaternion Q = transform.rotation;

                Q.eulerAngles += new Vector3(0, -Arc, 0);

                GameObject H = Instantiate(HitBox, transform.position + (Vector3.up * 1.5f), Q, transform);
                H.GetComponent<StrongEnemyHitbox>().ActiveTime = Active;
                H.GetComponent<StrongEnemyHitbox>().windup = WindUp;
                H.GetComponent<StrongEnemyHitbox>().Damage = Damage;
                H.GetComponent<StrongEnemyHitbox>().Timer = WindUp + Active;
                H.GetComponent<StrongEnemyHitbox>().KnockbackPower = KnockbackStrength;
                H.GetComponent<StrongEnemyHitbox>().Owner = transform;
                H.GetComponent<StrongEnemyHitbox>().speed = HitBoxSpeed;

                H.transform.localEulerAngles = new Vector3(0, -Arc, 0);


                Q.eulerAngles += new Vector3(0, Arc, 0);

                GameObject HH = Instantiate(HitBox, transform.position + (Vector3.up * 1.5f), Q, transform);
                HH.GetComponent<StrongEnemyHitbox>().ActiveTime = Active;
                HH.GetComponent<StrongEnemyHitbox>().windup = WindUp;
                HH.GetComponent<StrongEnemyHitbox>().Damage = Damage;
                HH.GetComponent<StrongEnemyHitbox>().Timer = WindUp + Active;
                HH.GetComponent<StrongEnemyHitbox>().KnockbackPower = KnockbackStrength;
                HH.GetComponent<StrongEnemyHitbox>().Owner = transform;
                HH.GetComponent<StrongEnemyHitbox>().speed = -HitBoxSpeed;

                HH.transform.localEulerAngles = new Vector3(0, Arc, 0);

            }

            if (timer <= 0)
            {
                attacking = false;
                state.Attacking = false;
            }
        }
        else if (state.Attacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = fullTime;
        W = true;
        A = false;
        E = false;
        attacking = true;
    }
}
