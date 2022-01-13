using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyHitbox : MonoBehaviour
{
    public float ActiveTime;
    public float windup;

    public int Damage;

    public Transform Anchor;

    public float Timer;

    public float KnockbackPower;

    public Transform Owner;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ActiveTime + windup);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= ActiveTime)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHP>().TakeDamage(Damage, KnockbackPower, Owner.transform.position, collision.GetContact(0).point);

        }
    }
}
