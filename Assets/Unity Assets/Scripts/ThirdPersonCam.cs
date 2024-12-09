using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    [Header("Player Movement and Ground Detection")]
    public float jumpForce = 10f; // Force applied when jumping
    public float gravity = -15f; // Gravity value
    public float groundedRadius = 0.28f; // Radius of the sphere for ground check
    public float groundedOffset = -0.14f; // Offset below player for ground check
    public LayerMask groundLayer; // Layer mask for ground detection
    private bool isGrounded; // Whether the player is on the ground
    private bool canJump = true; // Cooldown state for jumping

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Switch camera styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);

        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotate player object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }

        // Check if the player is grounded
        isGrounded = IsGrounded();

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpCooldown(0.3f)); // Cooldown of 0.3 seconds
        }

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * Mathf.Abs(gravity), ForceMode.Acceleration);
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);

        currentStyle = newStyle;
    }

    // Method to check if the player is grounded
    private bool IsGrounded()
    {
        Vector3 groundCheckPosition = player.position + Vector3.down * groundedOffset;
        bool grounded = Physics.CheckSphere(groundCheckPosition, groundedRadius, groundLayer);

        Debug.Log($"IsGrounded: {grounded}"); // Debug to verify ground detection
        return grounded;
    }

    // Coroutine to implement jump cooldown
    private IEnumerator JumpCooldown(float delay)
    {
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true;
    }

    // Visual debugging for ground check
    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 groundCheckPosition = player.position + Vector3.down * groundedOffset;
        Gizmos.DrawWireSphere(groundCheckPosition, groundedRadius);
    }
}
