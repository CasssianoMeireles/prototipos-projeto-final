using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public enum NPCState { Patrol, Alert, Chase }
    public NPCState currentState;
    
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    
    public float alertDistance = 10f;
    public float chaseDistance = 5f;
    public Transform player;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = NPCState.Patrol;
        agent.destination = patrolPoints[currentPatrolPoint].position;
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.Patrol:
                Patrol();
                break;
            case NPCState.Alert:
                Alert();
                break;
            case NPCState.Chase:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f) // Se o NPC chegou ao ponto de patrulha
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolPoint].position;
        }
        
        // Verifica a distância do player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= alertDistance)
        {
            currentState = NPCState.Alert;
        }
    }

    void Alert()
    {
        // Estado de alerta: verifica se o jogador se aproxima mais
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= chaseDistance)
        {
            currentState = NPCState.Chase;
        }
        else if (distanceToPlayer > alertDistance)
        {
            currentState = NPCState.Patrol;
            agent.destination = patrolPoints[currentPatrolPoint].position;
        }
    }

    void Chase()
    {
        // Persegue o player
        agent.destination = player.position;
        
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer > chaseDistance)
        {
            currentState = NPCState.Alert; // Volta ao estado de alerta
        }
    }
}