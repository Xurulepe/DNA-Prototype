using UnityEngine;

public class ManaRegenPool : ObjectPool
{
    public static ManaRegenPool Instance { get; private set; }

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
