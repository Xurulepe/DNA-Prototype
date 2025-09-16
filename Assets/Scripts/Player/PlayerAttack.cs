using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _projectileSpeed = 20f;

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
        GameObject bullet = ProjectilePool.Instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = _shootPoint.transform.position;
            bullet.transform.rotation = _shootPoint.transform.rotation;
            bullet.SetActive(true);

            bullet.GetComponent<Rigidbody>().linearVelocity = _shootPoint.forward * _projectileSpeed; 
        }
    }
    #endregion
}
