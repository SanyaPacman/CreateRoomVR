using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speedMove;
    public float speedRotate;
    public float jumpPower;
    private float gravityForce;
    private Vector3 moveVector;
    private CharacterController ch_controller;
    private Animator ch_animator;
    private MobileController mContr;
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
       // ch_animator = GetComponent<Animator>();
        mContr = FindObjectOfType<MobileController>();
        
    }
    private void Awake()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }
    }


    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
       // GamingGravity();
    }
    private void CharacterMovement()
    {
        CharacterMove();
        CharacterRotate();  
    }
    private void CharacterRotate()
    {
        Vector3 rotate = transform.rotation.eulerAngles;
        rotate = new Vector3(-mContr.VerticalRotate() * speedRotate, mContr.HorizontalRotate() * speedRotate,0);
        
        transform.Rotate(rotate);
        var rot = transform.rotation.eulerAngles;
        
        transform.rotation = Quaternion.Euler(rot.x, rot.y, 0);
    }
    private void CharacterMove()
    {        
        moveVector = transform.forward * mContr.VerticalMove() * speedMove+ transform.right * mContr.HorizontalMove() * speedMove;       
        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);
    }
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
            gravityForce -= 20f * Time.deltaTime;
        else
            gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
            gravityForce = jumpPower;
    }
}