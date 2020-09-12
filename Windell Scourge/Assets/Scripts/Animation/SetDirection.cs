using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        checkDirection(animator);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        checkDirection(animator);
    }

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

    void checkDirection(Animator animator)
    {
        float x = animator.GetFloat("Horizontal");
        float y = animator.GetFloat("Vertical");
        int direction = 0;
        if (x > 0)
            direction = 3;
        else if (x < 0)
            direction = 1;
        else if (y > 0)
            direction = 2;
        else if (y < 0)
            direction = 0;

        animator.SetInteger("CurrentDir", direction);
    }
}
