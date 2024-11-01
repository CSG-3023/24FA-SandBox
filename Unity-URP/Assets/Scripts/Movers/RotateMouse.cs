/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : RotateMouse.cs
* DESCRIPTION     : Rotate object with mouse click; requires clickable
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* * 2024/10/31   Akram Taghavi-Burris    class created
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clickable component is required
[RequireComponent(typeof(Clickable))]
public class RotateMouse : MonoBehaviour
{
    [Tooltip("Rotation angle per click in degrees")]
    [SerializeField]
    private float _rotationAngle = 45f;

    private Clickable _clickable;

    // Start is called before the first frame update
    private void Start()
    {
        _clickable = GetComponent<Clickable>();
        
    }//end Start()

    // Update is called once per frame
    private void Update()
    {
        if(_clickable.clickedOn)
        {
            Rotate();
        }
    }//end Update()

    // This method is called automatically when the object is clicked
    private void Rotate()
    {
        // Rotate the object by the specified angle around its Y-axis
        transform.Rotate(0, _rotationAngle, 0);
        _clickable.clickedOn = false;
    }//end OnMoudDown()
}
