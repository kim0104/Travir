using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerContChar : MonoBehaviour
{
    private CharacterController _charController;
    public Animator _animator;

    //move
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.1f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = tempPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        //Debug.Log(inputX + inputZ);
        if (inputX ==0 &&inputZ ==0)
        {
            _animator.SetBool("isRun",false);
        }
        else
        {
            {
                _animator.SetBool("isRun",true);
            }
        }
    }    

    private void FixedUpdate()
    {
        v_movement = new Vector3 (inputX*moveSpeed,0,inputZ*moveSpeed);
        _charController.Move(v_movement);
    }
}
