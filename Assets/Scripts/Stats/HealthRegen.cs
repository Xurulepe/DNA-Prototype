using UnityEngine;

public class HealthRegen : MonoBehaviour, ICollectable
{
    [SerializeField] private int _healAmount = 20;

    public void Collect(GameObject collector)
    {
        collector.GetComponent<Player>().Heal(_healAmount);
        gameObject.SetActive(false);
    }
}
