using UnityEngine;
using UnityEngine.AI;

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

    public Vector3 Center = new Vector3(0,0,0);

    public int retreatCounter = 0;

    public int shotCounter = 0;

    [Header("First Retreat")] public ParticleSystem steamExhaust;

    [Header("Second Retreat")] public ParticleSystem sparks;

    [Header("Third Retreat")] public ParticleSystem fluidLeak;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //AttackDuration = GetComponent<BasicEnemyAttack>().WindUp + GetComponent<BasicEnemyAttack>().Active + GetComponent<BasicEnemyAttack>().Ending;
        //ADreset = AttackDuration;

        agent.speed = ApproachSpeed;

        //agent.updateRotation = false;

        height = GetComponent<NavMeshAgent>().height / 2;


        Center.y = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();

        if(Retreating == true)
        {
            transform.LookAt(PlayerLastLocation);
            if(Vector3.Distance(Center, transform.position) < .5)
            {
                shooting = true;
                shootDuration = shootDurationReset;
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
            transform.LookAt(Player.transform);

            return;
        }
        else
        {
            agent.SetDestination(PlayerLastLocation);
        }
       
    }

    private void UpdateAnimator()
    {
          animator.SetBool("Attacking", Attacking);
          animator.SetBool("Shooting", shooting);
          animator.SetBool("Retreating", Retreating);
          animator.SetInteger("Stage", retreatCounter);
    }

    public void Retreat()
    {
        Retreating = true;
        agent.SetDestination(Center);
        retreatCounter++;
        shotCounter = 0;
        AddStageEffect(retreatCounter);
    }

    public void Die()
    {
        Attacking = false;
        shooting = false;
        Retreating = false;
        sparks.Stop();
        steamExhaust.Stop();
        fluidLeak.Stop();
        UpdateAnimator();
        enabled = false;
    }

    public void SetAttackWindUp(float t)
    {
        animator.SetFloat("WindUpSpeed", 1f/t);
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
    
    private void AddStageEffect(int stage)
    {
        switch (stage)
        {
            case 1:
                // Start emitting steam
                steamExhaust.Play();
                break;
            case 2:
                // Start the sparks
                sparks.Play();
                break;
            case 3:
                // Start leaking fluid
                fluidLeak.Play();
                break;
        }
    }

}
