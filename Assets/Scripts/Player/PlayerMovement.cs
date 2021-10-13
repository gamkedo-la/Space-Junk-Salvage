using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 V = new Vector3(0,0,0);

    public float speed;

    Quaternion Q = new Quaternion();

    Vector3 north = new Vector3(-1, 0, 1);
    Vector3 east = new Vector3(1, 0, 1);
    Vector3 south = new Vector3(1, 0, -1);
    Vector3 west = new Vector3(-1, 0, -1);

    public List<GameObject> RaycastDownPoints = new List<GameObject>();

    float direction;

    public bool Moving = false;

    public RaycastHit down;
    public float distanceToGround;

    public float height;

    public float FallSpeed;

    public bool falling = false;

    Rigidbody myRigidbody;

    public bool SwitchingRooms = false;

    public Transform OtherRoom;

    Vector3 KnockbackDestination;
    Vector3 tempPosition;

    public bool knockback = false;

    float t = 0;

    float KBSpeed = 4;

    public bool dashing = false;

    public float DashSpeed;

    public float DashLength;

    public Vector3 DashDestination;

    Vector3 DashStart;

    public float DashCooldown;
    float DCReset;

    public GameObject DashParticles;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        DCReset = DashCooldown;
        DashCooldown = 0;
    }

    public void ApplyKnockback(Vector3 AttackOrigin, float strength)
    {
        if (strength > 0)
        {

            knockback = true;

            AttackOrigin.y = transform.position.y;

            Vector3 K = transform.position - AttackOrigin;

            K.Normalize();

            K *= strength;

            KnockbackDestination = transform.position + K;

            tempPosition = transform.position;

            t = 0;
        }
    }



    // Update is called once per frame
    void Update()
    {
        DashCooldown -= Time.deltaTime;

        if(SwitchingRooms == true)
        {
            myRigidbody.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, OtherRoom.position, speed * Time.deltaTime/* *.7f*/);

            if(Vector3.Distance(transform.position, OtherRoom.position) < speed * Time.deltaTime/* * .7f*/)
            {
                SwitchingRooms = false;
            }
            return;
        }


        if (knockback == true)
        {
            transform.position = Vector3.Lerp(tempPosition, KnockbackDestination, t);

            t += (Time.deltaTime * KBSpeed);

            if(t >= 1)
            {
                knockback = false;
            }

            return;
        }


        V = Vector3.zero;

        if (GetComponent<PlayerAttacks>().CanMove == true && falling == false)
        {

            if (Input.GetKeyDown(KeyCode.H) && dashing == false && DashCooldown <= 0)
            {
                dashing = true;
                DashDestination = transform.position + transform.forward * DashLength;
                DashStart = transform.position;
                t = 0;

                Quaternion Q = transform.rotation;
                Q.eulerAngles = new Vector3(Q.eulerAngles.x, Q.eulerAngles.y + 180, Q.eulerAngles.z);

                Instantiate(DashParticles, transform.position, Q, transform);


            }

            if (dashing == true)
            {
                transform.position = Vector3.Lerp(DashStart, DashDestination, t);
                t += Time.deltaTime * DashSpeed;

                if(t >= 1)
                {
                    dashing = false;
                    DashCooldown = DCReset;
                    GetComponent<PlayerAttacks>().ResetDashAttackTimer();
                }
            }

            //V.x = Input.GetAxis("Horizontal");
            //V.z = Input.GetAxis("Vertical");

            else
            {
                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    V += north;
                }
                if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    V += south;
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    V += west;
                }
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    V += east;
                }
            }
        }        

        V.Normalize();
        if (V.magnitude > 0)
        {
            Q.SetLookRotation(V, Vector3.up);
            Moving = true;
        }
        else
        {
            Moving = false;
        }
        V *= speed;

        myRigidbody.velocity = V;

        V = Vector3.zero;

        //bool didHit = Physics.Raycast(transform.position, Vector3.down, out down, 5.0f);
        //if (didHit)
        //{
        //    distanceToGround = down.distance;

        //    if (distanceToGround > height + .1f)
        //    {
        //        falling = true;

        //        if (FallSpeed >= distanceToGround)
        //        {
        //            V.y = -(distanceToGround - height);
        //        }
        //        else
        //        {
        //            V.y -= FallSpeed;
        //        }
        //    }
        //    else
        //    {
        //        falling = false;
        //        V.y = -(distanceToGround - height);
        //    }
        //}

        falling = true;

        foreach(GameObject G in RaycastDownPoints)
        {
            bool didHit = Physics.Raycast(G.transform.position, Vector3.down, out down, 10.0f);

            if (down.distance <= height)
            {
                falling = false;
            }
        }


        bool didHitAgain = Physics.Raycast(transform.position, Vector3.down, out down, 10.0f);
        if (didHitAgain)
        {
            distanceToGround = down.distance;
        }

        if (falling == true)
        {
            if ((FallSpeed * Time.deltaTime) >= distanceToGround)
            {
                V.y = -(distanceToGround - height);
            }
            else
            {
                V.y -= (FallSpeed * Time.deltaTime);
            }
        }
        else
        {
            //V.y = -(distanceToGround - height);
        }


        transform.position += V;
        transform.rotation = Q;

    }

    public void Alert(float Radius)
    {
        RaycastHit down;
        bool didHit = Physics.Raycast(transform.position, Vector3.down, out down, 10.0f);
        height = down.distance;

        Collider[] collisions = Physics.OverlapSphere(transform.position, Radius);

        List<GameObject> AlertedEnemies = new List<GameObject>();

        foreach (Collider c in collisions)
        {
            if (c.gameObject.tag == "Enemy")
            {
                AlertedEnemies.Add(c.gameObject);
            }
        }

        foreach (GameObject E in AlertedEnemies)
        {
            E.GetComponent<BasicEnemyMovement>().Alert(transform.position, height);
        }
    }
}
