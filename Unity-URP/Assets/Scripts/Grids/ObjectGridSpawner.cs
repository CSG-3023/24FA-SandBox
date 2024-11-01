/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : ObjectGridSpawner.cs
* DESCRIPTION     : Spwans a grid of objects based on the position of the grid object (lower left hand corner)
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/31   Akram Taghavi-Burris    class created
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGridSpawner : MonoBehaviour
{
    [Tooltip("Prefab to create")]
    [SerializeField]
    private GameObject _prefab;

    [Tooltip("Number of colums in grid")]
    [SerializeField]
    private float _columns = 4f;

    [Tooltip("Number of rows in grid")]
    [SerializeField]
    private float _rows = 4f;

    [Tooltip("Space between objects")]
    [SerializeField]
    private float _spacing = 1.5f;

    // Grid's position based (this object)
    private Vector3 _gridPosition;

    //Reference to ObjectArray component
    private ObjectArray _objArray;

    // Start is called before the first frame update
    void Start()
    {
        _gridPosition = transform.position; //get grid (this) object's position
        GetObjectArray();
        SpawnGrid();
    }//end Start();


    //Get the ObjectArray component 
    private void GetObjectArray()
    {
        //Try to get ObjectArray component 
        if(gameObject.TryGetComponent(out ObjectArray component))
        {   
            //if component found, assign to reference
           _objArray = GetComponent<ObjectArray>(); 
        }

        return; //return out of method

    }//end GetObjectArray()


    //Spawn the grid of prefab objects. Grid 
    private void SpawnGrid()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                // Calculate the position for each prefab relative to the grid (this) object's position
                Vector3 position = _gridPosition + new Vector3(col * _spacing, 0, row * _spacing);

                //Instaniate the prefab instance
                GameObject _prefabInstance = Instantiate(_prefab, position, Quaternion.identity);

                //Name the instance in the hierarchy based on the prefab name and row_column value
                _prefabInstance.name = _prefab.name +"_"+ row.ToString() + "_" + col.ToString();



            }//end for(column)

        }//end for(row)

    }//end SpawnGrid()


}
