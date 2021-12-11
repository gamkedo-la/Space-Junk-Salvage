using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BossMovement : MonoBehaviour
{
    public NavMeshAgent agent;


    public float ApproachSpeed;
    public float RetreatSpeed;

    public bool Retreating = false;
    public bool shooting = false;

    public Vector3 PlayerLastLocation = new Vector3(0, 0, 0);

    public bool Attacking;

    public float AttackRange;

    private float AttackDuration;
    private float ADreset;

    private float shootDuration;
    private float shootDurationReset;

    public GameObject Player;

    public Health myHealth;
    
    public float height;

    public Animator animator;

    Vector3 Center;

    public int retreatCounter = 0;

    public int shotCounter = 0;

    


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //AttackDuration = GetComponent<BasicEnemyAttack>().WindUp + GetComponent<BasicEnemyAttack>().Active + GetComponent<BasicEnemyAttack>().Ending;
        //ADreset = AttackDuration;

        agent.speed = ApproachSpeed;

        //agent.updateRotation = false;

        height = GetComponent<NavMeshAgent>().height / 2;


        //change this to handle being somewhere else
        Center = new Vector3(0, transform.position.y, 0);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();

        if(Retreating == true)
        {
            if(Vector3.Distance(Center, transform.position) < .5)
            {
                shooting = true;
                shootDuration = shootDurationReset;
                transform.LookAt(PlayerLastLocation);
                GetComponent<BossAttack>().Shoot();
                shotCounter = 1;
                Retreating = false;
            }
            

            return;
        }

        if(shooting == true)
        {
            agent.updateRotation = false;
            agent.SetDestination(transform.position);
            transform.LookAt(PlayerLastLocation);
            shootDuration -= Time.deltaTime;

            if(shotCounter >= 3)
            {
                shooting = false;
                agent.updateRotation = true;
            }
            else
            {
                if(shootDuration <= 0)
                {
                    shootDuration = shootDurationReset;
                    GetComponent<BossAttack>().Shoot();
                    shotCounter++;
                }
            }


            return;

        }

        if (Attacking == true)
        {

            //agent.updateRotation = false;
            agent.SetDestination(transform.position);
            AttackDuration -= Time.deltaTime;
            if (AttackDuration <= 0)
            {
                Attacking = false;
                //agent.updateRotation = true;
            }
            return;

        }

        PlayerLastLocation = Player.transform.position;
        PlayerLastLocation.y = transform.position.y;

        if(Vector3.Distance(transform.position, PlayerLastLocation) < AttackRange)
        {
            Attacking = true;
            AttackDuration = ADreset;
            BroadcastMessage("Attack");


            return;
        }
        else
        {
            agent.SetDestination(PlayerLastLocation);
        }
       
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
          //  animator.SetBool("Alerted", Alerted);
          //  animator.SetBool("Attacking", Attacking);
          //  animator.SetBool("SeePlayer", SeePlayer);
        }
    }

    public void Retreat()
    {
        Retreating = true;
        agent.SetDestination(Center);
        retreatCounter++;
        shotCounter = 0;

    }

    public void SetAttackDuration(float D)
    {
        AttackDuration = D;
        ADreset = AttackDuration;
    }

    public void SetShotDuration(float D)
    {
        shootDuration = D + .2f;
        shootDurationReset = shootDuration;

    }
}
