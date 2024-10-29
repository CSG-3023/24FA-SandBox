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

public class MoveTransform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private Vector3 _direction = Vector3.right;
    private bool _canMove = true; 

    //Public property to get or set the speed of the object
    public float Speed { get { return _speed; } set { _speed = value; } }

    //Public property to get or set the direction of movement for the object
    public Vector3 Direction { get { return _direction; } set { _direction = value; } }


    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            Move();
        }
    }//end Update()

    public void Move()
    {
        transform.position += _direction * _speed * Time.deltaTime; //default movement
    }//end Move()

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the other object is Static
        if (collision.gameObject.isStatic)
        {
            _canMove = false;
        }
    }//end OnCollisionEnter()






}
