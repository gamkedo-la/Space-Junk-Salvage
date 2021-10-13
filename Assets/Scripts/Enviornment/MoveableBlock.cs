using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    public bool moving;
    Vector3 Startpoint = new Vector3(0, 0, 0);
    Vector3 Endpoint = new Vector3(0, 0, 0);

    Vector3 D = new Vector3(0, 0, 0);

    float t = 0;

    public float speed;

    public float PushTimer = .5f;

    float PTReset;


    public bool touching = false;
    // Start is called before the first frame update
    void Start()
    {
        PTReset = PushTimer;
    }

    // Update is called once per frame
    void Update()
    {        
        if(moving == true)
        {
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(Startpoint, Endpoint, t * speed);

            if(t * speed >= 1)
            {
                moving = false;
            }
        }
    }

    void MoveBlock(Vector3 Direction)
    {
        Vector3 temp = transform.position;
        temp.y -= .8f;

        RaycastHit raycastHit;
        bool didHit = Physics.Raycast(temp, Direction, out raycastHit, 2.8f);
        if(didHit == true)
        {
            //Play a sound effect or something
            return;

        }

        Startpoint = transform.position;
        Endpoint = transform.position + Direction;
        t = 0;
        moving = true;

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touching = true;
            if(collision.gameObject.GetComponent<PlayerMovement>().Moving == true)
            {

                PushTimer -= Time.deltaTime;

                if (PushTimer <= 0 && moving == false)
                {
                    D = transform.position - collision.gameObject.transform.position;
                    D.Normalize();
                    D *= 2;

                    if (Mathf.Abs(D.x) > Mathf.Abs(D.z))
                    {
                        D.z = 0;
                    }
                    else
                    {
                        D.x = 0;
                    }

                    D.x = (int)D.x;
                    D.z = (int)D.z;

                    D *= 2;

                    D.y = 0;

                    MoveBlock(D);
                }
            }
            else
            {
                PushTimer = PTReset;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PushTimer = PTReset;
            touching = false;
        }
    }

}
