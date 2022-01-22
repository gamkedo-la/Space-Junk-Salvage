using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{

    public Image image;
    [FormerlySerializedAs("dashReady")] public Sprite ready;
    [FormerlySerializedAs("dashActive")] public Sprite active;
    [FormerlySerializedAs("dashCooldown")] public Sprite cooldown;

    [Tooltip("Extra duration to show the active image")]
    public float extraActiveDuration;

    public AnimationCurve alphaCurve;

    public int numberOfCooldownBlinks;

    float total;
    float counter;
    float alpha;

    void Update()
    {
        counter += Time.deltaTime;

        if (counter < 0)
        {
            image.sprite = active;
            alpha = 1;
        }
        else if(counter >= total)
        {
            image.sprite = ready;
            alpha = 1;
        }
        else
        {
            alpha = alphaCurve.Evaluate(Mathf.PingPong((numberOfCooldownBlinks*2+1) * counter / total, 1f));
            image.sprite = cooldown;
        }

        Color c = image.color;
        c.a = alpha;
        image.color = c;

    }

    public void ActivateAndCooldown(float cooldown, float duration)
    {
        duration += extraActiveDuration;
        total = cooldown-duration;
        counter = -duration;
        image.sprite = active;
    }
}
