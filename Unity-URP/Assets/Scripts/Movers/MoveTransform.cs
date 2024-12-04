/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : MoveTransform.cs
* DESCRIPTION     : Move game object with speed and transform.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/26	Akram Taghavi-Burris    Created class
* 2024/10/28	    -                   Collision check added
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTransform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private Vector3 _direction = Vector3.right;

    public bool CanMove = true; 

    //Public property to get or set the speed of the object
    public float Speed { get { return _speed; } set { _speed = value; } }

    //Public property to get or set the direction of movement for the object
    public Vector3 Direction { get { return _direction; } set { _direction = value; } }


    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            Move();
        }
    }//end Update()


    ///<summary>
    /// Move the object by the transform position
    /// </summary>
    /// <param name="direction">The direction in which the projectile should move. If null, the default direction is used.</param>
    /// <param name="speed">The magnitude of the force to be applied. If null, the default speed is used.</param>
    public void Move(Vector3? direction = null, float? speed = null)
    {
      // Use the provided direction and speed, or fall back to instance variables
      Vector3 moveDirection = direction ?? _direction;
      float moveSpeed = speed ?? _speed;

      transform.position += moveDirection * moveSpeed * Time.deltaTime; //default movement

    }//end Move()



    private void OnCollisionEnter(Collision collision)
    {
        //Check if the other object is Static
        if (collision.gameObject.isStatic)
        {
            CanMove = false;
        }
    }//end OnCollisionEnter()






}
