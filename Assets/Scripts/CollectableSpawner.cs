using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    private const float _spawnRangeX = 4f;
    private const float _spawnRangeY = 4f;
    public static CollectableSpawner Instance;
    [SerializeField] private Transform _collectablePrefab;
    [SerializeField] private int _minCollectablesOnField = 10;
    [SerializeField] private Transform _parent;
    private readonly List<Transform> _activeCollectables = new List<Transform>();
    private readonly List<Vector3> _usedPositions = new List<Vector3>();

    [SerializeField, Tooltip("Toggle whether more collectables shall spawn after the first wave was cleared")] 
    private bool _shallSpawnMoreWhenEmpty = false;
    

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
        while (_activeCollectables.Count < 10)
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
            randomX = Random.Range(-_spawnRangeX, _spawnRangeX);
            randomY = Random.Range(-_spawnRangeY, _spawnRangeY);
            if (IsLonely(new Vector3(randomX * _parent.localScale.x, 1 * _parent.localScale.y, randomY * _parent.localScale.z)))
            {
                accepted = true;
            }
        } while (!accepted);

        Vector3 position = new Vector3(randomX * _parent.localScale.x, 1 * _parent.localScale.y, randomY * _parent.localScale.z);
        Transform item = Instantiate(_collectablePrefab, position, Quaternion.identity);
        _activeCollectables.Add(item);
        item.parent = _parent;
        item.localScale = new Vector3(_parent.localScale.x * _collectablePrefab.localScale.x, _parent.localScale.y * _collectablePrefab.localScale.y, _parent.localScale.z * _collectablePrefab.localScale.z);
        _usedPositions.Add(position);
    }

    private bool IsLonely(Vector3 position)
    {
        foreach (var usedPosition in _usedPositions)
        {
            var d = usedPosition - position;
            if (d.magnitude < 0.5 * _parent.localScale.x)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (_activeCollectables.Count == 0 && _shallSpawnMoreWhenEmpty)
        {
            Populate();
        }
    }

    public void RemoveCollectable(Transform collectableToRemove)
    {
        _activeCollectables.Remove(collectableToRemove.parent);
        _usedPositions.Remove(collectableToRemove.parent.position);
        Destroy(collectableToRemove.parent.gameObject);
    }
}