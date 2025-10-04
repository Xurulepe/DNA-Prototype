using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    [Header("Skill Settings")]
    public string skillName;
    public float cooldown;
    public Sprite icon;

    /// <summary>
    /// O que acontece quando a habilidade é ativada.
    /// </summary>
    public abstract void Activate(GameObject user);
}
