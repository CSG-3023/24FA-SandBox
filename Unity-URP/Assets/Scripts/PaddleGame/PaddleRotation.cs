/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : PaddleRotation.cs
* DESCRIPTION     : Controlls rotation motion of paddle
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

public class PaddleRotation : MonoBehaviour
{
    [Tooltip("The speed of rotation for paddle toggle")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("The maximum rotation on the paddle")]
    [SerializeField]
    private float _maxRotation = 45f;

    // Reference to the original rotation of the paddle
    private Quaternion _originalRotation;



    // Start is called before the first frame update
    private void Start()
    {
        // Save the initial rotation of the paddle
        _originalRotation = transform.rotation;
    }//end Start()

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RotatePaddleUp();
        }
    else
        {
            ReturnPaddleToOriginal();
        }//end if/else

    }//end Update


    private void RotatePaddleUp()
    {
        //The targetRotation is a Quaternion that is calculated by a the Quaternion conversion of a Euler angle (Quterion.Euler). Euler angles being pitch (X), yaw (Y) and roll (Z). 
        //A new vector3 with only a Z-axis value set by the _maxRotation is being added to the current Euler angle value (i.e. transform)
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, _maxRotation));

        // Here we rotate the paddle from its current rotation towards the target rotation.
        // Quaternion.RotateTowards smoothly interpolates between two rotations at a given speed (_rotationSpeed).
        // Time.deltaTime ensures the movement is frame rate independent.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

    }//end RotatePaddleUp()

    private void ReturnPaddleToOriginal()
    {
        // Rotate the paddle back to its original rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _originalRotation, _rotationSpeed * Time.deltaTime);
    }//end ReturnPaddleToOriginal()

}