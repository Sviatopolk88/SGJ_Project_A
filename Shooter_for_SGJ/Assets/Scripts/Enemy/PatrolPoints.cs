using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    public Transform CurrentPoint => _patrolPoints[_currentPoint];

    private NavMeshAgent _navMeshAgent;
    private int _currentPoint = 0;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (HasReached(_navMeshAgent))
            _navMeshAgent.SetDestination(GetNext().position);
    }

    public bool HasReached(NavMeshAgent agent)
    {
        if (!agent.pathPending)
        {
            
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("hasPath: " + agent.hasPath);
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("pathPending: " + agent.pathPending);
        }
        return false;
    }
    public Transform GetNext()
    {
        var point = _patrolPoints[_currentPoint];
        _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
        return point;
    }
}
