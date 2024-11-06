/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : SlidingTiles.cs
* DESCRIPTION     : Slides tile object in the direction of the mouse
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

public class SlidingTiles : MonoBehaviour
{
    private Vector3 _initialPosition; // Initial position before dragging
    private bool _isSliding = false; //Is the object being slid

    public float SnapDistance = 1.0f; // Distance to move in each slide, assuming grid size of 1 unit
    public LayerMask collisionLayer; // Layer to detect obstacles

    void Update()
    {
        CheckForInput();

    }//end Update()

    //Check for user input
    private void CheckForInput()
    {
        // Start dragging when the player clicks on the object
        if (Input.GetMouseButtonDown(0) && !_isSliding)
        {
            TryStartSlide(); // Attempt to start dragging
        }//end if(ButtonDown)

        // If object is already sliding, handle slide and movement
        if (_isSliding)
        {
            HandleSliding(); // Manage dragging if already in progress
        }//end if (_isSliding)

    }//end CheckForInput()


    //Try to strat sliding by checking if object can be slid
    private void TryStartSlide()
    {
        // Cast a ray from the mouse to detect if this piece was clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Is the object hit by the ray this object
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            _isSliding = true;
            _initialPosition = transform.position;
        }//end if(this.transform)

    }//end TryStartSlide

    //Handle behavior for sliding
    private void HandleSliding()
    {
        //Check if LeftMouse button is held down
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 direction = (mousePosition - _initialPosition).normalized;

            // Find closest grid-aligned direction
            Vector3 moveDirection = GetSnapDirection(direction);

            // Target position in the grid
            Vector3 targetPosition = _initialPosition + moveDirection * SnapDistance;

            // Check if object can slide (i.e. no obstacles in that direction)
            if (CanSlide(targetPosition))
            {
                // Move piece to the target position
                transform.position = targetPosition;
            }

            // Stop dragging once moved
            _isSliding = false;

        }//end if(GetMouseButton)
    }//end HandleSliding()


    // Check if object can move to the target position without obstacles
    private bool CanSlide(Vector3 position)
    {
        // Check if there are any colliders at the target position
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f, collisionLayer);

        // Return true if no colliders are found (meaning the path is clear to slide)
        return colliders.Length == 0;
    }

    // Get mouse position in world space
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos2D);
    }//end GetMouseWorldPosition()

    // Snap direction to nearest grid axis (up, down, left, right)
    private Vector3 GetSnapDirection(Vector3 direction)
    {
        // Check if the absolute value of the x-component of the direction vector is greater than the absolute value of the z-component
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            // If the x-component is larger, snap to the x-axis direction
            // Use Mathf.Sign to determine the direction (1 for right, -1 for left)
            return new Vector3(Mathf.Sign(direction.x), 0, 0);
        }
        else
        {
            // If the z-component is larger or equal, snap to the z-axis direction
            // Use Mathf.Sign to determine the direction (1 for up, -1 for down)
            return new Vector3(0, 0, Mathf.Sign(direction.z));
        }
    }//end GetSnapDirection()
}
