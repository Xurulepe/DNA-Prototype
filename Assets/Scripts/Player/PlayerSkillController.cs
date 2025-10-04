using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private List<SkillSO> _skills = new List<SkillSO>();
    private float[] _lastUseTimes;

    private void Awake()
    {
        _lastUseTimes = new float[_skills.Count];

        for (int i = 0; i < _lastUseTimes.Length; i++)
        {
            _lastUseTimes[i] = -Mathf.Infinity; // Inicializa para permitir uso imediato
        }
    }

    public void OnUseSkill1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryUse(0);
        }
    }

    public void OnUseSkill2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryUse(1);
        }
    }

    public void OnUseUltimateSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryUse(2);
        }
    }

    private void TryUse(int index)
    {
        if (index < 0 || index >= _skills.Count)
        {
            return;
        }

        SkillSO skill = _skills[index];
        if (Time.time >= _lastUseTimes[index] + skill.cooldown)
        {
            skill.Activate(gameObject);
            _lastUseTimes[index] = Time.time;
        }
    }
}
