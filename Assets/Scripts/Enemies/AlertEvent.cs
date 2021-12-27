using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEvent : MonoBehaviour
{
    public float Radius;

    public float height;



    // Start is called before the first frame update
    void Start()
    {
        RaycastHit down;
        bool didHit = Physics.Raycast(transform.position, Vector3.down, out down, 10.0f);
        height = down.distance;

        Collider[] collisions = Physics.OverlapSphere(transform.position, Radius);

        List<GameObject> AlertedEnemies = new List<GameObject>();

        foreach(Collider c in collisions)
        {
            if(c.gameObject.tag == "Enemy")
            {
                AlertedEnemies.Add(c.gameObject);
            }
        }

        foreach(GameObject E in AlertedEnemies)
        {
            E.GetComponent<EnemyBrain>().Alert(transform.position, height);
        }

        Destroy(gameObject);

    }

}
