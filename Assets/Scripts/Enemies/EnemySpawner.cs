using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> ThisRoomEnemies;

    private List<GameObject> LivingEnemies;
    private List<SpawnPoint> spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject e in ThisRoomEnemies)
        {
            SpawnPoint sp = new SpawnPoint
            {
                Enemy = e,
                P = e.transform.position
            };

            spawnPoints.Add(sp);

            e.SetActive(false);

            SpawnEnemies();
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

            GameObject A = Instantiate(sp.Enemy, sp.P, Quaternion.identity);
            LivingEnemies.Add(A);
        }


    }

    [System.Serializable]
    public struct SpawnPoint
    {
        public GameObject Enemy;
        public Vector3 P;
    }

}
