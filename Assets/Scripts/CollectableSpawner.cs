using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    private const float spawnRangeX = 4f;
    private const float spawnRangeY = 4f;
    public static CollectableSpawner Instance;
    [SerializeField] private Transform collectablePrefab;
    [SerializeField] private int minCollectablesOnField = 10;
    private readonly List<Transform> activeCollectables = new List<Transform>();
    private readonly List<Vector3> usedPositions = new List<Vector3>();

    [SerializeField, Tooltip("Toggle whether more collectables shall spawn after the first wave was cleared")] 
    private bool shallSpawnMoreWhenEmpty = false;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Populate();
    }

    private void Populate()
    {
        while (activeCollectables.Count < 10)
        {
            PlaceNewCollectable();
        }
    }

    private void PlaceNewCollectable()
    {
        float randomX, randomY;
        bool accepted = false;
        do
        {
            randomX = Random.Range(-spawnRangeX, spawnRangeX);
            randomY = Random.Range(-spawnRangeY, spawnRangeY);
            if (IsLonely(new Vector3(randomX, 1, randomY)))
            {
                accepted = true;
            }
        } while (!accepted);

        Vector3 position = new Vector3(randomX, 1, randomY);
        activeCollectables.Add(Instantiate(collectablePrefab, position, Quaternion.identity));
        usedPositions.Add(position);
    }

    private bool IsLonely(Vector3 position)
    {
        foreach (var usedPosition in usedPositions)
        {
            var d = usedPosition - position;
            if (d.magnitude < 0.5)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (activeCollectables.Count == 0 && shallSpawnMoreWhenEmpty)
        {
            Populate();
        }
    }

    public void RemoveCollectable(Transform collectableToRemove)
    {
        activeCollectables.Remove(collectableToRemove.parent);
        usedPositions.Remove(collectableToRemove.parent.position);
        Destroy(collectableToRemove.parent.gameObject);
    }
}