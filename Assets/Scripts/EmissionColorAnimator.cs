using System.Collections.Generic;
using UnityEngine;

public class EmissionColorAnimator : MonoBehaviour
{
    [Tooltip("Selection between the two colors over time")]
    public AnimationCurve curve;

    [ColorUsage(true, true)]
    public Color lowColor;
    [ColorUsage(true, true)]
    public Color highColor;
    [Tooltip("Use emission color from material as High Color instead")]
    public bool useEmissionColorFromMaterial;

    private readonly List<Animation> _activeAnimations = new List<Animation>();
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    public void Animate(Material material, float duration, bool reverse = false)
    {
        _activeAnimations.Add(new Animation()
        {
            Material = material,
            LowColor = lowColor,
            HighColor = useEmissionColorFromMaterial ? material.GetColor(EmissionColor) : highColor,
            Duration = duration,
            Current = 0f,
            Reverse = reverse
        });
    }

    public void SetColorFromTime(Material material, float time)
    {
        var t = curve.Evaluate(time);
        var color = Color.LerpUnclamped(lowColor,
            useEmissionColorFromMaterial ? material.GetColor(EmissionColor) : highColor, t);
        material.SetColor(EmissionColor, color);
    }

    private void Update()
    {
        var n = _activeAnimations.Count;

        for (var i = 0; i < n; i++)
        {
            var activeAnimation = _activeAnimations[i];
            activeAnimation.Current += Time.deltaTime;
            var time = Mathf.Clamp01(activeAnimation.Current / activeAnimation.Duration);
            if (activeAnimation.Reverse)
            {
                time = 1f - time;
            }
            
            var t = curve.Evaluate(time);
            var color = Color.LerpUnclamped(activeAnimation.LowColor, activeAnimation.HighColor, t);
            activeAnimation.Material.SetColor(EmissionColor, color);
            _activeAnimations[i] = activeAnimation;
        }

        for (var i = n - 1; i >= 0; i--)
        {
            if (_activeAnimations[i].Current >= _activeAnimations[i].Duration)
            {
                _activeAnimations.RemoveAt(i);
            }
        }
    }

    private struct Animation
    {
        public Material Material;
        public Color LowColor;
        public Color HighColor;
        public float Duration;
        public float Current;
        public bool Reverse;
    }

}