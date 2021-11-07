using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class Switch : MonoBehaviour
{
    [Min(0.1f)] [Tooltip("The time it takes to activate or deactivate the switch, in seconds")]
    public float operationTime = 1f;
    
    [FormerlySerializedAs("lockSwitchAfterUse")] [Tooltip("If enabled, switch is locked after being activated")]
    public bool lockSwitchActive;

    [FormerlySerializedAs("onSwitchEnabled")] [SerializeField] [Tooltip("Event fired when switch is enabled")]
    private UnityEvent onSwitchActivated;

    [FormerlySerializedAs("onSwitchDisabled")] [SerializeField] [Tooltip("Event fired when switch is disabled")]
    private UnityEvent onSwitchDeactivated;

    private Animator animator;
    private bool isLocked;
    private static readonly int Operating = Animator.StringToHash("Operating");
    private static readonly int OperationSpeed = Animator.StringToHash("OperationSpeed");

    private void Start()
    {
        animator = GetComponent<Animator>();
        isLocked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isLocked && collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool(Operating, true);
            animator.SetFloat(OperationSpeed, 1f/operationTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool(Operating, false);
        }
    }

    public void Activate()
    {
        if (lockSwitchActive)
        {
            isLocked = true;
        }

        animator.SetBool(Operating, false);
        onSwitchActivated.Invoke();
    }

    public void Deactivate()
    {
        animator.SetBool(Operating, false);
        onSwitchDeactivated.Invoke();
    }
}