using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;

    private CharacterController characterController;
    public Animator animator;
    private Vector3 moveDirection;
    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveCharacter();
        RotateCharacter();
    }

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        // 이동
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // 점프
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // 캐릭터 컨트롤러에 적용
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        float mouseX = Input.GetAxis("MouseX");
    }
}
