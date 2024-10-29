/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : FollowObject.cs
* DESCRIPTION     : Move game object to follow another game object
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
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowObject : MonoBehaviour
{
    public Transform Target; //reference to transform of object to follow

    [SerializeField]
    private float _speed = 2f; 

    // Update is called once per frame
    void Update()
    {
        //if there is a target
        if (Target != null)
        {
            FollowTarget();
        }
    }//end Update()

    //Follow the target
    void FollowTarget()
    {
        // Smoothly interpolate position between current position and target position
        transform.position = Vector3.Lerp(transform.position, Target.position, _speed * Time.deltaTime);

    }//end FollowTarget()





}
