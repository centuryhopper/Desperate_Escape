using UnityEngine;
using UnityEngine.AI;

public class SimpleAIMovementTest : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    void Update()
    {

        agent.SetDestination(target.position);

    }

}