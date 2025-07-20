
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float PatrolingTime = 10f;
    public float detectionArea = 18f;
    public float patrolSpeed = 2f;
    public Transform player;
    NavMeshAgent agent;
    List<Transform> way=new List<Transform>();
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent=animator.GetComponent<NavMeshAgent>();
        agent.speed=patrolSpeed;
        timer = 0;
        // get all way and move to first way
        GameObject waypoints = GameObject.FindGameObjectWithTag("Waypoints");
        foreach(Transform t in waypoints.transform)
        {
            way.Add(t);
        }
        Vector3 nextPosition = way[Random.Range(0, way.Count)].position;
        agent.SetDestination(nextPosition);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SoundManager.Instance.zombiechannel.isPlaying == false)
        {
            SoundManager.Instance.zombiechannel.clip = SoundManager.Instance.zombieWalking;
            SoundManager.Instance.zombiechannel.PlayDelayed(1f);
        }
        // if arrived to way point ,move to next point
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(way[Random.Range(0, way.Count)].position);
        }

        //  Transition to Idle state
        timer += Time.deltaTime;
        if (timer > PatrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }
        //transition to Chase state
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        SoundManager.Instance.zombiechannel.Stop();
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
}
