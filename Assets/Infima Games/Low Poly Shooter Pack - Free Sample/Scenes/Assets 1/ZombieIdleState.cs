using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{

    Transform player;
    float timer;
    public float idleTime=0f;
    public float detectionAreaRaduis = 18f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player=GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Transition to Patrol State
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatroling", true);
        }

        //  Transition to Chase State 

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRaduis)
        {
            animator.SetBool("isChasing", true);
        }


    }


    

  
}
