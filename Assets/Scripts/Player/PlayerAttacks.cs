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

    bool StartAttack;

    public Sword SWORD;

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

        if (Timer <= 0f && ActionQueued == 1)
        {
            Quaternion Q = transform.rotation;

            Q.eulerAngles += new Vector3(0, -Arc, 0);
            /*
            SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
            SWORD.GetComponent<Sword>().speed = -SwingSpeed;
            SWORD.GetComponent<Sword>().timer = SwingTime;
            SWORD.GetComponent<Sword>().myPlayerAttacks = this;
            */

            SWORD.Combo = 2;
            SWORD.dam = damage;
            if (DashCombo == true)
            {
                SWORD.DashAttack = true;
            }

            actionable = false;
            CanMove = false;
            Attacking = 2;
            ActionQueued = 0;

            Timer = SwingTime;
            Debug.Log("Triggering attack 2");
        }

        if (Timer <= 0f && ActionQueued == 2)
        {
            Quaternion Q = transform.rotation;

            Q.eulerAngles += new Vector3(0, Arc, 0);

            /*
            SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
            SWORD.GetComponent<Sword>().timer = EndSwingTime;
            SWORD.GetComponent<Sword>().speed = SwingSpeed;
            SWORD.GetComponent<Sword>().myPlayerAttacks = this;
            */

            SWORD.Combo = 3;
            SWORD.dam = damage;
            if (DashCombo == true)
            {
                SWORD.DashAttack = true;
            }

            actionable = false;
            CanMove = false;
            Attacking = 3;
            ActionQueued = 0;

            Timer = EndSwingTime;
            Debug.Log("Triggering attack 3");
        }
        else if (Timer <= 0f)
        {
            ActionQueued = 0;
            Attacking = 0;
            actionable = true;
            CanMove = true;
        }

        if (StartAttack == true)
        {
            StartAttack = false;
            if (Attacking == 0 && actionable == true && GetComponent<PlayerMovement>().CanAttack == true)
            {
                /*
                Quaternion Q = transform.rotation;

                Q.eulerAngles += new Vector3(0, Arc, 0);

                SWORD = Instantiate(s, transform.position, Q, gameObject.transform);
                SWORD.GetComponent<Sword>().timer = SwingTime;
                SWORD.GetComponent<Sword>().speed = SwingSpeed;
                SWORD.GetComponent<Sword>().myPlayerAttacks = this;
                */

                SWORD.Combo = 1;
                SWORD.dam = damage;
                if (DashAttackTimer > 0)
                {
                    SWORD.DashAttack = true;
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
                Debug.Log("Triggering attack 1");
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

    public void OnAttack()
    {
        StartAttack = true;
    }

    public void TakeDamage()
    {
        // TODO: Stop attacking when taking damage?
    }

    public void ComboWindowChanged(bool isOpen)
    {
        actionable = isOpen;
    }

    public void CheckCombo()
    {
        if (ActionQueued > 0)
        {
            Timer = 0f;
        }
    }

    public void EndAttack()
    {
        Timer = 0f;
    }
}