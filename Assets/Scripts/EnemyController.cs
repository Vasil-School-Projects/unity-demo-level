using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxWalkDistance = 20f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.hasPath && agent.remainingDistance > 1f) return;
        agent.SetDestination(GetRandomPointOnMap());
    }

    private Vector3 GetRandomPointOnMap()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxWalkDistance;
        NavMesh.SamplePosition(transform.position + randomDirection, out NavMeshHit hit, maxWalkDistance, NavMesh.AllAreas);
        return hit.position;
    }
}
