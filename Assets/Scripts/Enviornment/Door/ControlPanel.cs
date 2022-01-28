using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{

    public RingMover mover;

    public bool Active = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Active == true)
        {
            mover.ActivateUI();
        }
    }

    public void Activate()
    {
        Active = true;
    }
}
