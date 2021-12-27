using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAttack : MonoBehaviour
{
    public GameObject Bullet;

    public Transform Gun;

    public float AfterShotPause;

    bool W = false;
    bool A = false;
    bool E = false;

    bool attacking = false;

    float timer = 0;

    public int Damage;

    public float KnockbackStrength;

    public float bulletLife;

    public float bulletSpeed;

    public void CheckAttack(EnemyState state)
    {
        if (attacking == true)
        {
            timer -= Time.deltaTime;


            if (A == false)
            {
                A = true;
                GameObject B = Instantiate(Bullet, Gun.position, Gun.rotation);
                B.GetComponent<EnemyBullet>().SetParameters(Damage, bulletSpeed, KnockbackStrength, bulletLife);
                
            }

            if (timer <= 0)
            {
                attacking = false;
                state.Attacking = false;
            }

        }
        else if (state.Attacking == true)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = AfterShotPause;
        W = true;
        A = false;
        E = false;
        attacking = true;
    }
}
