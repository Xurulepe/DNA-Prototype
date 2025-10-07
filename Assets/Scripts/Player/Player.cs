using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(ManaSystem))]
public class Player : MonoBehaviour, IAttackable
{
    [Header("Stats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxMana = 50;
    private HealthSystem _healthSystem;
    private ManaSystem _manaSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _manaSystem = GetComponent<ManaSystem>();

        _healthSystem.SetMaxHealth(_maxHealth);
        _manaSystem.SetMaxMana(_maxMana);
    }

    private void Start()
    {
        _healthSystem.OnDeath.AddListener(Die);
    }

    public void Heal(int amount)
    {
        _healthSystem.Heal(amount);
    }

    public void RegenerateMana(int amount)
    {
        _manaSystem.RegenerateMana(amount);
    }

    public void ReceiveAttack(AttackType attackType, int damage)
    {
        TakeDamage(damage);
    }

    private void TakeDamage(int damage)
    {
        _healthSystem.TakeDamage(damage);
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(gameObject);
        }
    }
}
