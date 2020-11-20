using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private int _enemyAmount = 2;
    private readonly List<GameObject> _enemies = new List<GameObject>();

    [SerializeField] private Vector2[] _spawnPositions;

    [SerializeField] private Transform _parent;

    private void Update()
    {
        if (_enemies.Count > _enemyAmount)
        {
            return;
        }
        else 
        {
            GameObject item = Instantiate(_enemyPrefab, GetSpawnPosition(), Quaternion.identity);
            _enemies.Add(item);
            item.transform.parent = _parent;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        if (_spawnPositions.Length == 0)
        {
            return new Vector3(0, 0, 0);
        }

        var randomNumber = Random.Range(0, _spawnPositions.Length - 1);
        return new Vector3(_spawnPositions[randomNumber].x, 1, _spawnPositions[randomNumber].y);
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        _enemies.Remove(enemyToRemove);
        Destroy(enemyToRemove);
    }
}