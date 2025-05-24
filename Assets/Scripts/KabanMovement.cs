using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KabanMovement : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent mutantAgent;
    public Transform [] patrolPoints;
    private int currentPatrolIndex;
    private Transform playerPos;

    private UnityEngine.AI.NavMeshAgent NMAgent;
    private Animator mutantAnimator;
    // Start is called before the first frame update
    void Start()
    {
        NMAgent = GetComponent<NavMeshAgent>();
        mutantAnimator = GetComponent<Animator>();
        playerPos = GameObject.Find("Player").transform;
        mutantAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(patrolPoints.Length > 0){
            currentPatrolIndex = 0;
            MoveToNextPatrolPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerPos.position, transform.position);
        if(distanceToPlayer > 10f)
        {
            PatrolBehaviour();
        }
        else if(distanceToPlayer <= 10f)
        {
            StalkBehaviour();
        }
        WalkAnimation();
        AttackAnimation();
    }
    public bool IsAgentMoving(NavMeshAgent NMAgent){
        return NMAgent.velocity.sqrMagnitude > 0.1f && NMAgent.remainingDistance > NMAgent.stoppingDistance;
    }
    private void MoveToNextPatrolPoint(){
        if(patrolPoints.Length == 0) return;

        mutantAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
    private void PatrolBehaviour(){
        if(!mutantAgent.pathPending && mutantAgent.remainingDistance < 0.5f){
            MoveToNextPatrolPoint();
        }
    }
    private void StalkBehaviour(){
        mutantAgent.destination = playerPos.position;
    }
    private void WalkAnimation(){
        if(IsAgentMoving(NMAgent)){
            mutantAnimator.SetBool("isWalk", true);
        }
        else{
            mutantAnimator.SetBool("isWalk", false);
        }
    }
    public void AttackAnimation(){
        if(Input.GetMouseButtonDown(0)){
            int tempRand = Random.Range(0,2);
            mutantAnimator.Play("ATTACK");
        }
    }
}
