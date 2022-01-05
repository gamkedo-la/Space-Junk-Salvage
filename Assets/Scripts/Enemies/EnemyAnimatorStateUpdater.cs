using System.Linq;
using UnityEngine;

public class EnemyAnimatorStateUpdater : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public Animator[] animators;

    private static readonly int Alerted = Animator.StringToHash("Alerted");
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int SeePlayer = Animator.StringToHash("SeePlayer");
    private static readonly int Chasing = Animator.StringToHash("Chasing");
    private static readonly int Pausing = Animator.StringToHash("Pausing");
    private static readonly int Retreating = Animator.StringToHash("Retreating");

    private void Start()
    {
        // Make the animators array backwards compatible with the animator field
        if (animators == null || animators.Length == 0)
        {
            animators = new[] {animator};
        }
    }

    public void UpdateAnimator(EnemyState state)
    {
        SetBool(Alerted, state.Alerted);
        SetBool(Attacking, state.Attacking);
        SetBool(SeePlayer, state.SeePlayer);
        SetBool(Chasing, state.Chasing);
        SetBool(Pausing, state.Pausing);
        SetBool(Retreating, state.Retreating);
    }

    private void SetBool(int id, bool state)
    {
        foreach (var a in animators)
        {
            // Check if this animator has this parameter before setting it
            if (a.parameters.Any(p => p.nameHash == id))
            {
                a.SetBool(id, state);
            }
        }
    }
}