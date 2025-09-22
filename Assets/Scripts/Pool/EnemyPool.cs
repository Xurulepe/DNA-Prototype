using UnityEngine;

public class EnemyPool : ObjectPool
{
    public static EnemyPool Instance { get; private set; }

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

    public override GameObject GetPooledObject()
    {
        Debug.LogWarning("Use GetPooledEnemy(enemyType)!");
        return null;
    }

    public GameObject GetPooledEnemy(EnemyBase.EnemyType enemyType) 
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy && obj.GetComponent<EnemyBase>().GetEnemyType() == enemyType)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}
