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

    public Vector3 DirectionToPlayer;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public Vector3 DirectionFromAngle(float AngleInDesgrees, bool AngleInsGlobal)
    {
        if(AngleInsGlobal == false)
        {
            AngleInDesgrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(AngleInDesgrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDesgrees * Mathf.Deg2Rad));

    }

    public void LookForPlayer(EnemyState state)
    {
        state.SeePlayer = false;
        var distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        
        if (distanceToPlayer < PeripheralRadius)
        {
            DirectionToPlayer = (Player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirectionToPlayer) < PeripheralAngle / 2)
            {
                RaycastHit raycastHit;
                bool didHit = Physics.Raycast(transform.position, DirectionToPlayer, out raycastHit, PeripheralRadius);

                if (didHit == true && raycastHit.collider.gameObject.CompareTag("Player"))
                {
                    if (Mathf.Abs(raycastHit.collider.transform.position.y - transform.position.y) <= 1.3)
                    {
                        state.SeePlayer = true;
                        state.PlayerLastLocation = Player.transform.position;
                        state.Player = Player;
                    }
                }
            }
        }
        else if (distanceToPlayer < ViewRadius)
        {
            DirectionToPlayer = (Player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirectionToPlayer) < ViewAngle / 2)
            {
                RaycastHit raycastHit;
                bool didHit = Physics.Raycast(transform.position, DirectionToPlayer, out raycastHit, ViewRadius);

                if (didHit == true && raycastHit.collider.gameObject.CompareTag("Player"))
                {
                    state.SeePlayer = true;
                    state.PlayerLastLocation = Player.transform.position;
                    state.Player = Player;
                }
            }
        }
    }
}
