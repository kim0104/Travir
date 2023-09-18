using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5.0f;
    public float rotationSpeed = 200.0f;
    public float jumpForce = 2.0f;

    public Animator animator;

    public float mouseSensitivity = 200.0f;
    public float zoomSensitivity = 15.0f;
    public Transform cameraTransform;

    private float yRotation = 0f;
    private float xRotation = 0f;
    private float defaultDistance = 5f; // you can adjust this initial value

    private bool isJumping = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform != null)
        {
            defaultDistance = Vector3.Distance(transform.position, cameraTransform.position);
        }
    }
    
    void Awake()
    {
        if (photonView.IsMine)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        HandleMovement();
        HandleRotation();
        HandleCameraZoom();
    }

    void HandleMovement()
    {
        float moveVertical = Input.GetAxis("Vertical"); // 플레이어 상하이동
        float moveHorizontal = Input.GetAxis("Horizontal"); // 플레이어 좌우이동

        float animationSpeed = 1;
        bool isWalking = false;

        if (moveVertical != 0 || moveHorizontal != 0)
        {
            isWalking = true;
        }

        animator.SetBool("isWalk", isWalking);

        if (moveVertical < 0)
        {
            animator.SetFloat("playerMove", -1);
        }
        else
        {
            animator.SetFloat("playerMove", 1);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
            animationSpeed = 2;
        }
        else
        {
            animator.SetBool("isRun", false);
            animationSpeed = 1;
        }

        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed + transform.right * moveHorizontal * speed * animationSpeed;
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }
    }


void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (cameraTransform != null)
        {
            cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    void HandleCameraZoom()
    {
        if (cameraTransform == null) return;

        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        defaultDistance -= mouseScroll * zoomSensitivity;
        defaultDistance = Mathf.Clamp(defaultDistance, 1f, 10f);

        cameraTransform.position = transform.position - cameraTransform.forward * defaultDistance;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        switch (collision.gameObject.tag)
        {
            case ("Cube"):
                BusControl.Instance.ToggleDoor();
                break;
            case ("Jeju"):
                Data.spawnType = Data.SpawnType.Jeju;
                PhotonNetwork.LoadLevel(2);
                break;
            case ("Seoul"):
                Data.spawnType = Data.SpawnType.Seoul;
                PhotonNetwork.LoadLevel(1);
                break;
            default:
                break;
        }
    }
}