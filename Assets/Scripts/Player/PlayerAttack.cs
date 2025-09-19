using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private int _enemyLayer;

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
            projectileScript.Setup(_enemyLayer);
            projectileScript.ShootProjectile(_shootPoint.forward, _projectileSpeed);
        }
    }
    #endregion
}
