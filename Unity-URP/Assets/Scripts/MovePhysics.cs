/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : MovePhysics.cs
* DESCRIPTION     : Move game object with physics
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

public class MovePhysics : MonoBehaviour
{
    private Rigidbody rb; //reference to the object's RigidBody component

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private Vector3 _direction = Vector3.left;

    //Public property to get or set the speed of the object
    public float Speed { get { return _speed; } set { _speed = value; } }

    //Public property to get or set the direction of movement for the object
    public Vector3 Direction { get { return _direction; } set { _direction = value; } }

    // Awake is called once at instantiation
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get the RigidBody component
    }//end Awake()


    // Update is called once per frame
    void Update()
    {
        //MoveWithVelocity();
      MoveWithForce();
    }//end Update()


    ///<summary>
    /// Velocity can be thought of as the speed and direction of an object. When you press the gas pedal in a car, you’re increasing the car's speed in a specific direction.
    /// </summary>

    void MoveWithVelocity()
    {
        //Note that Time.deltaTime is not needed with velocity because Unity's physics engine is handling movement

        Vector3 normalizedDirection = _direction.normalized; // Normalize the direction, to have a magnitude (length) of 1
        rb.velocity = normalizedDirection * _speed; // Set the velocity based on normalized direction
    }//end MoveWithVelocity()

    ///<summary>
    /// Force is an external influence that can change the motion of an object. It’s like a push or gust of wind that causes the object to start moving, stop moving, or change direction.
    /// </summary>

    void MoveWithForce()
    {
        // Apply force in the direction vector
        rb.AddForce(_direction * _speed);
    }//end MoveWithForce()




}
