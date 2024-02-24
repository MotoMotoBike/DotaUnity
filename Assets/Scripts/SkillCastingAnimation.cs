using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastingAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    EntityView _view;
    bool casted = false;

    [SerializeField]
    [Range(0,1f)] 
    float AnimationNormalizedTime = 1;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        casted = false;
        if(_view == null) _view = animator.gameObject.GetComponent<EntityView>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (casted) return;
        Debug.Log(stateInfo.normalizedTime);
        if (stateInfo.normalizedTime >= AnimationNormalizedTime)
        {
            casted = true;
            animator.SetBool("isRunning", true);
            _view.CastingFinished();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
