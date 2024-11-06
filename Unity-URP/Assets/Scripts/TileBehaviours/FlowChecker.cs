/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : FlowChecker.cs
* DESCRIPTION     : Checks if pipe tiles allow for flow
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
using UnityEngine;

// This is a static class that is responsible for checking whether the water can flow through the pipes
public static class FlowChecker
{
    // This method checks the water flow for each neighbor connection
    public static void CheckFlow(PipeTiles currentTile, PipeTiles leftNeighbor, PipeTiles rightNeighbor, PipeTiles upNeighbor, PipeTiles downNeighbor)
    {
        bool canFlow = true; // Assume the water can flow initially

        // Get the current direction of the current tile
        PipeTiles.Direction currentDirection = currentTile.GetCurrentDirection();


        // Check flow for each neighbor (left, right, up, down)
        if (!CheckNeighborFlow(currentTile, currentDirection, leftNeighbor, PipeTiles.Direction.Left))
        {
            canFlow = false;
            Debug.LogError("Water cannot flow through the left connection.");
        }

        if (!CheckNeighborFlow(currentTile, currentDirection, rightNeighbor, PipeTiles.Direction.Right))
        {
            canFlow = false;
            Debug.LogError("Water cannot flow through the right connection.");
        }

        if (!CheckNeighborFlow(currentTile, currentDirection, upNeighbor, PipeTiles.Direction.Up))
        {
            canFlow = false;
            Debug.LogError("Water cannot flow through the up connection.");
        }

        if (!CheckNeighborFlow(currentTile, currentDirection, downNeighbor, PipeTiles.Direction.Down))
        {
            canFlow = false;
            Debug.LogError("Water cannot flow through the down connection.");
        }

        // Log if water can flow
        if (canFlow)
        {
            Debug.Log("Water can flow through the pipes.");
        }
        else
        {
            Debug.LogError("Water cannot flow through the pipes due to mismatched connections.");
        }//end if (canFlow)

    }//end CheckFlow()

  

    // This method checks the flow for a specific neighbor
    private static bool CheckNeighborFlow(PipeTiles currentTile, PipeTiles.Direction currentDirection, PipeTiles neighbor, PipeTiles.Direction direction)
    {
        // Check if the neighbor exists
        if (neighbor != null)
        {
            // Get the direction of the neighbor
            PipeTiles.Direction neighborDirection = neighbor.GetCurrentDirection();
            // Check if the current tile can flow to the neighbor
            if (!IsMatchingConnection(currentDirection, neighborDirection, direction))
            {
                LogMismatch(currentTile, direction); // Log mismatch
                return false; // Return false if there's a mismatch
            }
        }
        return true; // Return true if the neighbor connection is valid
    }//end CheckNeighborFlow()

    // Method to log a mismatch based on the direction
    private static void LogMismatch(PipeTiles currentTile, PipeTiles.Direction direction)
    {
        string directionName = direction.ToString(); // Get the direction name
        Debug.LogError("Mismatch at " + directionName.ToLower() + " neighbor: " + currentTile.gameObject.name + " can't flow " + directionName.ToLower());

    }//end Log error

    // Method to check if the directions match for a specific connection (Left, Right, Up, Down)
    private static bool IsMatchingConnection(PipeTiles.Direction currentDirection, PipeTiles.Direction neighborDirection, PipeTiles.Direction direction)
    {
        // Use a switch statement to check the specific direction
        switch (direction)
        {
            case PipeTiles.Direction.Left:
                // Check if the current direction has a left connection and the neighbor has a right connection
                return (currentDirection & PipeTiles.Direction.Left) != 0 && (neighborDirection & PipeTiles.Direction.Right) != 0;

            case PipeTiles.Direction.Right:
                // Check if the current direction has a right connection and the neighbor has a left connection
                return (currentDirection & PipeTiles.Direction.Right) != 0 && (neighborDirection & PipeTiles.Direction.Left) != 0;

            case PipeTiles.Direction.Up:
                // Check if the current direction has an up connection and the neighbor has a down connection
                return (currentDirection & PipeTiles.Direction.Up) != 0 && (neighborDirection & PipeTiles.Direction.Down) != 0;

            case PipeTiles.Direction.Down:
                // Check if the current direction has a down connection and the neighbor has an up connection
                return (currentDirection & PipeTiles.Direction.Down) != 0 && (neighborDirection & PipeTiles.Direction.Up) != 0;

            // If none of the cases match, return false
            default:
                return false;
        }//end switch(direction)

    }//end IsMatchingConnection()
}

