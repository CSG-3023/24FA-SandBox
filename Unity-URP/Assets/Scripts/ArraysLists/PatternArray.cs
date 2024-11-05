/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : PatternArray.cs
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

public class PatternArray : MonoBehaviour
{
    private List<GameObject> _patternArray = new List<GameObject>(); // Array to hold the generated pattern sequence

    private ObjectArray _objectArray;

    private int _rows = 5;
    private int _columns = 5;

    void Start()
    {
        _objectArray = ObjectArray.Instance;
        GeneratePattern();
    }

    //Generate a random pattern sequence for the level
    private void GeneratePattern()
    {
        // Ensure the master array has enough tiles for the grid size
        if (_objectArray.objList.Count < _rows * _columns)
        {
            Debug.LogWarning("Master array does not contain enough tiles.");
            return;
        }

        //For every row get the random object
        for (int row = 0; row < _rows; row++)
        {
            // Calculate the starting index of the row in the 1D array
            int rowStartIndex = row * _columns;

            // Randomly select a column within the current row
            int randomColumn = Random.Range(0, _columns);

            // Get the object at the random column of the current row
            GameObject selectedObject = _objectArray.objList[rowStartIndex + randomColumn];
            _patternArray.Add(selectedObject);

        }//end For each row

        // Output the generated pattern sequence for verification
        Debug.Log("Generated pattern sequence:");
        foreach (GameObject obj in _patternArray)
        {
            Debug.Log(obj.name);
            Tiles tilesComponent = obj.GetComponent<Tiles>();
            tilesComponent.LightUp();
        }//end foreach

    }//end GeneratePatter()


}
