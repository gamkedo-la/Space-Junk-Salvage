using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject s;

    public int Attacking = 0;
    public bool actionable = true;

    public int ActionQueued = 0;

    public float Timer;

    float Arc = 60;
    float SwingSpeed = 300;
    float SwingTime = .7f;
    float ChainWindow = .3f;
    float EndSwingTime = 1.0f;
    int damage = 15;

    public bool CanMove = true;

    public float DashAttackTimer;
    float DATReset;

    public bool DashCombo;

    // Start is called before the first frame update
    void Start()
    {
        DATReset = DashAttackTimer;
        DashAttackTimer = 0;
    }

    public void ResetDashAttackTimer()
    {
        DashAttackTimer = DATReset;
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;

        DashAttackTimer -= Time.deltaTime;

        if (Attacking != 0)
        {
            Timer -= Time.deltaTime;
        }
        if(Timer <= ChainWindow && Attacking < 3)
        {
            actionable = true;
        }
        if (Timer <= 0f && ActionQueued == 1)
        {
            Quaternion Q = transform.rotation;

            Q.eulerAngles += new Vector3(0, -Arc, 0);

            GameObject SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
            SWORD.GetComponent<Sword>().speed = -SwingSpeed;
            SWORD.GetComponent<Sword>().timer = SwingTime;
            SWORD.GetComponent<Sword>().Combo = 2;
            SWORD.GetComponent<Sword>().dam = damage;

            if(DashCombo == true)
            {
                SWORD.GetComponent<Sword>().DashAttack = true;
            }

            actionable = false;
            CanMove = false;
            Attacking = 2;
            ActionQueued = 0;

            Timer = SwingTime;
        }

        if (Timer <= 0f && ActionQueued == 2)
        {

            Quaternion Q = transform.rotation;

            Q.eulerAngles += new Vector3(0, Arc, 0);

            GameObject SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
            SWORD.GetComponent<Sword>().timer = EndSwingTime;
            SWORD.GetComponent<Sword>().speed = SwingSpeed;
            SWORD.GetComponent<Sword>().Combo = 3;
            SWORD.GetComponent<Sword>().dam = damage;

            if (DashCombo == true)
            {
                SWORD.GetComponent<Sword>().DashAttack = true;
            }

            actionable = false;
            CanMove = false;
            Attacking = 3;
            ActionQueued = 0;

            Timer = EndSwingTime;



        }
        else if (Timer <= 0f)
        {
            ActionQueued = 0;
            Attacking = 0;
            actionable = true;
            CanMove = true;
        }

        if (gamepad.buttonWest.isPressed)
        {
            if (Attacking == 0 && actionable == true)
            {
                Quaternion Q = transform.rotation;

                Q.eulerAngles += new Vector3(0, Arc, 0);

                GameObject SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
                SWORD.GetComponent<Sword>().timer = SwingTime;
                SWORD.GetComponent<Sword>().speed = SwingSpeed;
                SWORD.GetComponent<Sword>().Combo = 1;
                SWORD.GetComponent<Sword>().dam = damage;

                if(DashAttackTimer > 0)
                {
                    SWORD.GetComponent<Sword>().DashAttack = true;
                    DashCombo = true;
                }
                else
                {
                    DashCombo = false;
                }

                actionable = false;
                CanMove = false;
                Attacking = 1;

                Timer = SwingTime;


                

            }
            if (Attacking == 1 && actionable == true && ActionQueued == 0)
            {

                ActionQueued = 1;
             

            }
            if (Attacking == 2 && actionable == true)
            {
                ActionQueued = 2;

            }
        }
       


    }
}
