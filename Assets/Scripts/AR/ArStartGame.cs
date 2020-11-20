using System.Collections.Generic;
using UnityEngine;

public class ArStartGame : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjectsToActivate;
    public void Play()
    {
        foreach(var gameObject in _gameObjectsToActivate)
        {
            gameObject.SetActive(true);
        }
    }
}
