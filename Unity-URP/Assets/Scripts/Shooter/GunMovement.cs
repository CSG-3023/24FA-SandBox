/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : GunMoveemnt.cs
* DESCRIPTION     : Aims the gun by rotating it based on mouse input.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/12/03	Akram Taghavi-Burris    Created class
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class GunMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    [Tooltip("How fast the gun moves side to side")]
    public float horizontalSpeed = 5.0f;
    [Tooltip("Max distance the gun can move")]
    public float maxHorizontalDistance = 5.0f;

    [Header("Vertical Movement Settings")]
    [Tooltip("Max distance the gun can move up")]
    public float maxVerticalDistance = 2.0f;
    [Tooltip("Max distance the gun can move down")]
    public float minVerticalDistance = -1.0f;

    [Header("Rotation Settings")]
    [Tooltip("How fast the gun rotates")]
    public float rotationSpeed = 5.0f;
    [Tooltip("Maximum upward angle (degrees)")]
    public float maxTiltAngle = 10f;
    [Tooltip("Maximum downward angle (degrees)")]
    public float minTiltAngle = -10f;

    private Vector3 initialPosition; // The gun's starting position for reference

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial position of the gun to keep it locked vertically
        initialPosition = transform.position;
    }//end Start()


    // Update is called once per frame
    void Update()
    {
        //Get mouse position
        Vector3 mousePos3D = GetMouseWorldPosition();

        // Pass the mouse position to the movement and tilt methods
        MoveGun(mousePos3D);
        TiltGunVertically(mousePos3D);
    }

    //Calculate the Mouse position from screen position to 3D world space
    private Vector3 GetMouseWorldPosition()
    {
        // Get the current screen position of the mouse
        Vector3 mousePos2D = Input.mousePosition;

        // Set the z position based on the camera’s position
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert from 2D screen space to 3D world space
        return Camera.main.ScreenToWorldPoint(mousePos2D);

    }//end GetMouseWorldPosition()


    //Move the gun horizontally with mouse
    private void MoveGun(Vector3 movePosition)

    {
        // Clamp the gun's X and Y position within the allowed range
        float clampedX = Mathf.Clamp(movePosition.x, initialPosition.x - maxHorizontalDistance, initialPosition.x + maxHorizontalDistance);
        float clampedY = Mathf.Clamp(movePosition.y, initialPosition.y + minVerticalDistance, initialPosition.y + maxVerticalDistance);

        // Update the gun's position
        transform.position = new Vector3(clampedX, clampedY, initialPosition.z);
    }//end MoveGunHorizontally()


    //Tilt Gun Vertically with mouse
    private void TiltGunVertically(Vector3 verticalPosition)
    {
        // Calculate the tilt angle based on the mouse's Y position relative to the gun's initial position
        // Mathf.InverseLerp finds the percentage of where the mouse's Y position is in relation to our minimum and maximum tilt angle to find the actual "titlAnagle"
        float tiltAngle = Mathf.Lerp(maxTiltAngle, minTiltAngle, Mathf.InverseLerp(initialPosition.y + minVerticalDistance, initialPosition.y + maxVerticalDistance, verticalPosition.y));

        // Apply the tilt to the gun's Z rotation
        transform.localEulerAngles = new Vector3(tiltAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }//end TiltGunVertically

}
