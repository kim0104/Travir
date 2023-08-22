using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5.0f; // �̵� �ӵ�
    public float rotationSpeed = 200.0f; // ȸ�� �ӵ�
    public float jumpForce = 2.0f; // ���� ��

    public Animator animator;

    private bool isJumping = false; // ���� ������ üũ
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        // �յ� �̵��� w, s �Ǵ� ����Ű ���Ʒ�
        float moveVertical = Input.GetAxis("Vertical");

        // �¿� ȸ���� a, d �Ǵ� ����Ű �¿�
        float rotateHorizontal = Input.GetAxis("Horizontal");

        #region �ִϸ��̼� �ڵ�
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

        // �̵�
        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed;
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // ȸ��
        Vector3 rotation = new Vector3(0, rotateHorizontal, 0) * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.deltaTime));

        // ���� - ���� ���� �ƴ� ��, �����̽��ٸ� ������ ����
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }

        
    }

    // ���� �� �ٽ� ���� ��Ҵ��� üũ
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping=false;
        }

        switch (collision.gameObject.tag)
        {           
            case ("Cube"):
                BusControl.Instance.ToggleDoor();
                break;
            case ("Jeju"):
                Data.spawnType = Data.SpawnType.Jeju;
                SceneManager.LoadScene("Jeju");
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����忡 �������� ��
        if(other.tag == "DoorEntry" && !BusControl.Instance.isMoving)
        {
            BusControl.Instance.ToggleDoor();
        }
        // �ö�Ż ��
        else if(other.tag == "BusEntry" && !BusControl.Instance.isMoving)
        {
            transform.parent = BusControl.Instance.gameObject.transform;
            BusControl.Instance.BusMove();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �����忡�� ����� ��
        if (other.tag == "DoorEntry")
        {
            BusControl.Instance.ToggleDoor();
        }
        // �������� ���� ��
        else if (other.tag == "BusEntry" && !BusControl.Instance.isMoving)
        {
            if (transform.parent != null)
            {
                if (transform.parent == BusControl.Instance.gameObject.transform)
                {
                    transform.parent = null;
                }
            }
        }
    }
}
