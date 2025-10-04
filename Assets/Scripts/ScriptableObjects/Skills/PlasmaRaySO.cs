using UnityEngine;

[CreateAssetMenu(fileName = "PlasmaRay", menuName = "Skills/PlasmaRay")]
public class PlasmaRaySO : SkillSO
{
    public GameObject plasmaRayPrefab;
    public float duration = 3f;

    public override void Activate(GameObject user)
    {
        Debug.Log("Ativando Raio de Plasma!");
    }
}
