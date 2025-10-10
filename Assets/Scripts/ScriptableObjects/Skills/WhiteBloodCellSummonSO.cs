using UnityEngine;

[CreateAssetMenu(fileName = "WhiteBloodCellSummon", menuName = "Skills/WhiteBloodCellSummon")]
public class WhiteBloodCellSummonSO : SkillSO
{
    public GameObject whiteBloodCellPrefab;

    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private int _numberOfCells = 3;
    public override void Activate(GameObject user)
    {
        // Instancia os gl�bulos perto do jogador
        Debug.Log("Invocando gl�bulos brancos!");

        for (int i = 0; i < _numberOfCells; i++)
        {
            Vector3 spawnPosition = user.transform.position + Random.insideUnitSphere * _spawnRadius;
            spawnPosition.y = user.transform.position.y; // Mant�m a altura do jogador
            GameObject whiteBloodCell = WhiteBloodCellPool.Instance.GetPooledObject();

            if (whiteBloodCell != null)
            {
                whiteBloodCell.transform.position = spawnPosition;
                whiteBloodCell.transform.rotation = Quaternion.identity;
                whiteBloodCell.SetActive(true);
            }
        }
    }
}
