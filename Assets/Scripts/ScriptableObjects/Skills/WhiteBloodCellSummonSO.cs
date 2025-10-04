using UnityEngine;

[CreateAssetMenu(fileName = "WhiteBloodCellSummon", menuName = "Skills/WhiteBloodCellSummon")]
public class WhiteBloodCellSummonSO : SkillSO
{
    public GameObject whiteBloodCellPrefab;

    public override void Activate(GameObject user)
    {
        // Instancia os glóbulos perto do jogador
        Debug.Log("Invocando glóbulos brancos!");
    }
}
