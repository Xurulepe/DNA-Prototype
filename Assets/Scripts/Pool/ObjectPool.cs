using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected List<GameObject> pooledObjects;
    [SerializeField] protected List<ObjectToPool> objectsToPool;

    protected virtual void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            for (int j = 0; j < objectsToPool[i].amount; j++)
            {
                tmp = Instantiate(objectsToPool[i].prefab);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
    }

    public virtual GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ObjectToPool
{
    public GameObject prefab;
    public int amount;
}
