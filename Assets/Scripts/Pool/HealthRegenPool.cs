using UnityEngine;

public class HealthRegenPool : ObjectPool
{
    public static HealthRegenPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
