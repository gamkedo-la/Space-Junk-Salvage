using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    public UnityEvent<EnemyState> behaviours;

    private EnemyState _state;
    private float _height;

    private void Start()
    {
        _state = GetComponent<EnemyState>();
        _height = GetComponent<NavMeshAgent>().height / 2;
    }

    private void FixedUpdate()
    {
        behaviours.Invoke(_state);
    }
    
    public void Alert(Vector3 Location, float h)
    {
        _state.Alerted = true;
        _state.AlertLoc = Location;

        _state.AlertLoc.y -= h;
        _state.AlertLoc.y += _height;
    }
}