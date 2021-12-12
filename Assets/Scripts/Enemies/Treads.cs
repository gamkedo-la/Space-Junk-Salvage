using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Treads : MonoBehaviour
{
    [Tooltip("How far to offset the texture when the treads have moved one Unity unit")]
    public float texOffsetPerUnit;

    private MeshRenderer _renderer;
    private float _offset = 0f;
    private Vector3 _lastPosition;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _lastPosition = transform.position;
    }

    private void Update()
    {
        var p = transform.position;
        var fw = transform.forward;
        
        // Find out how far we've moved, and if it's forward or back
        var movement = p - _lastPosition;
        var mode = Vector3.Dot(movement, fw);

        var movedDistance = movement.magnitude * Mathf.Sign(mode);
        
        _lastPosition = p;
        
        _offset = Mathf.Repeat(_offset + movedDistance * texOffsetPerUnit, 1f);
        _renderer.material.mainTextureOffset = new Vector2(_offset, 0f);
    }
}
