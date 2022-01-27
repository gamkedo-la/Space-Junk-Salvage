using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMover : MonoBehaviour
{
    public Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRoom(int position)
    {

        Vector3 M = positions[position] - transform.position;

        GetComponent<EnemySpawner>().MoveRoom(M);

        transform.position = positions[position];



    }
}
