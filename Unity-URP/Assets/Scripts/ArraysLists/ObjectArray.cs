/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : ObjectArray.cs
* DESCRIPTION     : Places game objects in an array, when instantiated
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
*  2024/10/31   Akram Taghavi-Burris    class created
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArray : MonoBehaviour
{
    //Creates the global access to the ObjectArray
    public static ObjectArray Instance { get; private set; }

    // Master list of all objects
    public List<GameObject> objList = new List<GameObject>();


    // Awake is called once at instantiation
    void Awake()
    {
        // If no instance exists, assign this instance and mark it as persistent across scenes
        if (Instance == null)
        {
            Instance = this; //make this the instance
            DontDestroyOnLoad(gameObject); // Prevents the instance from being destroyed when changing scenes
        }
        // Else if a Instance already exists and it's not this one, destroy this instance to maintain the Singleton
        else
        {
            Destroy(gameObject); //if there is a instance destroy this object

        }//end if (Instance == null)

    } //end Awake()

   

    // Register Tiles to list 
    public void RegisterObject(GameObject obj)
    {
        objList.Add(obj);
        Debug.Log("Object added " + obj.name);
    }//end RegisterTile



}
