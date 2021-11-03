using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCamera : MonoBehaviour
{
    Transform Player;

    Vector3 V = new Vector3(0,0,0);

    bool MapOpen;

    public float MapSens = 1.0f;

    public GameObject MapRenderTexture;

    public int MapEdgeTop = 100;
    public int MapEdgeBot = -100;
    public int MapEdgeLeft = -100;
    public int MapEdgeRight = 100;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(MapOpen == true)
        {
            Vector3 temp = transform.position;
            temp += V * Time.unscaledDeltaTime * MapSens;

            if(temp.x > MapEdgeRight)
            {
                temp.x = MapEdgeRight;
            }
            if (temp.x < MapEdgeLeft)
            {
                temp.x = MapEdgeLeft;
            }
            if (temp.z > MapEdgeTop)
            {
                temp.z = MapEdgeTop;
            }
            if (temp.z < MapEdgeBot)
            {
                temp.z = MapEdgeBot;
            }
                                 
            transform.position = temp;
        }
    }

    public void OpenMap()
    {
        transform.position = Player.transform.position;
        MapOpen = true;
        MapRenderTexture.SetActive(true);

        Time.timeScale = 0;

    }

    public void CloseMap()
    {
        //transform.position = Player.transform.position;
        MapOpen = false;
        MapRenderTexture.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        V = new Vector3(input.x, 0, input.y);

        V = Quaternion.AngleAxis(-45, Vector3.up) * V;


    }

    public void OnOpenCloseMap()
    {
        if(MapOpen == true)
        {
            CloseMap();
        }
        else
        {
            OpenMap();
        }
    }


}
