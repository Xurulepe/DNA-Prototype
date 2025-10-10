using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthSystem))]
public class WhiteBloodCell : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private HealthSystem _healthSystem;

    [Header("AI Settings")]
    [SerializeField] private float _detectionRange;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform _currentTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        GetTargets();
    }

    #region SELECT TARGET
    private void GetTargets()
    {
        _targets = new List<Transform>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRange, _targetLayerMask);

        foreach (var hitCollider in hitColliders)
        {
            _targets.Add(hitCollider.transform);
        }

        if (_targets.Count != 0)
        {
            SelectTarget();
        }
        else
        {
            _currentTarget = null;
        }
    }

    private void SelectTarget()
    {
        _currentTarget = _targets[0].transform;
        float distanceToCurrentTarget = Vector3.Distance(transform.position, _currentTarget.position);

        foreach (var target in _targets)
        {
            float distanceToNextTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToNextTarget < distanceToCurrentTarget)
            {
                _currentTarget = target.transform;
            }
        }
    }
    #endregion
}
