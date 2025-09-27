using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private LayerMask _targetLayer;

    private int _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(int targetLayerInt, int projectileDamage)
    {
        _targetLayer = targetLayerInt;
        _damage = projectileDamage;
    }

    public void ShootProjectile(Vector3 direction, float speed)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _targetLayer)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.ReceiveAttack(AttackType.Ranged, _damage);
            }
        }

        gameObject.SetActive(false);
    }
}
