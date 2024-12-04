/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : MovePhysics.cs
* DESCRIPTION     : Move game object with physics
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/26	Akram Taghavi-Burris    Created class
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhysics : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed (or force) of projectile movement")]
    private float _speed = 10f;

    [SerializeField]
    [Tooltip("Direction to move the projectile")]
    private Vector3 _direction = Vector3.forward;

    public bool CanMove = true;


    //reference to the object's RigidBody component
    private Rigidbody _rigidBody; 

    //Public property to get or set the speed of the object
    public float Speed { get { return _speed; } set { _speed = value; } }

    //Public property to get or set the direction of movement for the object
    public Vector3 Direction { get { return _direction; } set { _direction = value; } }



    // Awake is called once at instantiation
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>(); //get the RigidBody component
    }//end Awake()


    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            Move();

        }//end if(CanMove)

    }//end Update()



    ///<summary>
    /// Move triggers the specified movement method passing through the desired direction and speed.
    /// </summary>
    /// <param name="direction">The direction in which the projectile should move. If null, the default direction is used.</param>
    /// <param name="speed">The magnitude of the force to be applied. If null, the default speed is used.</param>
    public void Move(Vector3? direction = null, float? speed = null)
    {
        // Use the provided direction and speed, or fall back to instance variables
        Vector3 moveDirection = direction ?? _direction;
        float moveSpeed = speed ?? _speed;

        //MoveWithVelocity();
        MoveWithForce(moveDirection, moveSpeed);


    }//end Move()



    ///<summary>
    /// Velocity can be thought of as the speed and direction of an object. When you press the gas pedal in a car, you’re increasing the car's speed in a specific direction.
    /// </summary>

    void MoveWithVelocity(Vector3 moveDirection, float moveSpeed)
    {
        //Note that Time.deltaTime is not needed with velocity because Unity's physics engine is handling movement

        Vector3 normalizedDirection = _direction.normalized; // Normalize the direction, to have a magnitude (length) of 1
        _rigidBody.velocity = normalizedDirection * _speed; // Set the velocity based on normalized direction
    }//end MoveWithVelocity()



    ///<summary>
    /// Force is an external influence that can change the motion of an object. It’s like a push or gust of wind that causes the object to start moving, stop moving, or change direction.
    /// </summary>

    void MoveWithForce(Vector3 moveDirection, float moveSpeed)
    {
        // Apply force in the direction vector
        _rigidBody.AddForce(moveDirection * moveSpeed, ForceMode.Impulse);
    
    }//end MoveWithForce()




}
