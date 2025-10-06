using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthSystem))]
public class EnemyBase : MonoBehaviour, IAttackable
{
    [Header("Enemy Stats")]
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _moveSpeed;
    protected HealthSystem _healthSystem;

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
    private float _attackTimer;
    private bool _canAttack = true;

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
        Attacking,
        Retreating
    }

    protected void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    protected virtual void OnEnable()
    {
        _healthSystem.SetMaxHealth(_maxHealth);
        _currentState = State.Patroling;
    }

    protected virtual void Start()
    {
        _healthSystem.OnDeath.AddListener(Die);
        _target = GameManager.Instance.GetPlayerTransform();
        _currentState = State.Patroling;
    }

    protected virtual void Update()
    {
        HandleStates();
        AttackCooldown();
    }

    #region State Handling
    private void HandleStates()
    {
        switch (_currentState)
        {
            case State.Patroling:
                HandlePatrolState();
                break;
            case State.Chasing:
                HandleChaseState();
                break;
            case State.Attacking:
                HandleAttackState();
                break;
            case State.Retreating:
                HandleRetreatState();
                break;
        }
    }

    protected virtual void HandlePatrolState()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _detectionRange)
        {
            _currentState = State.Chasing;
        }
        else
        {
            Patrol();
        }
    }

    protected virtual void HandleChaseState()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _attackRange)
        {
            _currentState = State.Attacking;
        }
        else
        {
            Chase();
        }
    }

    protected virtual void HandleAttackState()
    {
        if (_canAttack)
        {
            Attack();
        }
        else if (Vector3.Distance(transform.position, _target.position) > _attackRange)
        {
            _currentState = State.Chasing;
        }
    }

    protected virtual void HandleRetreatState()
    {
        
    }
    #endregion

    #region States Actions
    protected virtual void Patrol()
    {

    }

    protected virtual void Chase()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_target.position);
    }

    protected virtual void Attack()
    {
        _canAttack = false;
        _navMeshAgent.isStopped = true;
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
    }

    protected virtual void Retreat()
    {

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

    #region Receive Damage and Death
    public virtual void ReceiveAttack(AttackType attackType, int damage)
    {
        if (attackType == _weaknessToAttackType)
        {
            TakeDamage(damage);
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        _healthSystem.TakeDamage(damage);
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        gameObject.SetActive(false);
    }
    #endregion

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}

