using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform cameraTransform;

    private bool isGrounded = true;
    private Vector3 moveDirection;

    private void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;

        moveDirection = (forward * v + right * h).normalized;
        playerRigidbody.linearVelocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, jumpForce, playerRigidbody.linearVelocity.z);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
