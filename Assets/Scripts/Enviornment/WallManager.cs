using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WallManager : MonoBehaviour
{
    public GameObject[] Segments;

    public GameObject WallSegmentPrefab;

    public int Side;

    public GameObject Room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceSegments()
    {
        //EditorUtility.SetDirty(this);
        Vector3 V = new Vector3(0, 0, 3);
        
        for(int i = 0; i< Segments.Length; i++)
        {
            if(Segments[i] == null)
            {
                Segments[i] = Instantiate(WallSegmentPrefab, transform);

                if(Side == 1 || Side == 2)
                {
                    Segments[i].GetComponent<MeshRenderer>().enabled = false;
                }
            }


            Segments[i].name = (i + 1).ToString();
            Segments[i].transform.localPosition = V;
            Segments[i].transform.localEulerAngles = new Vector3(0, 90, 0);


            if(Segments[i].tag == "Door")
            {
                Vector3 T = V;
                T.x = -2.5f;
                T.y = 0;

                Segments[i].transform.localPosition = T;
                Segments[i].transform.localEulerAngles = new Vector3(0, 90, 0);

                Segments[i].GetComponent<DoorManager>().MyRoom = Room;
            }


            V.z += 6;

        }

    }

}
