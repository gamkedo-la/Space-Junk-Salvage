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

    bool Retreating = false;

    public Vector3 PlayerLastLocation = new Vector3(0, 0, 0);

    public bool Attacking;

    public float AttackRange;

    private float AttackDuration;
    private float ADreset;

    public GameObject Player;

    public Health myHealth;
    
    public float height;

    public Animator animator;

    Vector3 Center;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //AttackDuration = GetComponent<BasicEnemyAttack>().WindUp + GetComponent<BasicEnemyAttack>().Active + GetComponent<BasicEnemyAttack>().Ending;
        //ADreset = AttackDuration;

        agent.speed = ApproachSpeed;

        //agent.updateRotation = false;

        height = GetComponent<NavMeshAgent>().height / 2;

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
                //special attack
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


    }

    public void SetAttackDuration(float D)
    {
        AttackDuration = D;
        ADreset = AttackDuration;
    }
}
