using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Tooltip("If an enemy is closer than this, player character will be in alerted idle mode")]
    public float enemyAlertRange = 10f;

    [Header("References")] public PlayerMovement movement;
    public PlayerAttacks attacks;

    [Header("Animation parameters")] [Tooltip("Player model animator")]
    public Animator animator;

    [Tooltip("Time to ramp up animation from idle to moving")]
    public float movingDampTime = 0.05f;

    [Tooltip("Time to ramp down animation from moving to idle")]
    public float stoppingDampTime = 0.25f;

    [Tooltip("Time to change between alerted idle and normal idle")]
    public float alertedDampTime = 0.5f;


    private int _attacking;
    private bool _alerted;

    private static readonly int SpeedProperty = Animator.StringToHash("Speed");
    private static readonly int AttackingProperty = Animator.StringToHash("Attacking");
    private static readonly int AlertedProperty = Animator.StringToHash("Alerted");
    private static readonly int AttackNumberProperty = Animator.StringToHash("AttackNumber");

    private void LateUpdate()
    {
        _alerted = CheckForNearbyEnemies();
        UpdateAnimatorProperties();
    }

    private bool CheckForNearbyEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var playerPosition = transform.position;
        var alertRangeSquared = enemyAlertRange * enemyAlertRange;
        foreach (var enemy in enemies)
        {
            if ((enemy.transform.position - playerPosition).sqrMagnitude < alertRangeSquared)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateAnimatorProperties()
    {
        if (movement.Moving)
        {
            animator.SetFloat(SpeedProperty, movement.MovementPercentage, movingDampTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(SpeedProperty, 0, stoppingDampTime, Time.deltaTime);
        }

        if (attacks.Attacking != _attacking)
        {
            animator.SetInteger(AttackNumberProperty, attacks.Attacking);
            if (attacks.Attacking > 0)
            {
                animator.SetTrigger(AttackingProperty);
            }

            _attacking = attacks.Attacking;
        }

        animator.SetFloat(AlertedProperty, _alerted ? 1f : 0f, alertedDampTime, Time.deltaTime);
    }
}