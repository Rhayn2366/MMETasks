using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject target;

    [SerializeField] private float movementSpeed = 0.5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (target == null) return;
        transform.LookAt(target.transform, Vector3.up);
        var step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
}
