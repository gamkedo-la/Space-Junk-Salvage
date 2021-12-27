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

    public void CheckAttack(EnemyState state)
    {
        if (attacking)
        {
            timer -= Time.deltaTime;

            if (A == false)
            {
                A = true;
                GameObject H = Instantiate(HitBox, hitboxLocation.position + (Vector3.up * 1.5f), Quaternion.identity,
                    transform);
                var basicEnemyHitbox = H.GetComponent<BasicEnemyHitbox>();
                basicEnemyHitbox.ActiveTime = Active;
                basicEnemyHitbox.windup = WindUp;
                basicEnemyHitbox.Damage = Damage;
                basicEnemyHitbox.Anchor = hitboxLocation;
                basicEnemyHitbox.Timer = WindUp + Active;
                basicEnemyHitbox.KnockbackPower = KnockbackStrength;
                basicEnemyHitbox.Owner = transform;
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