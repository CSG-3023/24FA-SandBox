/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : PipeTiles.cs
* DESCRIPTION     : Checks rotation orientation of the pipe
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
*  2024/11/03   Akram Taghavi-Burris    class created
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class PipeTiles : MonoBehaviour
{
    //represent each direction of connection 
    [System.Flags] //allows for multi-select
    public enum Direction { 
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,
    }//end enum Direction

    //Enum to set the axes of rotation
    public enum RotationAxis
    {
        X, 
        Y, 
        Z 
    }//end enum RotationAxis

    [SerializeField]
    private RotationAxis _rotationAxis = RotationAxis.Z; // Default to Z-axis rotation

    [SerializeField]
    private Direction _initialDirection; // Default direction

    private Direction _currentDirection; //current direction
    private float _defaultRotation; //Default Y rotation 
    private float _lastRotation; // To track rotation changes

    //References to all neighbor tiles 
    private PipeTiles _leftNeighbor;
    private PipeTiles _rightNeighbor;
    private PipeTiles _upNeighbor;
    private PipeTiles _downNeighbor;


    // Start is called before the first frame update
    void Start()
    {
        _defaultRotation = GetRotationAxis(); // Store the initial rotation angle
        _lastRotation = _defaultRotation; //set the last rotation to the default rotation on start
        _currentDirection = _initialDirection; // Set initial pipe direction
        UpdateConnections(GetRotationAxis()); // Update connections based on the initial direction
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        // Get the current rotation from axis
        float currentRotation = GetRotationAxis(); 

        //Check if the rotation has changed from last rotation
        if (!Mathf.Approximately(currentRotation, _lastRotation))
        {
            // Rotation has changed; update connections
            UpdateConnections(currentRotation);

            // Update last rotation to the new value
            _lastRotation = currentRotation;
        }
    }//end Update()

    //Get the rotation Axis from inspector
    private float GetRotationAxis()
    {
        //Check the rotationAxis set
        switch (_rotationAxis)
        {
            case RotationAxis.X:
               return transform.eulerAngles.x; 
            case RotationAxis.Y:
                return transform.eulerAngles.y;
            case RotationAxis.Z:
                return transform.eulerAngles.z;
            default:
                return 0f;
        }//end Switch

    }//end GetRotationAxis()

    // Update connections when rotated
    void UpdateConnections(float currentRotation)
    {
        // Calculate rotation steps based on the difference from the last rotation
        int rotationSteps = Mathf.FloorToInt((currentRotation - _lastRotation) / 90f) % 4; // Assuming 90 degrees per step
        _lastRotation = currentRotation; // Update last rotation

        // Get the current connection based on the rotation steps
        _currentDirection = GetCurrentConnection(rotationSteps);

        // Log the active connections for debugging , will output object name and current pipe direction
        Debug.Log("Current Direction after rotation: " + gameObject.name + " " + _currentDirection);
    }


    // Determine the current connection based on the rotation steps
    private Direction GetCurrentConnection(int rotationSteps)
    {
        // Determine the current connection based on the rotation steps
        return RotateDirection(_currentDirection, rotationSteps);

    }//end GetCurrentConnection()

    // Rotate the connection based on current direction
    private Direction RotateDirection(Direction currentDirection, int rotationSteps)
    {
        for (int i = 0; i < rotationSteps; i++)
        {
            // Rotate the combined directions using bitwise operations
            switch (currentDirection)
            {
                case Direction.Left | Direction.Right:
                    currentDirection = Direction.Up | Direction.Down; // 90-degree clockwise rotation
                    break;
                case Direction.Up | Direction.Down:
                    currentDirection = Direction.Right | Direction.Left; // 90-degree clockwise rotation
                    break;
                case Direction.Left | Direction.Up:
                    currentDirection = Direction.Right | Direction.Up; // 90-degree clockwise rotation
                    break;
                case Direction.Left | Direction.Down:
                    currentDirection = Direction.Left | Direction.Up; // 90-degree clockwise rotation
                    break;
                case Direction.Right | Direction.Up:
                    currentDirection = Direction.Right | Direction.Down; // 90-degree clockwise rotation
                    break;
                case Direction.Right | Direction.Down:
                    currentDirection = Direction.Left | Direction.Down; // 90-degree clockwise rotation
                    break;
                case Direction.None:
                default:
                    return Direction.None; // No valid connection
            }
        }

        return currentDirection;
    }//end RotateDirection()


    // Call this method when you need to check the flow
    public void CheckWaterFlow()
    {
        // Ensure neighbors are correctly passed to FlowChecker
        FlowChecker.CheckFlow(this, _leftNeighbor, _rightNeighbor, _upNeighbor, _downNeighbor);
    }//end CheckWaterFlow()

    // Getter for the current direction to be passed to FlowChecker
    public Direction GetCurrentDirection()
    {
        return _currentDirection;
    }


}


