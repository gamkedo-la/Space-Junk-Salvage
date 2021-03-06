using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FloorManager : MonoBehaviour
{

    public List<Tile> tiles;

    public int cX;
    public int cZ;

    public GameObject FloorTilePrefab;
    public GameObject[] floorTilePrefabs;

    [System.Serializable]
    public struct Tile
    {
        public GameObject T;
        public int X;
        public int Z;
    }



    // Start is called before the first frame update
    void Start()
    {
        if (floorTilePrefabs == null || floorTilePrefabs.Length == 0)
        {
            floorTilePrefabs = new[] {FloorTilePrefab};
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFloorSize(int nX, int nZ)
    {
        //EditorUtility.SetDirty(this);

        if (cX > nX)
        {
            foreach (Tile tile in tiles.ToArray())
            {
                if (tile.X > nX)
                {

                    DestroyImmediate(tile.T);
                    tiles.Remove(tile);
                }
            }
            cX = nX;

        }
        if (cZ > nZ)
        {
            foreach (Tile tile in tiles.ToArray())
            {
                if (tile.Z > nZ)
                {

                    DestroyImmediate(tile.T);
                    tiles.Remove(tile);


                }
            }
            cZ = nZ;

        }



        for (int i = 1; i <= nX; i++)
        {

            if (i <= cX)
            {
                for (int j = cZ + 1; j <= nZ; j++)
                {
                    Tile t = new Tile();

                    Debug.Log("new floor tile at " + i.ToString() + " x " + j.ToString() + " z");

                    GameObject prefab = floorTilePrefabs[Random.Range(0, floorTilePrefabs.Length)];

                    t.T = Instantiate(prefab, transform);
                    t.X = i;
                    t.Z = j;

                    t.T.name = t.X.ToString() +" x " + t.Z.ToString() + " z";

                    t.T.transform.localPosition = new Vector3((6 * t.X) - 3, 0, -((6 * t.Z) - 3));
                    t.T.transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);

                    tiles.Add(t);
                }
            }
            else
            {

                for (int j = 1; j <= nZ; j++)
                {
                    Tile t = new Tile();

                    Debug.Log("new floor tile at " + i.ToString() + " x " + j.ToString() + " z");

                    GameObject prefab = floorTilePrefabs[Random.Range(0, floorTilePrefabs.Length)];

                    t.T = Instantiate(prefab, transform);
                    t.X = i;
                    t.Z = j;

                    t.T.name = t.X.ToString() + " x " + t.Z.ToString() + " z";

                    t.T.transform.localPosition = new Vector3((6 * t.X) - 3, 0, -((6 * t.Z) - 3));
                    t.T.transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);

                    tiles.Add(t);
                }

            }




        }

        cX = nX;
        cZ = nZ;


    }

}
