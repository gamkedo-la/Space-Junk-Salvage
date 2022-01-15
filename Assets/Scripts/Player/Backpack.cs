using System.Collections;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [Tooltip("Y distance between engine particle systems")]
    public float engineOffset = 0.1f;
    [Tooltip("Top particle system")]
    public ParticleSystem topEngine;
    [Tooltip("Color of an engine that is in cooldown")]
    [ColorUsage(false, true)]
    public Color engineCooldownColor;

    [Tooltip("Controls how fast engine glow fades up or down")]
    public float fadeFactor = 0.5f;

    private ParticleSystem[] _engines;
    private Material _engineMaterial;
    private Color _engineGlowColor;
    private float _currentFade;
    private float _fadeTo;

    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    private void Start()
    {
        _engineMaterial = GetComponent<MeshRenderer>().materials[1];
        _engineGlowColor = _engineMaterial.GetColor(EmissionColor);
        _engines = new[]
        {
            topEngine,
            Instantiate(topEngine, transform),
            Instantiate(topEngine, transform)
        };
        StopEngines();
        FadeEngines(1f);
    }

    private void Update()
    {
        var topEnginePosition = _engines[0].transform.localPosition;
        for (var i = 1; i < 3; i++)
        {
            _engines[i].transform.localPosition = topEnginePosition + i * engineOffset * Vector3.down;
        }

        _currentFade = Mathf.Lerp(_currentFade, _fadeTo, fadeFactor * Time.deltaTime);
        var color = Color.LerpUnclamped(engineCooldownColor, _engineGlowColor, _currentFade);
        _engineMaterial.SetColor(EmissionColor, color);
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

    private void FadeEngines(float to)
    {
        _fadeTo = to;
    }

    private IEnumerator AnimateEngines(float duration, float cooldownTime)
    {
        StartEngines();
        yield return new WaitForSeconds(duration);
        StopEngines();
        FadeEngines(0f);
        yield return new WaitForSeconds(cooldownTime-fadeFactor*0.1f); // fadeFactor*0.1f is really a magic number that just happens to fit
        FadeEngines(1f);
    }
}