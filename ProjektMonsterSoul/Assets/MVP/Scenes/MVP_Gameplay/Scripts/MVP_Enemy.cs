using UnityEngine;
using UnityEngine.AI;

public class MVP_Enemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(player.position);
    }
}
