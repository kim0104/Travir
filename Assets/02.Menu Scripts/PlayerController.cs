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

    private bool isJumping = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        float moveVertical = Input.GetAxis("Vertical");


        float rotateHorizontal = Input.GetAxis("Horizontal");

        #region  
        float animationSpeed = 0;



        if (rotateHorizontal != 0 || moveVertical != 0)
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


        #endregion


        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed;
        rb.MovePosition(rb.position + movement * Time.deltaTime);


        Vector3 rotation = new Vector3(0, rotateHorizontal, 0) * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.deltaTime));


        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }


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