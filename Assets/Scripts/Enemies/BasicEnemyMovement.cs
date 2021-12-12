using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BasicEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
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

    public Vector3 PlayerLastLocation = new Vector3(0,0,0);

    public bool Attacking;

    public float AttackRange;

    private float AttackDuration;
    private float ADreset;

    public GameObject Player;

    public Health myHealth;

    public float GetHitStunTimer;
    private float GHSreset;

    public BackstabChecker BSC;

    public bool hitstun = false;

    public TextMeshPro AlertText;

    public bool Alerted;
    public Vector3 AlertLoc;

    public float height;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PPreset = PatrolPause;
        CPreset = ChasePause;
        //AttackDuration = GetComponent<BasicEnemyAttack>().WindUp + GetComponent<BasicEnemyAttack>().Active + GetComponent<BasicEnemyAttack>().Ending;
        //ADreset = AttackDuration;
        GHSreset = GetHitStunTimer;

        agent.updateRotation = false;

        height = GetComponent<NavMeshAgent>().height / 2;

        AlertText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();

        if(myHealth.hit == true)
        {
            agent.updateRotation = false;

            if (BSC.PlayerIsBehind == true)
            {
                hitstun = true;
                GetHitStunTimer = GHSreset;
            }
            else
            {
                hitstun = true;
                GetHitStunTimer = GHSreset * .5f;
            }

            myHealth.hit = false;

            AlertText.text = "* * *";

        }

        if(hitstun == true)
        {
            Alerted = false;
            agent.updateRotation = false;
            GetHitStunTimer -= Time.deltaTime;

            agent.SetDestination(transform.position);


            if(GetHitStunTimer <= 0)
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
            agent.updateRotation = false;
            AttackDuration -= Time.deltaTime;
            if(AttackDuration <= 0)
            {
                Attacking = false;
                pausing = false;
                //agent.updateRotation = true;
            }
            return;

        }

        if(SeePlayer == true)
        {
            Alerted = false;
            PlayerLastLocation.y = transform.position.y;
            if (Vector3.Distance(transform.position, PlayerLastLocation) < AttackRange)
            {
                Attacking = true;
                pausing = true;
                chasing = false;
                agent.SetDestination(transform.position);
                agent.updateRotation = false;

                transform.LookAt(Player.transform, Vector3.up);

                AttackDuration = ADreset;
                BroadcastMessage("Attack");
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
            agent.updateRotation = false;

            AlertText.text = "!";

            PlayerLastLocation.y = transform.position.y;

            if (SeePlayer == true)
            {
                transform.LookAt(PlayerLastLocation, Vector3.up);
            }
            agent.speed = ChaseSpeed;

            Vector3 temp = PlayerLastLocation - transform.position;

            temp.Normalize();

            temp *= AttackRange;

            temp += PlayerLastLocation;

            

            agent.SetDestination(temp);

            if(SeePlayer == false)
            {
                //AlertText.text = " ";

                if (Vector3.Distance(transform.position, PlayerLastLocation) < ChaseSpeed * Time.deltaTime && pausing == false)
                {
                    pausing = true;

                }
                else if(pausing == true)
                {
                    ChasePause -= Time.deltaTime;

                    agent.SetDestination(transform.position);

                    if (ChasePause <= 0)
                    {
                        ChasePause = CPreset;

                        agent.updateRotation = true;

                        pausing = false;
                        chasing = false;

                    }

                }
            }

        }
        else
        {
            if (Alerted == true)
            {
                AlertText.text = "!";
                agent.speed = ChaseSpeed;
                agent.SetDestination(AlertLoc);
                agent.updateRotation = true;

                if (Vector3.Distance(transform.position, AlertLoc) < ChaseSpeed * Time.deltaTime && pausing == false)
                {

                    pausing = true;
                }
                else if (pausing == true)
                {
                    ChasePause -= Time.deltaTime;

                    agent.SetDestination(transform.position);

                    if (ChasePause <= 0)
                    {
                        ChasePause = CPreset;

                        agent.updateRotation = true;

                        pausing = false;
                        Alerted = false;

                    }

                }

            }
            else
            {
                agent.updateRotation = true;
                AlertText.text = " ";

                agent.speed = PatrolSpeed;

                agent.SetDestination(PatrolPoints[PBookmark]);

                //transform.LookAt(PatrolPoints[PBookmark], Vector3.up);

                if (Vector3.Distance(transform.position, PatrolPoints[PBookmark]) < PatrolSpeed * Time.deltaTime && pausing == false)
                {
                    pausing = true;
                }
                else if (pausing == true)
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

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool("Alerted", Alerted);
            animator.SetBool("Attacking", Attacking);
            animator.SetBool("SeePlayer", SeePlayer);
        }
    }

    public void Alert(Vector3 Location, float h)
    {
        Alerted = true;
        AlertLoc = Location;

        AlertLoc.y -= h;
        AlertLoc.y += height;

    }

    public void SetAttackDuration(float D)
    {
        AttackDuration = D;
        ADreset = AttackDuration;
    }

}
