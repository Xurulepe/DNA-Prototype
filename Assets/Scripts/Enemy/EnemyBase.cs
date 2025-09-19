using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _moveSpeed;
    protected float _currentHealth;

    [Header("Enemy Behaviour")]
    [SerializeField] protected AttackType _weaknessToAttackType;
    [SerializeField] protected State _currentState;

    [Header("Enemy Attack")]
    [SerializeField] protected float _detectionRange;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackCooldown;

    [Header("Enemy Rewards")]
    [SerializeField] protected float _experiencePoints;
    [SerializeField] protected GameObject _deathDrop;

    protected enum State
    {
        Patroling,
        Chasing,
        Attacking
    }

    protected virtual void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void Start()
    {
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
        
    }

    protected virtual void Chase()
    {
        
    }

    protected virtual void Attack()
    {
        
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
}
