using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed;

    public float timer;
    float InternalChronometer;

    public int Combo;

    public int dam;

    public bool DashAttack;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
        InternalChronometer = timer;

        dam += (int)(dam * (Combo-1) * .1f);

        if(DashAttack == true)
        {
            dam += (int)(dam * .3f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (InternalChronometer > .2f)
        {
            transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        }
        InternalChronometer -= Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit " + collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(dam);

        }

    }
}
