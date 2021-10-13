using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackstabChecker : MonoBehaviour
{

    [Range(0, 360)]
    public float BackstabAngle;

    public float backstabRadius = 5;

    public GameObject Player;

    public bool PlayerIsBehind = false;

    public Vector3 DirectionToPlayer;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    public Vector3 DirectionFromAngle(float AngleInDesgrees, bool AngleInsGlobal)
    {
        if (AngleInsGlobal == false)
        {
            AngleInDesgrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(AngleInDesgrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDesgrees * Mathf.Deg2Rad));

    }


    // Update is called once per frame
    void Update()
    {

        DirectionToPlayer = (Player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, DirectionToPlayer) > BackstabAngle / 2)
        {

            PlayerIsBehind = true;

        }
        else
        {
            PlayerIsBehind = false;
        }
             
    }
}
