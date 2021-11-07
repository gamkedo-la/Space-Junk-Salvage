using UnityEngine;

public class SwitchDeactivated : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Switch>().Deactivate();
    }
}