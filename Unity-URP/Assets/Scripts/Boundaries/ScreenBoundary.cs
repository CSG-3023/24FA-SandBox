/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : ScreenBoundary.cs
* DESCRIPTION     : Boundary to keep objects on the screen
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

public class ScreenBoundary : MonoBehaviour
{
    private static Camera _mainCamera; //reference to main camera

    [Tooltip("Object padding for boundary checks")]
    [SerializeField] 
    private float _padding = 0f;

    private MoveTransform _moveTransform;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _moveTransform = GetComponent<MoveTransform>();
        //_playerController = GetComponent<PlayerController>();     
    }//end Start();

    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
    }//end Update()


    public static Vector3 ScreenToWorldPoints(Vector2 screenPosition)
    {
        // Calculate vector 2 position to world position
        return _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -_mainCamera.transform.position.z));
    }//end ScreenToWorld()

    //Check if boundary has been hit
    private void CheckBoundary()
    {
        // Default horizontal boundary check
        Vector3 screenLeftWorldPos = ScreenToWorldPoints(new Vector2(0, 0));
        Vector3 screenRightWorldPos = ScreenToWorldPoints(new Vector2(Screen.width, 0));

        if (transform.position.x >= screenRightWorldPos.x - _padding ||
            transform.position.x <= screenLeftWorldPos.x + _padding)
        {
            OnBoundaryHit(); // Base class sends notification
        }
    } // end CheckBoundary()

    private void OnBoundaryHit()
    {
        Debug.Log("Boundary Hit");
        _moveTransform.CanMove = false;
        //_playerController.CanMove = false;

    }//end OnBoundaryHit()

}
