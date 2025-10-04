using UnityEngine;

[CreateAssetMenu(fileName = "ForceField", menuName = "Skills/ForceField")]
public class ForceFieldSO : SkillSO
{
    public GameObject shieldPrefab;
    public float shieldDuration = 5f;

    public override void Activate(GameObject user)
    {
        Debug.Log("Escudo protetor ativado!");
    }
}
