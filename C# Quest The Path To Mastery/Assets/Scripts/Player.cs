using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedRun;
    [SerializeField] private float speedSit;
    [SerializeField] private GameObject parameters;
    [SerializeField] private TextMeshProUGUI speedDisplayText;

    private CharacterController characterController;
    private Vector3 walkDirection;
    private Vector3 velocity;
    private float currentSpeed;
    private bool isRunning;
    private bool isSitting;

    private void Start()
    {
        currentSpeed = speedWalk;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.F12) && parameters.activeInHierarchy == true)
        {
            parameters.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.F12) && parameters.activeInHierarchy == false)
        {
            parameters.SetActive(true);
        }
        Jump(characterController.isGrounded && Input.GetKey(KeyCode.Space));
        Run(Input.GetKey(KeyCode.LeftControl));
        Sit(Input.GetKey(KeyCode.LeftShift));
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        walkDirection = transform.right * x + transform.forward * z;

        // Update the speed display
        if (speedDisplayText != null)
        {
            speedDisplayText.text = $"Speed: {currentSpeed:F2}";
        }
    }

    private void FixedUpdate()
    {
        Walk(walkDirection);
        DoGravity(characterController.isGrounded);
    }

    private void Walk(Vector3 direction)
    {
        characterController.Move(direction * currentSpeed * Time.fixedDeltaTime);
    }

    private void DoGravity(bool isGrounded)
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        velocity.y -= gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);
    }

    private void Jump(bool canJump)
    {
        if (canJump)
            velocity.y = jumpForce;
    }

    private void Run(bool canRun)
    {
        if (canRun && !isSitting)
        {
            currentSpeed = speedRun;
        }
        else
        {
            currentSpeed = speedWalk;
        }
        isRunning = canRun;
    }

    private void Sit(bool canSit)
    {
        characterController.height = canSit ? 1f : 2f;
        isSitting = canSit;
        if (canSit)
        {
            currentSpeed = speedSit;
        }
        else if (isRunning)
        {
            currentSpeed = speedRun;
        }
        else
        {
            currentSpeed = speedWalk;
        }
    }
}
