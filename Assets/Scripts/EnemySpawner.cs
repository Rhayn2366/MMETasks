using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int enemyAmount = 2;
    private readonly List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private Vector2[] spawnPositions; 

    private void Update()
    {
        if (enemies.Count > enemyAmount)
        {
            return;
        }
        else 
        {
            enemies.Add(Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity));
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var randomNumber = Random.Range(0, spawnPositions.Length - 1);
        return new Vector3(spawnPositions[randomNumber].x, 1, spawnPositions[randomNumber].y);
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
        Destroy(enemyToRemove);
    }
}