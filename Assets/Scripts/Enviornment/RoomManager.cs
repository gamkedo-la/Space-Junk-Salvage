using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public WallManager[] Walls;

    public GameObject Floor;

    public GameObject Corner;

    public GameObject WallSegment;

    [Header("length in 6 unit tiles")]
    public int LengthX = 1;
    public int LengthZ = 1;

    [SerializeField]
    private int LastSizeX = 0;
    [SerializeField]
    private int LastSizeZ = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateSize()
    {

        Walls[0].transform.localPosition = new Vector3(-(3 * LengthX), 0, (3 * LengthZ) + 0.5f);

        Walls[1].transform.localPosition = new Vector3((3 * LengthX) + 0.5f, 0 , (3 * LengthZ));

        Walls[2].transform.localPosition = new Vector3((3 * LengthX), 0, -((3 * LengthZ) + 0.5f));

        Walls[3].transform.localPosition = new Vector3(-((3 * LengthX) + 0.5f), 0, -(3 * LengthZ));


        for (int i = 0; i < Walls.Length; i++)
        {
            if(i == 0 || i == 2) // x length
            {

                if (LengthX != LastSizeX)
                {
                    GameObject[] temp = new GameObject[LengthX];

                    if (LengthX < LastSizeX)
                    {
                        for(int j = 0; j < Walls[i].Segments.Length; j++)
                        {
                            if (j < LengthX)
                            {
                                temp[j] = Walls[i].Segments[j];
                                Walls[i].Segments[j] = null;
                            }
                            else
                            {

                                DestroyImmediate(Walls[i].Segments[j]);
                            }                                                       
                        }

                        foreach(GameObject g in Walls[i].Segments)
                        {
                            DestroyImmediate(g);
                        }

                        Walls[i].Segments = temp;
                        
                    }
                    else if(LengthX > LastSizeX)
                    {
                        for (int j = 0; j < Walls[i].Segments.Length; j++)
                        {
                            if (j < LastSizeX)
                            {
                                temp[j] = Walls[i].Segments[j];
                                Walls[i].Segments[j] = null;
                            }
                            else
                            {
                                temp[j] = Instantiate(WallSegment, Walls[i].transform);
                                if(i == 2)
                                {
                                    temp[j].GetComponent<MeshRenderer>().enabled = false;
                                }

                            }
                        }

                        Walls[i].Segments = temp;
                    }
                }
            }
            else if(i == 1 || i == 3)  // z length
            {

                if (LengthZ != LastSizeZ)
                {
                    GameObject[] temp = new GameObject[LengthZ];

                    if (LengthZ < LastSizeZ)
                    {
                        for (int j = 0; j < Walls[i].Segments.Length; j++)
                        {
                            if (j < LengthZ)
                            {
                                temp[j] = Walls[i].Segments[j];
                                Walls[i].Segments[j] = null;
                            }
                            else
                            {
                                DestroyImmediate(Walls[i].Segments[j]);
                            }
                        }

                        foreach (GameObject g in Walls[i].Segments)
                        {
                            DestroyImmediate(g);
                        }

                        Walls[i].Segments = temp;

                    }
                    else if (LengthZ > LastSizeZ)
                    {
                        for (int j = 0; j < Walls[i].Segments.Length; j++)
                        {
                            if (j < LastSizeZ)
                            {
                                temp[j] = Walls[i].Segments[j];
                                Walls[i].Segments[j] = null;
                            }
                            else
                            {
                                temp[j] = Instantiate(WallSegment, Walls[i].transform);
                                if (i == 1)
                                {
                                    temp[j].GetComponent<MeshRenderer>().enabled = false;
                                }
                            }
                        }

                        Walls[i].Segments = temp;
                    }
                }
            }
            else
            {
                Debug.Log("ERROR: ROOM WITH MORE THAN 4 WALLS " + gameObject.name);
            }




        }

        foreach(WallManager w in Walls)
        {
            w.PlaceSegments();
        }

        Vector3 C = new Vector3(-(LengthX * 3) - 0.5f, 3f, (LengthZ * 3) + 0.5f);
        Corner.transform.localPosition = C;

        Floor.transform.localPosition = new Vector3(-(LengthX * 3), 0, (LengthZ * 3));

        Floor.GetComponent<FloorManager>().UpdateFloorSize(LengthX, LengthZ);

        LastSizeX = LengthX;
        LastSizeZ = LengthZ;

    }
}
