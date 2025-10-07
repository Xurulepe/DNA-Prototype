using UnityEngine;

public class ManaRegen : MonoBehaviour, ICollectable
{
    [SerializeField] private int _manaAmount = 20;

    public void Collect(GameObject collector)
    {
        collector.GetComponent<Player>().RegenerateMana(_manaAmount);
        gameObject.SetActive(false);
    }
}
