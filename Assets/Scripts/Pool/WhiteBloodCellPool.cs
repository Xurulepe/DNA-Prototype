using UnityEngine;

public class WhiteBloodCellPool : ObjectPool
{
    public static WhiteBloodCellPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
