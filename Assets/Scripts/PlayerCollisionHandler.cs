using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CollectableSpawner collectableSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectableSpawner.RemoveCollectable(other.transform);
            scoreManager.IncreaseScore();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemySpawner.RemoveEnemy(other.gameObject);
            scoreManager.DecreaseScore();
        }
    }
}