using UnityEngine;

public class SwitchActivated : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Switch>().Activate();
    }
}