using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> ThisRoomEnemies;

    private List<GameObject> LivingEnemies = new List<GameObject>();
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();


    // Start is called before the first frame update
    void Start()
    {
        if (ThisRoomEnemies.Count > 0)
        {
            foreach (GameObject e in ThisRoomEnemies)
            {
                SpawnPoint sp = new SpawnPoint
                {
                    Enemy = e,
                    P = e.transform.localPosition
                };

                spawnPoints.Add(sp);

                e.SetActive(false);

                SpawnEnemies();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SpawnEnemies()
    {
        foreach(GameObject e in LivingEnemies)
        {

            Destroy(e);
            
        }
        LivingEnemies.Clear();


        foreach(SpawnPoint sp in spawnPoints)
        {

            GameObject A = Instantiate(sp.Enemy,(transform.position + sp.P), Quaternion.identity, transform);
            A.SetActive(true);
            LivingEnemies.Add(A);
        }


    }


    public void MoveRoom(Vector3 Move)
    {
        foreach(GameObject e in ThisRoomEnemies)
        {
            for(int i= 0; i < e.GetComponent<BasicEnemyMovement>().PatrolPoints.Length; i++)
            {
                e.GetComponent<BasicEnemyMovement>().PatrolPoints[i] += Move;
            }
        }

        SpawnEnemies();


    }

    [System.Serializable]
    public struct SpawnPoint
    {
        public GameObject Enemy;
        public Vector3 P;
    }

}
