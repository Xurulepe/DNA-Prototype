using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    public UnityEvent OnHealtChanged;
    public UnityEvent OnDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        OnHealtChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealtChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _currentHealth = 0;
        OnDeath?.Invoke();
    }
}
