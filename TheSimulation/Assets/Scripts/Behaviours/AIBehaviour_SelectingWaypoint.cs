using UnityEngine;

public class AIBehaviour_SelectingWaypoint : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AIController ai = animator.GetComponent<AIController>();
        ai.SetNextPoint();
    }

}
