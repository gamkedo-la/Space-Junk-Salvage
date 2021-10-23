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

    // Start is called before the first frame update
    void Start()
    {

        BroadcastMessage("SetAttackDuration", AfterShotPause);
    }

    // Update is called once per frame
    void Update()
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
                GetComponent<BasicEnemyMovement>().Attacking = false;
            }

        }



    }

    public void Attack()
    {
        timer = AfterShotPause;
        W = true;
        A = false;
        E = false;
        attacking = true;
    }
}
