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

    [Header("Attack Settings")]
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackCooldown;
    private float _attackTimer;
    [SerializeField] private int _attackDamage;
    private bool _canAttack = true;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        GetTargets();
    }

    private void Update()
    {
        HandleBehaviour();
        AttackCooldown();
    }

    #region HANDLE BEHAVIOUR
    private void HandleBehaviour()
    {
        if (_currentTarget == null || !_currentTarget.gameObject.activeSelf)
        {
            GetTargets();
            return;
        }

        if (Vector3.Distance(transform.position, _currentTarget.position) <= _attackRange)
        {
            if (_canAttack)
            {
                Attack();
            }
        }
        else if (Vector3.Distance(transform.position, _currentTarget.position) <= _detectionRange)
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_currentTarget.position);
    }

    private void Attack()
    {
        _canAttack = false;
        _navMeshAgent.isStopped = true;
        transform.rotation = Quaternion.LookRotation(_currentTarget.position - transform.position);

        Vector3 halfExtends = _attackRange * 0.5f * Vector3.one;
        Collider[] hitEnemies = Physics.OverlapBox(_attackPoint.position, halfExtends, Quaternion.identity, _targetLayerMask);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IAttackable attackable))
            {
                Debug.Log($"{enemy.gameObject.name} Hit by White Blood Cell");
                attackable.ReceiveAttack(AttackType.Melee, _attackDamage);
            }
        }
    }
    #endregion

    private void AttackCooldown()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackCooldown)
        {
            _attackTimer = 0f;
            _canAttack = true;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPoint.position, new Vector3(_attackRange, _attackRange, _attackRange));
    }
}
