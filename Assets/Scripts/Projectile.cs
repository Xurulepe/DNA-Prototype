using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody _rigidbody;
    LayerMask _targetLayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(int targetLayerInt)
    {
        _targetLayer = targetLayerInt;
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
            // aplicar dano ao alvo
            Debug.Log("Hit target: " + other.name);
        }

        Debug.Log("Hit " + other.name);
        gameObject.SetActive(false);
    }
}
