using UnityEngine;

public class EnemyShooter : EnemyBase
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private int _projectileDamage = 10;
    [SerializeField] private int _enemyLayerInt;

    protected override void Attack()
    {
        base.Attack();

        GameObject projectile = ProjectilePool.Instance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = _shootPoint.transform.position;
            projectile.transform.rotation = _shootPoint.transform.rotation;
            projectile.SetActive(true);

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Setup(_enemyLayerInt, _projectileDamage);
            projectileScript.ShootProjectile(_shootPoint.forward, _projectileSpeed);
        }
    }

    protected override void Die()
    {
        GameObject manaRegen = ManaRegenPool.Instance.GetPooledObject();

        if (manaRegen != null)
        {
            manaRegen.transform.position = transform.position;
            manaRegen.transform.rotation = Quaternion.identity;
            manaRegen.SetActive(true);
        }

        base.Die();
    }

    protected override void HandleRetreatState()
    {
        if (Vector3.Distance(transform.position, _target.position) >= _retreatRange)
        {
            _currentState = State.Chasing;
        }
        else
        {
            Retreat();
        }
    }

    protected override void Retreat()
    {
        _navMeshAgent.isStopped = false;
        Vector3 directionAwayFromPlayer = (transform.position - _target.position).normalized;
        Vector3 retreatPosition = transform.position + directionAwayFromPlayer;
        _navMeshAgent.SetDestination(retreatPosition);
    }
}
