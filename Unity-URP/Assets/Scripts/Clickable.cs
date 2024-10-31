/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Sandbox
* FILE NAME       : Clickable.cs
* DESCRIPTION     : Object behaviours for being clicked on
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

public class Clickable : MonoBehaviour
{
    public bool clickedOn = false;

    // Update is called once per frame
    void Update()
    {
        CheckClick();
    }//end Update()

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; //object hit

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    OnClicked();
                }//end if(transform)

            }//end if(ray)


        }//end if(LeftMouseButton)
    }//end CheckClick()


    protected virtual void OnClicked()
    {
        clickedOn = true;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Debug.Log("Clicked on Object " + gameObject.name);
    }//end OnClick
}
