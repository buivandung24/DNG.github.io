using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Run_Controller : StateMachineBehaviour
{
    private float changeState;
    private float maxTime = 4;
    private float minTime = 1;
    public float speed;
    public float attackRange;
    private Transform player;
    private Rigidbody2D rb;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       changeState = Random.Range(minTime, maxTime);
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        //chase
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos =  Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);
        //attack
        if(Vector2.Distance(player.position, rb.position) <= attackRange){
            animator.SetTrigger("Attack");
        }
        //change State
       if(changeState <= 0){
            animator.SetTrigger("Idle");
       } else {
            changeState -= Time.deltaTime;
       }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
