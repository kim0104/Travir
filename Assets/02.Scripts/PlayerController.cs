using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float m_movespeed = 50f;


    private CharacterController characterController;
    public Animator animator;
    private Vector3 moveDirection;
    private float verticalVelocity;
    public bool condition = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ControlByType();
    }

    private void ControlByType()
    {
        if(condition)
        {
            MoveCharacter();
            RotateCharacter();
        }
        else
        {
            MovePlayer();
        }
    }

    // bool 타입 변수 하나를 선언하고
    // 변수 값에 따라 각각 다른 조작 함수를 호출하도록 코드를 작성하시오.
    

    #region Type1
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

        // 만약에 플레이어가 뒤로 가면 animation을 역행하시오.
        if (verticalInput < 0)
        {
            animator.SetFloat("playerMove", -1);
        }
        else
        {
            animator.SetFloat("playerMove", 1);
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

        // 회전 반경 범위 제한
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        defaultDistance -= mouseScroll * zoomSensitivity; // 마우스 휠에 따라 거리 조절
        defaultDistance = Mathf.Clamp(defaultDistance, 1f, 10f); // 카메라 거리 제한

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // 카메라의 회전을 설정하고, 카메라를 캐릭터 뒤로 이동
        cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        cameraTransform.position = transform.position - cameraTransform.forward * defaultDistance;
    }
    #endregion

    #region Type2
    private void MovePlayer()
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

        // 만약에 플레이어가 뒤로 가면 animation을 역행하시오.
        if (verticalInput < 0)
        {
            animator.SetFloat("playerMove", -1);
        }
        else
        {
            animator.SetFloat("playerMove", 1);
        }

        // 이동
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // 회전
        transform.Rotate(Vector3.up * horizontalInput * m_movespeed * Time.deltaTime);
        Debug.Log("rotateComplete");
        Debug.Log(horizontalInput);
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
    #endregion
}

