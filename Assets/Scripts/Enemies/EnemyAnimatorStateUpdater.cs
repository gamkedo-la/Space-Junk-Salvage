using UnityEngine;

public class EnemyAnimatorStateUpdater : MonoBehaviour
{
    public Animator animator;
    private static readonly int Alerted = Animator.StringToHash("Alerted");
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int SeePlayer = Animator.StringToHash("SeePlayer");
    private static readonly int Chasing = Animator.StringToHash("Chasing");
    private static readonly int Pausing = Animator.StringToHash("Pausing");
    private static readonly int Retreating = Animator.StringToHash("Retreating");

    public void UpdateAnimator(EnemyState state)
    {
        animator.SetBool(Alerted, state.Alerted);
        animator.SetBool(Attacking, state.Attacking);
        animator.SetBool(SeePlayer, state.SeePlayer);
        animator.SetBool(Chasing, state.Chasing);
        animator.SetBool(Pausing, state.Pausing);
        animator.SetBool(Retreating, state.Retreating);
    }
}