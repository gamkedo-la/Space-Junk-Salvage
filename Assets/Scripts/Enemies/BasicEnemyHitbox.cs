using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHitbox : MonoBehaviour
{
    public float ActiveTime;
    public float windup;

    public int Damage;

    public Transform Anchor;

    public float Timer;

    public float KnockbackPower;

    public Transform Owner;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ActiveTime + windup);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer <= ActiveTime)
        {
            transform.position = Anchor.position;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHP>().TakeDamage(Damage, KnockbackPower, Owner.transform.position);

            }
    }
}
