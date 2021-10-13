using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float ViewRadius;
    [Range(0,360)]
    public float ViewAngle;

    public float PeripheralRadius;
    [Range(0, 360)]
    public float PeripheralAngle;

    public GameObject Player;

    public bool CanSeePlayer = false;

    public Vector3 DirectionToPlayer;

    public BasicEnemyMovement movement;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<BasicEnemyMovement>().Player = Player;
        movement = GetComponent<BasicEnemyMovement>();
    }

    public Vector3 DirectionFromAngle(float AngleInDesgrees, bool AngleInsGlobal)
    {
        if(AngleInsGlobal == false)
        {
            AngleInDesgrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(AngleInDesgrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDesgrees * Mathf.Deg2Rad));

    }

    private void Update()
    {
        CanSeePlayer = false;
        movement.SeePlayer = false;

        if (Vector3.Distance(transform.position, Player.transform.position) < PeripheralRadius)
        {
            DirectionToPlayer = (Player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirectionToPlayer) < PeripheralAngle / 2)
            {
                RaycastHit raycastHit;
                bool didHit = Physics.Raycast(transform.position, DirectionToPlayer, out raycastHit, PeripheralRadius);

                if (didHit == true && raycastHit.collider.gameObject.tag == "Player")
                {
                    if (Mathf.Abs(raycastHit.collider.transform.position.y - transform.position.y) <= 1.3)
                    {

                        CanSeePlayer = true;
                        movement.SeePlayer = true;
                        movement.PlayerLastLocation = Player.transform.position;
                    }
                }
            }

        }
        else if (Vector3.Distance(transform.position, Player.transform.position) < ViewRadius)
        {
            DirectionToPlayer = (Player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirectionToPlayer) < ViewAngle / 2)
            {
                RaycastHit raycastHit;
                bool didHit = Physics.Raycast(transform.position, DirectionToPlayer, out raycastHit, ViewRadius);

                if(didHit == true && raycastHit.collider.gameObject.tag == "Player")
                {
                    CanSeePlayer = true;
                    movement.SeePlayer = true;
                    movement.PlayerLastLocation = Player.transform.position;
                }
            }
        }

    }

}
