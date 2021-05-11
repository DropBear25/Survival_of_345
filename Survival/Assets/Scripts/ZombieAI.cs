using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{

    public GameObject target;
    Animator anim;
    NavMeshAgent agent;


    enum STATE { IDLE, WANDER, ATTACK, CHASE, DEAD};
    STATE state = STATE.IDLE; 



    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();

        anim.SetBool("isWalking", true);
      
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }
         
    }
}
