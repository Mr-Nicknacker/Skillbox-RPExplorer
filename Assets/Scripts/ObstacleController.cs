using System.Collections.Generic;
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _pathContainer;

    private Transform[] _pathPoints;
    private List<Transform> _pathPointsList;

    private const float PROXIMITY_OFFSET = 0.2f;
    private float _offsetSquare;
    private int _pointIndex = 0;

    private Vector3 _nextPoint;

    private bool _isMovingBack = false;
    private bool _hasEnoughPoints;

    private void Awake()
    {
        _offsetSquare = PROXIMITY_OFFSET * PROXIMITY_OFFSET;
        _pathPoints = _pathContainer.GetComponentsInChildren<Transform>();
        _pathPointsList = GetPathPoints();
        _nextPoint = GetNextPoint(_pathPointsList);
        _hasEnoughPoints = GetPathPoints().Count > 1;
    }
    private void FixedUpdate()
    {
        Vector3 direction;
        float sqrDistance;

        direction = _nextPoint - transform.position;
        sqrDistance = direction.sqrMagnitude;

        HandleMovement(_nextPoint);
        if (_hasEnoughPoints && sqrDistance <= _offsetSquare)
        {
            _nextPoint = GetNextPoint(_pathPointsList);
        }
    }
    private List<Transform> GetPathPoints()
    {
        List<Transform> pathPointsList=new(_pathPoints.Length - 1);

        foreach (Transform point in _pathPoints)
        {
            if (_pathContainer.transform == point)
            {
                continue;
            }
            pathPointsList.Add(point);
        }
        return pathPointsList;
    }
    private Vector3 GetNextPoint(List<Transform> pathPointsList)
    {
        if (_pointIndex == 0)
        {
            _isMovingBack = false;
        }
        else if (_pointIndex >= pathPointsList.Count)
        {
            _isMovingBack = true;
            _pointIndex = pathPointsList.Count - 2;

        }
        Vector3 nextPoint = pathPointsList[_pointIndex].position;
        switch (_isMovingBack)
        {
            case false:
                _pointIndex++;
                break;
            case true:
                _pointIndex--;
                break;
        }
        return nextPoint;
    }
    private void HandleMovement(Vector3 targetPoint)
    {
        float moveSpeed = _movementSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed);
    }
}
