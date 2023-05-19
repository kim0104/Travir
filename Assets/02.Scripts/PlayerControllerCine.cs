using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCine : MonoBehaviour
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

    }

    public float turnSpeed = 3f;
    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isRun", true);
       /*     if (verticalInput < 0)
            {
                animator.SetFloat("VerticalDir", -1);
                
            }
            else
            {
                animator.SetFloat("VerticalDir", 1);
            }*/
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        // �̵�
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
        moveDirection *= moveSpeed;

        // ����
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

        // ĳ���� ��Ʈ�ѷ��� ����
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
