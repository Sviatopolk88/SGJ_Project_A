using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public GameObject[] detectedObjects => _detectedObjects.ToArray();

    [SerializeField] private Transform player;

    private NavMeshAgent _navMeshAgent;

    private List<GameObject> _detectedObjects = new List<GameObject>();
    private bool _flag = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_flag)
            _navMeshAgent.SetDestination(player.position);
    }

    public void Detect(IDetectableObject detectableObject)
    {
        if (!_detectedObjects.Contains(detectableObject.gameObject))
        {
            detectableObject.Detected(gameObject);
            _detectedObjects.Add(detectableObject.gameObject);

            OnGameObjectDetectedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    public void ReleaseDetection(IDetectableObject detectableObject)
    {
        if (_detectedObjects.Contains(detectableObject.gameObject))
        {
            detectableObject.DetectionReleased(gameObject);
            _detectedObjects.Remove(detectableObject.gameObject);

            OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedObject))
        {
            Detect(detectedObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedObject))
        {
            ReleaseDetection(detectedObject);
        }
    }
    private bool IsColliderDetectableObject(Collider coll, out IDetectableObject detectedObject)
    {
        detectedObject = coll.GetComponent<IDetectableObject>();

        return detectedObject != null;
    }

}
