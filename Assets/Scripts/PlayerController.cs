using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float lookSensitivity = 10f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        lookAction = playerInput.actions["Look"];

        jumpAction.performed += Jump;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        Vector3 movement = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
    }

    private void RotatePlayer()
    {
        Vector2 lookDirection = lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, lookDirection.x * lookSensitivity * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.right, -lookDirection.y * lookSensitivity * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, 1.1f);
}
