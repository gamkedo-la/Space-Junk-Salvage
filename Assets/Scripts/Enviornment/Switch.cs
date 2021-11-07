using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Switch : MonoBehaviour
{
    [Tooltip("If enabled, switch is locked after being used once")]
    public bool lockSwitchAfterUse;

    [Tooltip("Event fired when switch is enabled")]
    public UnityEvent onSwitchEnabled;

    [Tooltip("Event fired when switch is disabled")]
    public UnityEvent onSwitchDisabled;

    private Animator animator;
    private bool isLocked;
    private static readonly int Enabled = Animator.StringToHash("Enabled");

    private void Start()
    {
        animator = GetComponent<Animator>();
        isLocked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isLocked)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            var newState = !animator.GetBool(Enabled);

            animator.SetBool(Enabled, newState);

            if (newState)
            {
                onSwitchEnabled.Invoke();
            }
            else
            {
                onSwitchDisabled.Invoke();
            }

            if (lockSwitchAfterUse)
            {
                isLocked = true;
            }
        }
    }
}