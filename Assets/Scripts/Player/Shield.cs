using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Shield : MonoBehaviour
{
    [Header("Idle animation")] [Range(0.1f, 10f)]
    public float idleSweepTime = 1f;

    [Range(0, 1)] public float idleSweepWidth = 0.05f;
    [Range(0, 1)] public float idleNoiseMagnitude = 0.87f;
    [Range(0, 1)] public float idleOpacity = 0.25f;
    [Range(0, 1)] public float idleChance = 0.1f;

    [Header("Hit animation")] [Range(0.1f, 10f)]
    public float hitSweepTime = 1f;

    [Range(0, 1)] public float hitSweepWidth = 0.05f;
    [Range(0, 1)] public float hitNoiseMagnitude = 0.87f;
    [Range(0, 1)] public float hitOpacity = 0.25f;

    private bool _wasHit;
    private Vector3 _hitPoint;
    private Material _material;
    private Coroutine _sweep;
    private bool _isSweep;
    private static readonly int Sweep = Shader.PropertyToID("_Sweep");
    private static readonly int SweepWidth = Shader.PropertyToID("_SweepWidth");
    private static readonly int NoiseMagnitude = Shader.PropertyToID("_NoiseMagnitude");
    private static readonly int Opacity = Shader.PropertyToID("_Opacity");

    public void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.SetFloat(Sweep, -1f);
    }

    private void Update()
    {
        if (_wasHit)
        {
            if (_isSweep)
            {
                StopCoroutine(_sweep);
            }

            _isSweep = true;
            _sweep = StartCoroutine(DoSweep(hitSweepWidth, hitNoiseMagnitude, hitOpacity, hitSweepTime,
                Quaternion.LookRotation(_hitPoint - transform.position)));
            _wasHit = false;
        }

        if (!_isSweep && Random.value < idleChance * Time.deltaTime)
        {
            _isSweep = true;
            _sweep = StartCoroutine(DoSweep(idleSweepWidth, idleNoiseMagnitude, idleOpacity, idleSweepTime,
                Random.rotationUniform));
        }
    }

    private IEnumerator DoSweep(float sweepWidth, float noiseMagnitude, float opacity, float sweepTime,
        Quaternion rotation)
    {
        yield return new WaitForSeconds(Random.value * 0.1f);
        // Turn to a random direction
        transform.rotation = rotation;
        // Set values
        _material.SetFloat(SweepWidth, sweepWidth);
        _material.SetFloat(NoiseMagnitude, noiseMagnitude);
        _material.SetFloat(Opacity, opacity);

        var startTime = Time.time;

        while (Time.time - startTime < sweepTime)
        {
            var t = Time.time - startTime;
            var sweep = Mathf.Lerp(-sweepWidth, 2, t / sweepTime);
            _material.SetFloat(Sweep, sweep);
            yield return null;
        }

        _isSweep = false;
    }

    public void RegisterHit(Vector3 hitPoint)
    {
        _wasHit = true;
        _hitPoint = hitPoint;
    }
}