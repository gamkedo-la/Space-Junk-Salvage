using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SelectionBase]
public class FlyingEnemy : MonoBehaviour
{

    public bool SeePlayer = false;

    public Vector3[] PatrolPoints;
    private int PBookmark = 0;

    public float PatrolSpeed;
    public float ChaseSpeed;

    public float PatrolPause;
    private float PPreset;
    public bool pausing = false;

    public float ChasePause;
    private float CPreset;
    public bool chasing = false;

    public Vector3 PlayerLastLocation = new Vector3(0, 0, 0);

    public bool Attacking;

    public float AttackRange;

    public float AttackDuration;
    private float ADreset;

    public GameObject Player;

    public Health myHealth;

    public float GetHitStunTimer;
    private float GHSreset;

    public bool hitstun = false;

    public TextMeshPro AlertText;

    public bool Alerted;
    public Vector3 AlertLoc;

    public float VisionRange = 12.0f;

    public GameObject Bullet;
    public Transform Gun;

    public GameObject Alert;

    [Header( "bullet properties")]
    public int Damage;

    public float KnockbackStrength;

    public float bulletLife;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {

        PPreset = PatrolPause;
        CPreset = ChasePause;
        GHSreset = GetHitStunTimer;
        ADreset = AttackDuration;


        AlertText.text = " ";

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Temp = Player.transform.position;
        Temp.y = transform.position.y;

        if(myHealth.hit == true)
        {
            myHealth.hit = false;
            AlertText.text = "* * *";

            hitstun = true;
            GetHitStunTimer = GHSreset;
        }

        if(hitstun == true)
        {
            Alerted = false;

            GetHitStunTimer -= Time.deltaTime;


            if (GetHitStunTimer <= 0)
            {
                AlertText.text = "a";

                hitstun = false;
                transform.LookAt(Player.transform.position, Vector3.up);
                PatrolPause = PPreset;
                pausing = true;

            }
            return;
        }


        if(Attacking == true)
        {
            Alerted = false;
            AttackDuration -= Time.deltaTime;
            if(AttackDuration <= 0)
            {
                Attacking = false;
                AttackDuration = ADreset;
            }
            return;
        }

        if(Vector3.Distance(transform.position, Temp) <= VisionRange)
        {
            PlayerLastLocation = Temp;
            if (Vector3.Distance(transform.position, Temp) <= AttackRange)
            {
                pausing = true;
                chasing = false;
                Attack();
                return;
            }
            else
            {
                chasing = true;
            }
        }

        if(chasing == true)
        {
            Alerted = false;

            AlertText.text = "!";
            transform.position = Vector3.MoveTowards(transform.position, PlayerLastLocation, ChaseSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, PatrolPoints[PBookmark]) < .1)
            {
                pausing = true;
            }
            if(pausing == true)
            {
                ChasePause -= Time.deltaTime;

                if (ChasePause <= 0)
                {
                    ChasePause = CPreset;
                                       
                    pausing = false;
                    chasing = false;

                }
            }
        }

        else
        {


            if (Alerted == true)
            {
                AlertText.text = "!";
                Vector3 ATemp = AlertLoc;
                Temp.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, ATemp, ChaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, ATemp) < ChaseSpeed * Time.deltaTime && pausing == false)
                {

                    pausing = true;
                }
                if (pausing == true)
                {
                    ChasePause -= Time.deltaTime;

                    if (ChasePause <= 0)
                    {
                        ChasePause = CPreset;

                        pausing = false;
                        Alerted = false;

                    }

                }

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[PBookmark], PatrolSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, PatrolPoints[PBookmark]) < PatrolSpeed * Time.deltaTime && pausing == false)
                {
                    pausing = true;
                }
                if (pausing == true)
                {
                    PatrolPause -= Time.deltaTime;
                    if (PatrolPause <= 0)
                    {
                        PatrolPause = PPreset;
                        PBookmark++;
                        if (PBookmark >= PatrolPoints.Length)
                        {
                            PBookmark = 0;
                        }
                        pausing = false;

                    }
                }
            }

        }

    }

    void Attack()
    {
        Attacking = true;
        AttackDuration = ADreset;

        GameObject B = Instantiate(Bullet, Gun.position, Quaternion.identity);
        B.transform.LookAt(Player.transform);
        B.GetComponent<EnemyBullet>().SetParameters(Damage, bulletSpeed, KnockbackStrength, bulletLife);
    }
}
