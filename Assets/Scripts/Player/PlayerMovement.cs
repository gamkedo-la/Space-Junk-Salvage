using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 V = new Vector3(0,0,0);

    public float speed;

    Quaternion Q = new Quaternion();

    public List<GameObject> RaycastDownPoints = new List<GameObject>();

    float direction;

    public bool CanMove = true;

    public bool Moving = false;

    public RaycastHit down;
    public float distanceToGround;

    public float height;

    public float FallSpeed;

    public bool falling = false;

    Rigidbody myRigidbody;

    public bool SwitchingRooms = false;
    public bool jumped = false;

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

    bool StartDash;

    public float DashCooldown;
    float DCReset;

    public GameObject DashParticles;

    public bool CanAttack = true;

    bool MapOpen = false;

    public DashCooldownUI cool;
    public float MovementPercentage => V.magnitude / speed;

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

    private void FixedUpdate()
    {
        DashCooldown -= Time.deltaTime;

        falling = true;

        myRigidbody.velocity = Vector3.zero;

        foreach (GameObject G in RaycastDownPoints)
        {
            bool didHit = Physics.Raycast(G.transform.position, Vector3.down, out down, 10.0f);

            if (down.distance <= height)
            {
                falling = false;
            }
        }


        if (SwitchingRooms == true)
        {
            CanAttack = false;
            myRigidbody.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, OtherRoom.position, speed * Time.deltaTime);

            if (jumped == true)
            {
                if (Vector3.Distance(transform.position, OtherRoom.position) < speed * Time.deltaTime)
                {
                    SwitchingRooms = false;
                    jumped = false;
                }
            }
            return;
        }
        else if (falling == true)
        {
            CanAttack = false;
            bool didHitAgain = Physics.Raycast(transform.position, Vector3.down, out down, 10.0f);
            if (didHitAgain)
            {
                distanceToGround = down.distance;
            }

            if ((FallSpeed * Time.deltaTime) >= distanceToGround)
            {
                V.y = -(distanceToGround - height);

                falling = false;
            }
            else
            {
                V.y -= (FallSpeed * Time.deltaTime);
            }

            Vector3 temp = transform.position;
            temp.y += V.y;
            transform.position = temp;

            V.y = 0;

            return;
        }
        else if (knockback == true)
        {
            CanAttack = false;
            transform.position = Vector3.Lerp(tempPosition, KnockbackDestination, t);

            t += (Time.deltaTime * KBSpeed);

            if (t >= 1)
            {
                knockback = false;
            }

            return;
        }
        else if(MapOpen == true)
        {
            return;
        }
        else if (GetComponent<PlayerAttacks>().CanMove == true && falling == false)
        {
            if (StartDash == true)
            {


                CanAttack = false;
                StartDash = false;
                if (dashing == false && DashCooldown <= 0)
                {
                    dashing = true;
                    if (cool != null)
                    {
                        cool.Dash(DCReset);
                    }


                    DashDestination = transform.position + transform.forward * DashLength;
                    DashStart = transform.position;
                    t = 0;

                    Quaternion Q = transform.rotation;
                    Q.eulerAngles = new Vector3(Q.eulerAngles.x, Q.eulerAngles.y + 180, Q.eulerAngles.z);

                    Instantiate(DashParticles, transform.position, Q, transform);
                    
                    GetComponentInChildren<Backpack>().FireEngines(1f/DashSpeed);
                }
            }
            else if (dashing == true)
            {
                CanAttack = false;
                transform.position = Vector3.Lerp(DashStart, DashDestination, t);
                t += Time.deltaTime * DashSpeed;

                if (t >= 1)
                {
                    dashing = false;
                    DashCooldown = DCReset;
                    GetComponent<PlayerAttacks>().ResetDashAttackTimer();
                }
            }
            else
            {
                CanAttack = true;
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

                transform.rotation = Q;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        V = new Vector3(input.x, 0, input.y);

        V = Quaternion.AngleAxis(-45, Vector3.up) * V;


    }

    public void OnDash()
    {
        StartDash = true;
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
            E.GetComponent<EnemyBrain>().Alert(transform.position, height);
        }
    }

    public void OnOpenCloseMap()
    {
        if (MapOpen == true)
        {
            MapOpen = false;
            return;
        }
        else
        {
            MapOpen = true;
            return;
        }
    }
}
