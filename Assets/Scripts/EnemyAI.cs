using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] KillCount target;
    [SerializeField] float chaseRange = 14.0f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    [SerializeField] bool isProvoked = false;
    bool startTimeCheck = false;
    float startTime;
    float timeLimit = 7f;
    [SerializeField] float elapsedTime = 0f;
    [SerializeField] float turnSpeed = 5f;
    EnemyHealth enemyHealth; 


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<KillCount>();
    }

    void Update()
    {
        if (PauseMenu.isGamePaused) return;

        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
            elapsedTime += Time.deltaTime;
            //print(elapsedTime);
        }

        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            startTimeCheck = true;
        }
        
        if (elapsedTime >= timeLimit)
        {
            elapsedTime = 0f;
            isProvoked = false;
            GetComponent<Animator>().SetBool("Move", false);
        }
    }

    public void OnDamageTaken()
    {
	    isProvoked = true;
        startTimeCheck = true;
    }


    void EngageTarget()
    {
        if (!enemyHealth.isDead) LookAtTarget();

        else if (enemyHealth.isDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

        if ((distanceToTarget > navMeshAgent.stoppingDistance))
        {
            ChaseTarget();
        }

        else if ((distanceToTarget <= navMeshAgent.stoppingDistance))
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.transform.position);
        
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetBool("Move", true);
    }

    private void AttackTarget()
    {
        Animator weaponBoop = GetComponentInChildren<Animator>();        
        weaponBoop.SetBool("Attack", true);
    }

    private void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color (1, 0.3f, 0.3f, 0.6f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
