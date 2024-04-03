using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Controller : StateMachineBehaviour
{
    private float changeState;
    private float maxTime = 4;
    private float minTime = 1;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       changeState = Random.Range(minTime, maxTime);       
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        //change State
       if(changeState <= 0){
            animator.SetTrigger("Run");
       } else {
            changeState -= Time.deltaTime;
       }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
