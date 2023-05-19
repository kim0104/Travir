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

        // �̵�
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
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

    public float mouseSensitivity = 0f;
    public float zoomSensitivity = 0f;
    public Transform cameraTransform;

    private float yRotation = 0f;
    private float xRotation = 0f;
    private float defaultDistance = 0f;

    private void RotateCharacter()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        yRotation += mouseX;
        xRotation -= mouseY;

        // ȸ�� �ݰ� ���� ����
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        defaultDistance -= mouseScroll * zoomSensitivity; // ���콺 �ٿ� ���� �Ÿ� ����
        defaultDistance = Mathf.Clamp(defaultDistance, 1f, 10f); // ī�޶� �Ÿ� ����

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // ī�޶��� ȸ���� �����ϰ�, ī�޶� ĳ���� �ڷ� �̵�
        cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        cameraTransform.position = transform.position - cameraTransform.forward * defaultDistance;
    }
}

