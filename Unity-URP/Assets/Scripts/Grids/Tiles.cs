/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : Tiles.cs
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

public class Tiles : MonoBehaviour
{
    [SerializeField]
    private Material tileMaterial; // Reference to the tile's material

    private Material instanceMaterial; // Reference to the instance of the material

    [SerializeField]
    private Color lightUpColor = Color.yellow; // Color to use for the light-up effect

    [SerializeField]
    private float lightUpDuration = 0.5f; // Duration of the light-up effect

    private Color originalColor;

    [SerializeField]
    private bool isLightingUp = false;

    [SerializeField]
    private bool _canLightUp = false;

    // Awake is called once at instantiation
    void Awake()
    {
        //If an ObjectArray exsists
        if(ObjectArray.Instance != null)
        {
            ObjectArray.Instance.RegisterObject(this.gameObject);
        }else{
            Debug.LogWarning("ObjectArray component not found in the scene.");
        }
    
    }//end Awake

    private void Start()
    {
        MaterialSetup();
    }//end Strat()

    private void Update()
    {
        if (_canLightUp) { LightUp(); }

    }//end Update()

    private void MaterialSetup()
    {
        // Create a new instance of the material
        instanceMaterial = new Material(tileMaterial);

        // Assign the instance material to the renderer
        GetComponent<Renderer>().material = instanceMaterial;

        // Store the original color of the material
        originalColor = instanceMaterial.GetColor("_EmissionColor");

        // Enable emission on the material
        instanceMaterial.EnableKeyword("_EMISSION");

    }//end MaterialSetup()

    public void LightUp()
    {
        //If we are not already lighting up
        if (!isLightingUp)
        {
            StartCoroutine(LightUpEffect());
        }
    }//end LightUp()


    //Run the Lighup effect 
    private IEnumerator LightUpEffect()
    {
        isLightingUp = true;
        float elapsedTime = 0f;

        // Increase the emission intensity
        while (elapsedTime < lightUpDuration)
        {
            float intensity = Mathf.PingPong(elapsedTime / lightUpDuration, 1f);
            instanceMaterial.SetColor("_EmissionColor", lightUpColor * intensity);
            elapsedTime += Time.deltaTime;
            yield return null;
        }//end while

        // Return to original emission color
        instanceMaterial.SetColor("_EmissionColor", originalColor);
        isLightingUp = false;
    }

}
