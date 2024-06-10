using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedRun;
    [SerializeField] private float speedSit;

    private CharacterController characterController;
    private Vector3 walkDirection;
    private Vector3 velocity;
    private float speed;    

    private void Start()
    {
        speed = speedWalk;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Jump(characterController.isGrounded && Input.GetKey(KeyCode.Space));
        Run(Input.GetKey(KeyCode.LeftControl));
        Sit(Input.GetKey(KeyCode.LeftShift));
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        walkDirection = transform.right * x + transform.forward * z;
    }

    private void FixedUpdate()
    {
        Walk(walkDirection);
        DoGravity(characterController.isGrounded);
    }

    private void Walk(Vector3 direction)
    {
        characterController.Move(direction * speedWalk * Time.fixedDeltaTime);
    }

    private void DoGravity(bool isGrounded)
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }            
        velocity.y -= gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);        
    }

    private void Jump(bool canJump)
    {
        if(canJump)
            velocity.y = jumpForce;
    }

    private void Run(bool canRun)
    {
        speedWalk = canRun ? speedRun : speed;
    }

    private void Sit(bool canSit)
    {
        characterController.height = canSit ? 1f : 2f;
    }
}
