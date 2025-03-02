using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private InputManager inputManager;

    private bool isGrounded = true;

    private void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJump.AddListener(Jump);
    }

    private void MovePlayer(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y) * playerSpeed;
        playerRigidbody.linearVelocity = new Vector3(moveDirection.x, playerRigidbody.linearVelocity.y, moveDirection.z);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, jumpForce, playerRigidbody.linearVelocity.z);
            isGrounded = false;
            Debug.Log("Player Jumped");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  
            Debug.Log("Player Landed on Ground");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player Left the Ground");
        }
    }
}
