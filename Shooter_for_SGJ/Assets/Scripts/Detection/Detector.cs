using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Detector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public Transform CurrentPoint => _patrolPoints[_currentPoint];
    public float DistanceAttack = 3f;
    public float SpeedAttack = 2f;

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Transform _player;

    private NavMeshAgent _navMeshAgent;
    private int _currentPoint = 0;
    private bool _isStalkingPlayer = false;
    private bool _isAttacking = false;
    private EnemyBase _enemy;
    private IHittable _target;
    private float _timerAttack = 0f;

    void Start()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _enemy = GetComponentInParent<EnemyBase>();
        _target = _player.GetComponent<IHittable>();
    }

    private void Update()
    {
        
        if (_isStalkingPlayer)
        {
            _navMeshAgent.SetDestination(_player.position);
            if (_navMeshAgent.remainingDistance < DistanceAttack)
            {
                _timerAttack += Time.deltaTime;
                if (_timerAttack >= SpeedAttack)
                {
                    _target.HitObject(_enemy.Damage);
                    _timerAttack = 0f;
                }
            }
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
                    return true;
                }
            }
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
        _isStalkingPlayer = true;
        detectablePlayer.Detected(gameObject);

        OnGameObjectDetectedEvent?.Invoke(gameObject, detectablePlayer.gameObject);
    }

    public void ReleaseDetection(IDetectableObject detectablePlayer)
    {
        _isStalkingPlayer = false;
        detectablePlayer.DetectionReleased(gameObject);

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
