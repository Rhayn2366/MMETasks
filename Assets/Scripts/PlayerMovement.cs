using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float rotationAmount = 2f;
    public Vector2 panLimit;

    private Vector3 position;
    private Quaternion rotation;

    private void Update()
    {
        SetPositionValues();
        UserInput();
        RestrictValues();
        UpdateTransformValues();
    }

    private void SetPositionValues()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    private void UserInput()
    {
        Move();
        RotateYAxis();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        position += transform.forward * vertical * movementSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        position += transform.right * horizontal * movementSpeed;
    }

    private void RotateYAxis()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rotation *= Quaternion.Euler(Vector3.up * -rotationAmount * movementSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation *= Quaternion.Euler(Vector3.up * rotationAmount * movementSpeed);
        }
    }

    private void RestrictValues()
    {
        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
        position.z = Mathf.Clamp(position.z, -panLimit.y, panLimit.y);
    }

    private void UpdateTransformValues()
    {
        transform.position = Vector3.Lerp(transform.position, position, Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
    }
}