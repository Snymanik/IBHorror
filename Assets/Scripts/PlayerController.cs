using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  
    
    [SerializeField]
    GameObject camera_;
    
    Vector3 dir;
    #region CameraMovement
    float MouseX, MouseY, sensitivity = 10f,xRotation,yRotation;
    #endregion
    #region Jumping
    bool isGrounded;
    float Timing;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    float jumpCooldown,length;
    #endregion
    #region PlayerMovement
    Rigidbody rb;
    float Horizontal,Vertical;
    [SerializeField]
    private float speed,drag,maxVelocity;
    #endregion
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        MouseY = Input.GetAxis("Mouse Y");
        MouseX = Input.GetAxis("Mouse X");
        MouseX = Mathf.Clamp(MouseX, -90, 90);


        xRotation -= MouseY * Time.deltaTime * sensitivity;
        yRotation += MouseX * Time.deltaTime * sensitivity;

        camera_.transform.rotation = Quaternion.Euler(xRotation, yRotation+90, 0);
        this.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        dir = this.gameObject.transform.forward*Horizontal  + this.gameObject.transform.right*Vertical ;
        rb.AddForce(dir*speed,ForceMode.Force);
        rb.drag = drag;

        SpeedControl();


        if(Input.GetKey(KeyCode.Space))
        {
            CheckIfCanJump();
        }

    }

    private void SpeedControl()
    {
         Vector3 SpeedControl = new Vector3(rb.velocity.x,0,rb.velocity.z);
        if(rb.velocity.magnitude > 7)
        {
            SpeedControl = SpeedControl.normalized * 7;
            rb.velocity = new Vector3(SpeedControl.x,rb.velocity.y,SpeedControl.z);
        }
    }
    private void CheckIfCanJump()
    {
        
        isGrounded = Physics.Raycast(this.gameObject.transform.position, Vector3.down, length*0.4f, groundLayer);
       
        if (isGrounded)
        {
            Debug.Log("FY7FGUSAIF");

        }
            
        if(isGrounded && Time.fixedTime > Timing)
        { 
            rb.AddForce(Vector3.up*10, ForceMode.Impulse);
            Timing = Time.fixedTime +jumpCooldown;
        }
     
    }
}
