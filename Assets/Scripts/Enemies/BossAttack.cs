using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject HitBox;

    //public Transform hitboxLocation;

    public float WindUp;
    public float Active;
    public float Ending;
    

    float fullTime;
    bool W = false;
    bool A = false;
    bool AA = false;
    bool E = false;

    bool attacking = false;

    public bool shooting = false;

    public bool aim = false;
    public bool recoil = false;

    


    public float timer = 0;

    public int Damage;

    public float KnockbackStrength;

    public float HitBoxSpeed = 250;

    public float Arc = 75;

    [Header("Shooting Attack")]
    public float aimTime;
    public float recoilTime;
    public int shotDamage;
    public float shotKnockback;
    public float bulletSpeed;
    public float bulletLife;
    public GameObject Bullet;

    public Transform GunA;
    public Transform GunB;
    public Transform GunC;

    float shoottime;

    // Start is called before the first frame update
    void Start()
    {
        fullTime = WindUp + (Active * 2) + Ending;
        shoottime = aimTime + recoilTime;

        BroadcastMessage("SetAttackDuration", fullTime);
        BroadcastMessage("SetShotDuration", shoottime);
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


               

            }
            if(A == true && AA == false && timer <= Active + Ending)
            {
                AA = true;

                Quaternion Q = transform.rotation;

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
                GetComponent<BossMovement>().Attacking = false;
            }

        }

        if(shooting == true)
        {
            timer -= Time.deltaTime;

            if(aim == true)
            {
                aim = false;

                GameObject B = Instantiate(Bullet, GunA.position, GunA.rotation);
                B.GetComponent<EnemyBullet>().SetParameters(shotDamage, bulletSpeed, shotKnockback, bulletLife);

                B = Instantiate(Bullet, GunB.position, GunB.rotation);
                B.GetComponent<EnemyBullet>().SetParameters(shotDamage, bulletSpeed, shotKnockback, bulletLife);

                B = Instantiate(Bullet, GunC.position, GunC.rotation);
                B.GetComponent<EnemyBullet>().SetParameters(shotDamage, bulletSpeed, shotKnockback, bulletLife);




            }
            if(timer <= 0)
            {
                shooting = false;
            }


        }


    }

    public void Attack()
    {
        timer = fullTime;
        W = true;
        A = false;
        AA = false;
        E = false;
        attacking = true;
    }

    public void Shoot()
    {
        timer = shoottime;
        aim = true;
        recoil = false;
        shooting = true;

    }
}
