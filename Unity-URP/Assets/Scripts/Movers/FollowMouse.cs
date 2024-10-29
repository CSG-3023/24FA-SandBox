/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : FollowMouse.cs
* DESCRIPTION     : Move object with mouse
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

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition(); //get mouse position
        UpdateObjectPosition(mousePosition.x, mousePosition.y); //update mouse position for x and y
    }//end Update()


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

    //Update object position to mouse x and y
    private void UpdateObjectPosition(float xPosition, float yPosition)
    {
        // Update both x and y positions of the game object
        Vector3 pos = transform.position;
        pos.x = xPosition;
        pos.y = yPosition;
        transform.position = pos;

    }//end UpdateObjectPosition()
}
