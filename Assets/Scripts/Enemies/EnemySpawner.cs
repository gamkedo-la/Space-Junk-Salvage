using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> LivingEnemies;
    public List<SpawnPoint> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
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

        foreach(SpawnPoint sp in spawnPoints)
        {

            Instantiate(sp.Enemy, sp.P.position, Quaternion.identity);

        }


    }

    [System.Serializable]
    public struct SpawnPoint
    {
        public GameObject Enemy;
        public Transform P;
    }

}
