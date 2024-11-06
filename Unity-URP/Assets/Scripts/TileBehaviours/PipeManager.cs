/*******************************************************************
/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : PipeManager.cs
* DESCRIPTION     : Check all tiles
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

public class PipeManager : MonoBehaviour
{
    // List to hold all pipe tiles in the scene
    private List<PipeTiles> allPipeTiles = new List<PipeTiles>();

    // Call this method to gather all pipe tiles in the scene
    private void Start()
    {
        // Find all PipeTiles objects in the scene and add them to the list
        allPipeTiles.AddRange(FindObjectsOfType<PipeTiles>());

    }//end Start

    // Method to be called by the button to check water flow
    public void CheckAllWaterFlow()
    {
        // Iterate through each tile and check water flow
        foreach (PipeTiles tile in allPipeTiles)
        {
            tile.CheckWaterFlow();
        }
    }//end CheckAllWatterFlow()
}

