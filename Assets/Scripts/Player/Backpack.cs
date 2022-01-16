using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EmissionColorAnimator))]
public class Backpack : MonoBehaviour
{
    [Tooltip("Y distance between engine particle systems")]
    public float engineOffset = 0.1f;
    [Tooltip("Top particle system")]
    public ParticleSystem topEngine;

    private EmissionColorAnimator _emissionColorAnimator;

    private ParticleSystem[] _engines;
    private Material _engineMaterial;

    private void Start()
    {
        _emissionColorAnimator = GetComponent<EmissionColorAnimator>();
        _engineMaterial = GetComponent<MeshRenderer>().materials[1];
        _engines = new[]
        {
            topEngine,
            Instantiate(topEngine, transform),
            Instantiate(topEngine, transform)
        };
        StopEngines();
    }

    private void Update()
    {
        var topEnginePosition = _engines[0].transform.localPosition;
        for (var i = 1; i < 3; i++)
        {
            _engines[i].transform.localPosition = topEnginePosition + i * engineOffset * Vector3.down;
        }
    }

    private void StopEngines()
    {
        foreach (var engine in _engines)
        {
            engine.Stop();
        }
    }

    private void StartEngines()
    {
        foreach (var engine in _engines)
        {
            engine.Play();
        }
    }

    public void FireEngines(float duration, float cooldownTime)
    {
        StartCoroutine(AnimateEngines(duration, cooldownTime));
    }

    private IEnumerator AnimateEngines(float duration, float cooldownTime)
    {
        StartEngines();
        yield return new WaitForSeconds(duration);
        StopEngines();
        _emissionColorAnimator.Animate(_engineMaterial, cooldownTime);
    }
}