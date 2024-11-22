/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : Paddle.cs
* DESCRIPTION     : Adds opposing force to target object on collision
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
*  2024/11/22   Akram Taghavi-Burris    class created
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaddleForce : MonoBehaviour
{
    [Tooltip("The amount of force to apply")]
    [SerializeField]
    private float _forceAmount = 10f;

    [Tooltip("The target object colliding with the paddle")]
    [SerializeField]
    private GameObject _collisionTarget;

    [Tooltip("The speed of rotation for paddle toggle")]
    [SerializeField]
    private float _rotationSpeed = 5f;

    //Rigidbody of target object
    private Rigidbody _targetRB;

    //The direction to apply force
    private Vector3 _forceDirection; 



    private void OnCollisionEnter(Collision collision)
    {
        //check if the paddle collides with the target objct
        if(collision.gameObject == _collisionTarget)
        {
            //get the Rigidboady of the target object
           _targetRB = collision.gameObject.GetComponent<Rigidbody>();

            //if the target object has a RigidBody
            if(_targetRB != null)
            {
                //Get the direction of the collision (this will be perpendicular to the surface)
                _forceDirection = collision.contacts[0].normal;

                //Apply force to the target Rigidbody using the force direction
                ApplyForce(_targetRB, _forceDirection);
            }//end if(_targetRB != null)
        }//end if(collision)
    }//end OnCollsion

    
    /// Applies opposing forces to the target object
    private void ApplyForce(Rigidbody targetRB, Vector3 forceDirection)
    {
        //Apply the force in the opposite direction of the normal to make the target object move away
        targetRB.AddForce(-forceDirection * _forceAmount, ForceMode.Impulse);

        //Optionally you can apply some horizontal force for more variety in the target object's movement
        Vector3 extraForce = new Vector3(0, 0, 5f); //Adjust this value for more of a "paddle hit" effect
        targetRB.AddForce(extraForce, ForceMode.Impulse);
    }//end ApplyForce()
}
