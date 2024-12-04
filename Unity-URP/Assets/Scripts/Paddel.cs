/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : Paddel.cs
* DESCRIPTION     : Short Description of script.
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

public class Paddel : MonoBehaviour
{
    public float forceAmount = 10f;  // The amount of force to apply when colliding with the ball

    void OnCollisionEnter(Collision collision)
    {
        // Check if the paddle collides with the ball (make sure the ball has a Rigidbody)
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Get the Rigidbody of the ball (which is the object that the paddle collided with)
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRb != null)
            {
                // Get the direction of the collision (this will be perpendicular to the surface)
                Vector3 forceDirection = collision.contacts[0].normal;

                // Apply force in the opposite direction of the normal to make the ball move away
                ballRb.AddForce(-forceDirection * forceAmount, ForceMode.Impulse);

                // Optionally, you can also apply some horizontal force for more variety in the ball's movement
                Vector3 extraForce = new Vector3(0, 0, 5f);  // Adjust this for a more "paddle hit" effect
                ballRb.AddForce(extraForce, ForceMode.Impulse);
            }
        }
    }

}

    
