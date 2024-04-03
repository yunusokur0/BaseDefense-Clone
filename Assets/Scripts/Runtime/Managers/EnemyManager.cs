using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawmList = new List<GameObject>();
    [SerializeField] private List<GameObject> turretList = new List<GameObject>();
    private int _enemiesPerWave = 4;
    private int _spawnedEnemies = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 0, 4);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _enemiesPerWave; i++)
        {
            SpawnEnemy();
        }
        _spawnedEnemies = 0;
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = PoolSignals.Instance.onGetPoolObject?.Invoke(3);
        if (newEnemy != null)
        {
            GameObject spawnPoint = spawmList[Random.Range(0, spawmList.Count)];
            newEnemy.GetComponent<EnemyController>().Target = turretList[Random.Range(0, turretList.Count)];
            newEnemy.transform.position = spawnPoint.transform.position;
            newEnemy.SetActive(true);
            newEnemy.transform.SetParent(transform);
        }
    }
}
