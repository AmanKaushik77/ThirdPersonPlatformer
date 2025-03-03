using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private int maxJumps = 1;

    private int jumpCount;
    private Vector3 moveDirection;

    private void Update()
    {
        bool wasGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance);
        
        if (wasGrounded && jumpCount != 0)
        {
            jumpCount = 0; 
        }

        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;

        moveDirection = (forward * v + right * h).normalized;
        playerRigidbody.linearVelocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    private void Jump()
    {
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, jumpForce, playerRigidbody.linearVelocity.z);
        jumpCount++;
    }
}
