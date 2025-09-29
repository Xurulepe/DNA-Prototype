using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private LayerMask _ignoreLayer;

    private int _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(int ignoreLayerInt, int projectileDamage)
    {
        _ignoreLayer = ignoreLayerInt;
        _damage = projectileDamage;
    }

    public void ShootProjectile(Vector3 direction, float speed)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _ignoreLayer)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.ReceiveAttack(AttackType.Ranged, _damage);
            }

            gameObject.SetActive(false);
        }
    }
}
