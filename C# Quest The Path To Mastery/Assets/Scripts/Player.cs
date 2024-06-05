using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;

    private CharacterController characterController;
    private Vector3 walkDirection;
    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Jump(characterController.isGrounded && Input.GetKey(KeyCode.Space));
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
}
