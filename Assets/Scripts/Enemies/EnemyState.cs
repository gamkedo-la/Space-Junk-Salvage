using System;
using UnityEngine;
using UnityEngine.Events;

/*
 * Holds the state of an enemy in a separate component that can be accessed and modified by different behaviours
 * 
 */
public class EnemyState : MonoBehaviour
{
    // States
    public bool Alerted;
    public bool SeePlayer;
    public bool Attacking;
    public bool Chasing;
    public bool Pausing;
    public bool Retreating;
    
    // Locations
    public Vector3 PlayerLastLocation = Vector3.zero;
    public Vector3 AlertLoc;
    
    // References
    public GameObject Player;
}