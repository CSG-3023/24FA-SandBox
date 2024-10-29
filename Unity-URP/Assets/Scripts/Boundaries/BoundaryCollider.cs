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
    private MovePhysics _movePhysics;

    // Awake is called once at instantiation
    void Awake()
    {
        Boundary.isTrigger = true; //boundary collider must be set to trigger
        _movePhysics = GetComponent<MovePhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundary(); 
    }//end Update()

    //IsInsideBoundary is a boolean check if the object is in the boundary 
    private bool IsInsideBoundary()
    {
        Bounds bounds = Boundary.bounds; //gets the bounds of the box collider 
        return bounds.Contains(transform.position); //returns if this game object in contained in the bounds
    }

    //Check if game object is inside or outside the boundary
    private void CheckBoundary()
    {
        //if the game object is in the boundary, then record the last position
        if (IsInsideBoundary())
        {
            _lastPositionInsideBoundary = transform.position;
        }
        else
        {
            Debug.Log("Exited Boundary");
            ReturnToBoundary(); //send it back to the boundary
        }//end if (IsInsideBoundary())
    
    }//end CheckBoundary()


    private void ReturnToBoundary()
    {
        Debug.Log("Returned to Boundary");

        // Reset the object's position to the last position inside the boundary
        transform.position = _lastPositionInsideBoundary;

        //Stop object from moving
        _movePhysics.CanMove = false;

    }//end ReturnToBoundary()
}
