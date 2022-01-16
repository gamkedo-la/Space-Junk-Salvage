using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EmissionColorAnimator), typeof(MeshRenderer), typeof(Rigidbody))]
public class EnergyCellRecharge : MonoBehaviour
{
    public float dischargeVelocity;
    public float timeToLand;
    public float timeToSink;
    
    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    public void Deplete(float timeToDeplete)
    {
        StartCoroutine(PlayDepleteAnimation(timeToDeplete));
    }

    private IEnumerator PlayDepleteAnimation(float timeToDeplete)
    {
        GetComponent<EmissionColorAnimator>().Animate(_material, timeToDeplete);
        yield return new WaitForSeconds(timeToDeplete);
        transform.parent = null;
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var c in colliders)
        {
            c.enabled = true;
        }
        rb.AddForce(-dischargeVelocity * transform.up, ForceMode.VelocityChange);
        yield return new WaitForSeconds(timeToLand);
        foreach (var c in colliders)
        {
            c.enabled = false;
        }

        yield return new WaitForSeconds(timeToSink);
        Destroy(gameObject);
    }
}