using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    int myDamage;
    float mySpeed;
    float myKnockback;
    float myLifetime;

    public Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetParameters(int damage, float speed, float knockback, float lifetime)
    {
        myDamage = damage;
        mySpeed = speed;
        myLifetime = lifetime;
        myKnockback = knockback;

        Destroy(gameObject, lifetime);

        Vector3 V = transform.forward * mySpeed;

        myRigidbody.velocity = V;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHP>().TakeDamage(myDamage, myKnockback, transform.position);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
