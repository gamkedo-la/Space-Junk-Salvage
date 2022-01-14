using System.Collections;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public float cooldownTime = 0.1f;
    
    public float engineOffset = 0.1f;
    public ParticleSystem topEngine;

    private ParticleSystem[] _engines;

    private void Start()
    {
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

    public void FireEngines(float duration)
    {
        StartCoroutine(AnimateEngines(duration + cooldownTime));
    }

    private IEnumerator AnimateEngines(float duration)
    {
        StartEngines();
        yield return new WaitForSeconds(duration);
        StopEngines();
    }
}