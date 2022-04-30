using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public Transform CurrentPoint => _patrolPoints[_currentPoint];

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Transform player;

    private NavMeshAgent _navMeshAgent;
    private int _currentPoint = 0;
    private bool _flag = false;

    void Start()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_flag)
        {
            _navMeshAgent.SetDestination(player.position);
        }
        else
        {
            if (HasReached(_navMeshAgent))
                _navMeshAgent.SetDestination(GetNext().position);
        }
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



    public void Detect(IDetectableObject detectablePlayer)
    {
        detectablePlayer.Detected(gameObject);
        _flag = true;

        OnGameObjectDetectedEvent?.Invoke(gameObject, detectablePlayer.gameObject);
    }

    public void ReleaseDetection(IDetectableObject detectablePlayer)
    {
        detectablePlayer.DetectionReleased(gameObject);
        Debug.Log(_flag);
        _flag = false;

        OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectablePlayer.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedPlayer))
        {
            Detect(detectedPlayer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedPlayer))
        {
            ReleaseDetection(detectedPlayer);
        }
    }
    private bool IsColliderDetectableObject(Collider coll, out IDetectableObject detectedPlayer)
    {
        detectedPlayer = coll.GetComponent<IDetectableObject>();

        return detectedPlayer != null;
    }

}
