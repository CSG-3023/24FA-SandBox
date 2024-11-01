/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : PipeTiles.cs
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

public class PipeTiles : MonoBehaviour
{
    //represent each directon of connection 
    [System.Flags] //allows for multi-select
    public enum Direction { 
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,
    }

    [SerializeField]
    private Direction _currentDirection; //current direction

    //Default Y rotation 
    [SerializeField]
    private float _defaultRotationY;

    private float _lastRotationY;        // To track rotation changes


    // Start is called before the first frame update
    void Start()
    {
        _defaultRotationY = transform.eulerAngles.y; //record default rotation

        //Setup with default direction 
        UpdateConnections();
    }//end Start()


    void UpdateConnections()
    {
        //Get the current Y rotation
        float rotationY = transform.eulerAngles.y;

        int rotationSteps = Mathf.RoundToInt((rotationY - _defaultRotationY) / 90) % 4;

        // Determine the current direction based on normalized rotation relative to the default
        _currentDirection = GetCurrentConnection(rotationSteps);

        // Log the active connections for debugging
        Debug.Log($"Current Direction after rotation: {_currentDirection}");

    }//end UpdateConnections()

     // Determine the active connections based on the normalized rotation steps
    private Direction GetCurrentConnection(int rotationSteps)
    {
        // Check the initial default direction to map rotation changes accordingly
        switch (_currentDirection)
        {
            case Direction.Left | Direction.Right:
                return rotationSteps switch
                {
                    0 => Direction.Left | Direction.Right, // 0 degrees
                    1 => Direction.Up | Direction.Down,    // 90 degrees
                    2 => Direction.Left | Direction.Right, // 180 degrees
                    3 => Direction.Up | Direction.Down,    // 270 degrees
                    _ => Direction.None,
                };
            case Direction.Up | Direction.Down:
                return rotationSteps switch
                {
                    0 => Direction.Up | Direction.Down,    // 0 degrees
                    1 => Direction.Left | Direction.Right, // 90 degrees
                    2 => Direction.Up | Direction.Down,    // 180 degrees
                    3 => Direction.Left | Direction.Right, // 270 degrees
                    _ => Direction.None,
                };
            default:
                return Direction.None;
        }
    }

    private void Update()
    {
        // Check if the Y rotation has changed
        float currentRotationY = transform.eulerAngles.y;

        if (!Mathf.Approximately(currentRotationY, _lastRotationY))
        {
            // Rotation has changed; update connections
            UpdateConnections();

            // Update last rotation to the new value
            _lastRotationY = currentRotationY;
        }
    }

}


