using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyBase.EnemyType _enemyTypeToSpawn;

    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = EnemyPool.Instance.GetPooledEnemy(_enemyTypeToSpawn);
        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
        }
    }
}
