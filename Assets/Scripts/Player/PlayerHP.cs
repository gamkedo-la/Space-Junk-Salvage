using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public StatusBars HPBar;

    public int CurrentHP;
    public int MaxHP;

    public PlayerMovement movement;
    public PlayerAttacks attacks;

    public MeshRenderer mesh;

    public Material Normal;
    public Material Damaged;

    public float InvulnTimer;
    float ITreset;

    public bool invuln = false;

    public string[] GameOver;

    public DialogWindow DG;

    // Start is called before the first frame update
    void Start()
    {
        HPBar.SetMax(MaxHP, true);

        CurrentHP = MaxHP;
        ITreset = InvulnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(invuln == true)
        {
            InvulnTimer -= Time.deltaTime;
            if(InvulnTimer <= 0)
            {
                mesh.material = Normal;
                invuln = false;
            }
        }
    }

    public void TakeDamage(int Damage, float knockbackPower, Vector3 Source, Vector3 contactPoint)
    {
        if (movement.knockback == false)
        {
            movement.ApplyKnockback(Source, knockbackPower);
            attacks.TakeDamage();
            if (Damage > 0)
            {
                GetComponentInChildren<Shield>().RegisterHit(contactPoint);
            }

            CurrentHP -= Damage;

            if (CurrentHP <= 0)
            {

                DG.DisplayDialog(GameOver, true);

            }

            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }

            HPBar.SetFill(CurrentHP);

            if (Damage > 0)
            {
                invuln = true;
                mesh.material = Damaged;
                InvulnTimer = ITreset;
            }
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log("player enter trigger");

    //    if(other.gameObject.tag == "EnemyWeapon")
    //    {
    //        TakeDamage(other.gameObject.GetComponent<BasicEnemyHitbox>().Damage);
    //        movement.ApplyKnockback(other.gameObject.GetComponent<BasicEnemyHitbox>().Owner.position, other.gameObject.GetComponent<BasicEnemyHitbox>().KnockbackPower);
    //    }
    //}




}
