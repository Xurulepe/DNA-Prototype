using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private int _projectileDamage = 10;
    [SerializeField] private int _playerLayerInt;

    [Header("Melee Settings")]
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private Vector3 _meleeRange;
    [SerializeField] private int _meleeDamage = 10;
    [SerializeField] private LayerMask _enemyLayer;

    #region SHOOT
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = ProjectilePool.Instance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = _shootPoint.transform.position;
            projectile.transform.rotation = _shootPoint.transform.rotation;
            projectile.SetActive(true);

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Setup(_playerLayerInt, _projectileDamage);
            projectileScript.ShootProjectile(_shootPoint.forward, _projectileSpeed);
        }
    }
    #endregion

    #region SLASH
    public void OnSlash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Slash();
        }
    }

    private void Slash()
    {
        Collider[] hitEnemies = Physics.OverlapBox(_meleeAttackPoint.position, _meleeRange * 0.5f, Quaternion.identity, _enemyLayer);

        foreach (Collider hitEnemy in hitEnemies)
        {
            if (hitEnemy.TryGetComponent(out EnemyBase enemyBase))
            {
                enemyBase.ReceiveAttack(AttackType.Melee, _meleeDamage);
            }
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_meleeAttackPoint.position, _meleeRange);
    }
}

public enum AttackType
{
    Melee,
    Ranged
}
