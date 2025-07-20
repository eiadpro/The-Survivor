using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;

    void Update()
    {
        // Toggle between walking and sprinting
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector3.right;

        // Normalize to prevent faster diagonal movement
        transform.Translate(moveDirection.normalized * currentSpeed * Time.deltaTime, Space.World);
    }
}

