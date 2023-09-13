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

    public float mouseSensitivity = 100.0f;
    public float zoomSensitivity = 2.0f;
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

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        HandleMovement();
        HandleRotation();
        HandleCameraZoom();
    }

    void HandleMovement()
    {
        float moveVertical = Input.GetAxis("Vertical");

        float animationSpeed = 1;

        if (moveVertical != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

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

        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed;
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