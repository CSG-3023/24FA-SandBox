/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : SpawnMonster.cs
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

public class SpawnMonster : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnLocaiton;
    [SerializeField]
    private GameObject _monster;

    private Clickable _clickableComponent;

    private void Start()
    {
       _clickableComponent = GetComponent<Clickable>();
    }//end 

    private void Update()
    {
        if (_clickableComponent.clickedOn)
        {
            SpawnMosnters();
        }
    }


    private void SpawnMosnters()
    {
        Vector3 pos = _spawnLocaiton.position;
        Instantiate(_monster, pos, Quaternion.identity) ;

        _clickableComponent.clickedOn = false;
    }

}
