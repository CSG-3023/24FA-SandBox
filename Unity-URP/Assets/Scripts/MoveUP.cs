/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : MoveUP.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2000/01/01		Developer's Name    		 Created <short comment of changes>
* 
*
/******************************************************************/
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object moves up

    void Update()
    {
        // Check if the "W" key or Up Arrow key is pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move the object up along the y-axis
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
}