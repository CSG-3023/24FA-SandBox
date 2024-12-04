/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : PlayerController.cs
* DESCRIPTION     : move game object with player controls (i.e. keyboard commands)
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/28	Akram Taghavi-Burris    Created class
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private Vector3 _direction = Vector3.forward;

    private Rigidbody _rigidBody; //reference to the object's RigidBody component

    public bool CanMove = true;

    //Public property to get or set the speed of the object
    public float Speed { get { return _speed; } set { _speed = value; } }

    // Awake is called once at instantiation
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>(); //get the RigidBody component
    }//end Awake()


    // Update is called once per frame
    void Update()
    {
        MoveWithTransform();
    }//end update

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        //MoveWithPhysics();
    }//end update


    //Move with transform.positiion
    void MoveWithTransform() 
    {
        // Get input from the Horizontal and Vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement direction vector based on input
        _direction = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize direction to prevent faster diagonal movement
        _direction = _direction.normalized;

        // Move the object
        transform.position += _direction * _speed * Time.deltaTime;
    }//end MoveWithTransform()

    //Move with physics, needs reference to RigidBbody
    void MoveWithPhysics()
    {
        // Get input from the Horizontal and Vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement direction vector based on input
        _direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Set the Rigidbody's velocity based on direction and speed
        _rigidBody.velocity = _direction * _speed;

    }//end MoveWithPhysics()




}
