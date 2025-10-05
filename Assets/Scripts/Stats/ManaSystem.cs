using UnityEngine;
using UnityEngine.Events;

public class ManaSystem : MonoBehaviour
{
    private int _maxMana;
    private int _currentMana;

    public UnityEvent OnManaChanged;

    private void Awake()
    {
        _currentMana = _maxMana;
    }

    public void SetMaxMana(int maxMana)
    {
        _maxMana = maxMana;
        _currentMana = _maxMana;
    }

    public int GetCurrentMana()
    {
        return _currentMana;
    }

    public float GetManaNormalized()
    {
        return (float)_currentMana / _maxMana;
    }

    public void RegenerateMana(int amount)
    {
        _currentMana += amount;
        if (_currentMana > _maxMana)
        {
            _currentMana = _maxMana;
        }
        OnManaChanged?.Invoke();
    }

    public void UseSkill(int manaCost)
    {
        if (_currentMana >= manaCost)
        {
            _currentMana -= manaCost;
            OnManaChanged?.Invoke();
        }
    }
}
