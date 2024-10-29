/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : BoundaryCollider.cs
* DESCRIPTION     : Boundary inside a colilder
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2000/01/01		Developer's Name    		 Created <short comment of changes>
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollider : MonoBehaviour
{
    [Tooltip("Game object that sets boundary with box collider")]
    public BoxCollider Boundary;

    private Rigidbody _rigidBody; //reference to the object's RigidBody component
    private Vector3 _lastPositionInsideBoundary; 

    // Awake is called once at instantiation
    void Awake()
    {
        Boundary.isTrigger = true; //boundary collider must be set to trigger
    }

    // Update is called once per frame
    void Update()
    {
        //if the game object is in the boundary, then record the last position
        if (IsInsideBoundary())
        {
            _lastPositionInsideBoundary = transform.position;
        }
    }//end Update()

    //IsInsideBoundary is a boolean check if the object is in the bounadry 
    private bool IsInsideBoundary()
    {
        Bounds bounds = Boundary.bounds; //gets the bounds of the box collider 
        return bounds.Contains(transform.position); //returns if this game object in conttained in the bounds
    }

    //When game object exits the trigger
    private void OnTriggerExit(Collider other)
    {
        //if the trigger is the boundary
        if(other == Boundary)
        {
            Debug.Log("Left Boundary");

            // Reset the object's position to the last position inside the boundary
            transform.position = _lastPositionInsideBoundary;
            

            // Stop any ongoing movement (if using Rigidbody)
            if (_rigidBody != null)
            {
                _rigidBody.velocity = Vector3.zero;
                _rigidBody.angularVelocity = Vector3.zero;

            }//end if(_rigidBody)
        }
        
    }//end OnTriggerExit
}
