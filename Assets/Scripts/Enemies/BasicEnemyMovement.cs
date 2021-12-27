using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(EnemyState))]
public class BasicEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Vector3[] PatrolPoints;
    private int PBookmark = 0;

    public float PatrolSpeed;
    public float ChaseSpeed;

    public float PatrolPause;
    private float PPreset;

    public float ChasePause;
    private float CPreset;

    public float AttackRange;

    private float AttackDuration;
    private float ADreset;

    public Health myHealth;

    public float GetHitStunTimer;
    private float GHSreset;

    public BackstabChecker BSC;

    public bool hitstun = false;

    public TextMeshPro AlertText;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PPreset = PatrolPause;
        CPreset = ChasePause;
        GHSreset = GetHitStunTimer;

        agent.updateRotation = false;

        AlertText.text = " ";
    }

    public void Move(EnemyState state)
    {
        if (myHealth.hit == true)
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

        if (hitstun == true)
        {
            state.Alerted = false;
            agent.updateRotation = false;
            GetHitStunTimer -= Time.deltaTime;

            agent.SetDestination(transform.position);


            if (GetHitStunTimer <= 0)
            {
                AlertText.text = "a";

                hitstun = false;
                transform.LookAt(state.Player.transform.position, Vector3.up);
                PatrolPause = PPreset;
                state.Pausing = true;
            }

            return;
        }

        if (state.Attacking == true)
        {
            state.Alerted = false;
            agent.updateRotation = false;
            return;
        }

        if (state.SeePlayer == true)
        {
            state.Alerted = false;
            state.PlayerLastLocation.y = transform.position.y;
            if (Vector3.Distance(transform.position, state.PlayerLastLocation) < AttackRange)
            {
                state.Attacking = true;
                state.Pausing = true;
                state.Chasing = false;
                agent.SetDestination(transform.position);
                agent.updateRotation = false;

                transform.LookAt(state.Player.transform, Vector3.up);

                return;
            }
            else
            {
                state.Attacking = false;
                state.Chasing = true;
            }
        }

        if (state.Chasing == true)
        {
            state.Alerted = false;
            agent.updateRotation = false;

            AlertText.text = "!";

            state.PlayerLastLocation.y = transform.position.y;

            if (state.SeePlayer == true)
            {
                transform.LookAt(state.PlayerLastLocation, Vector3.up);
            }

            agent.speed = ChaseSpeed;

            Vector3 temp = state.PlayerLastLocation - transform.position;

            temp.Normalize();

            temp *= AttackRange;

            temp += state.PlayerLastLocation;


            agent.SetDestination(temp);

            if (state.SeePlayer == false)
            {
                //AlertText.text = " ";

                if (Vector3.Distance(transform.position, state.PlayerLastLocation) < ChaseSpeed * Time.deltaTime &&
                    state.Pausing == false)
                {
                    state.Pausing = true;
                }
                else if (state.Pausing == true)
                {
                    ChasePause -= Time.deltaTime;

                    agent.SetDestination(transform.position);

                    if (ChasePause <= 0)
                    {
                        ChasePause = CPreset;

                        agent.updateRotation = true;

                        state.Pausing = false;
                        state.Chasing = false;
                    }
                }
            }
        }
        else
        {
            if (state.Alerted == true)
            {
                AlertText.text = "!";
                agent.speed = ChaseSpeed;
                agent.SetDestination(state.AlertLoc);
                agent.updateRotation = true;

                if (Vector3.Distance(transform.position, state.AlertLoc) < ChaseSpeed * Time.deltaTime && state.Pausing == false)
                {
                    state.Pausing = true;
                }
                else if (state.Pausing == true)
                {
                    ChasePause -= Time.deltaTime;

                    agent.SetDestination(transform.position);

                    if (ChasePause <= 0)
                    {
                        ChasePause = CPreset;

                        agent.updateRotation = true;

                        state.Pausing = false;
                        state.Alerted = false;
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

                if (Vector3.Distance(transform.position, PatrolPoints[PBookmark]) < PatrolSpeed * Time.deltaTime &&
                    state.Pausing == false)
                {
                    state.Pausing = true;
                }
                else if (state.Pausing == true)
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

                        state.Pausing = false;
                    }
                }
            }
        }
    }
}
