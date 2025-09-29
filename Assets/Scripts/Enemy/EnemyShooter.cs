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
}
