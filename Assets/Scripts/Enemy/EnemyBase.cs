using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _moveSpeed;
    protected float _currentHealth;

    [Header("Enemy Behaviour")]
    [SerializeField] protected AttackType _weaknessToAttackType;
    [SerializeField] protected State _currentState;
    [SerializeField] protected EnemyType _enemyType;

    [Header("AI Settings")]
    [SerializeField] protected Transform _target;
    [SerializeField] protected NavMeshAgent _navMeshAgent;

    [Header("Enemy Attack")]
    [SerializeField] protected float _detectionRange;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackCooldown;

    [Header("Enemy Rewards")]
    [SerializeField] protected float _experiencePoints;
    [SerializeField] protected GameObject _deathDrop;

    public enum EnemyType
    {
        Melee,
        Shooter
    }

    protected enum State
    {
        Patroling,
        Chasing,
        Attacking
    }

    protected void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void Start()
    {
        _target = GameManager.Instance.GetPlayerTransform();
        _currentState = State.Patroling;
    }

    protected virtual void Update()
    {
        switch (_currentState)
        {
            case State.Patroling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
        }
    }

    protected virtual void Patrol()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _detectionRange)
        {
            _currentState = State.Chasing;
        }
    }

    protected virtual void Chase()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_target.position);

        if (Vector3.Distance(transform.position, _target.position) <= _attackRange)
        {
            _currentState = State.Attacking;
        }
    }

    protected virtual void Attack()
    {
        _navMeshAgent.isStopped = true;
        // Attack
    }

    public virtual void ReceiveAttack(float damage, AttackType attackType)
    {
        if (attackType == _weaknessToAttackType)
        {
            TakeDamage(damage);
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}

