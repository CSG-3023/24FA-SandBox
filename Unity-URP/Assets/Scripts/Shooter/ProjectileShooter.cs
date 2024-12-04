/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : ProjectileShooter.cs
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
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Tooltip("The amount of force applied to the projectile")]
    public float ShootingForce = 20f;

    [SerializeField]
    [Tooltip("Prefab of projectile")]
    private GameObject _projectilePrefab;

    [SerializeField]
    [Tooltip("Spawn position of projectile")]
    private Transform _spawnPoint;

    [SerializeField]
    [Tooltip ("The distance from the camera, or depth that the target object is at.")]
    private float _targetDistance = 10f;

    //Instance of the projectile
    private GameObject _projectile;

    //The vector 3 value of the spawn point
    private Vector3 _spawnPointPosition;

    //The target position to shoot towards
    private Vector3 _targetPosition;


    // Update is called once per frame
    void Update()
    {
        //When mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Use mouse position as target position
            _targetPosition = GetMouseWorldPosition();

            Shoot();
            Rigidbody rb = _projectile.GetComponent<Rigidbody>();
            Debug.Log("velocity " + rb.velocity);

        }
//Debug.Log("Projectile moving " + _projectile.transform.position);
    }//end Update

    //Calculate the Mouse position from screen position to 3D world space
    private Vector3 GetMouseWorldPosition()
    {
        // Get the current screen position of the mouse
        Vector3 mousePos2D = Input.mousePosition;

        // Set the Z position based on the camera’s position (at the same distance as _targetDistance)
        mousePos2D.z = _targetDistance; // Distance from the camera to the target

        // Set the z position based on the camera’s position
        //mousePos2D.z = -Camera.main.transform.position.z;

        // Convert from 2D screen space to 3D world space
        return Camera.main.ScreenToWorldPoint(mousePos2D);

    }//end GetMouseWorldPosition()

    void Shoot()
    {
        //Get the shootDirection vector 
        //Vector3 shootDirection = CalculateShootDirection();

        // Debugging: Log the direction and position
       // Debug.Log("Shoot Direction: " + shootDirection);
        Debug.Log("Target Position: " + _targetPosition);
        Debug.Log("Spawn Point Position: " + _spawnPointPosition);

        // Draw a red line to visualize the direction of the shot
        //Debug.DrawLine(_spawnPointPosition, _spawnPointPosition + shootDirection * 50f, Color.red, 2f);
        Debug.DrawRay(_spawnPoint.position, _spawnPoint.forward * 5f, Color.red, 2f);
        // Get the spawn point's position at the time of shooting
        _spawnPointPosition = _spawnPoint.position;

        _projectile = InstanaiateProjectile();
        Rigidbody rb = _projectile.GetComponent<Rigidbody>();

        // Disable gravity on the projectile's Rigidbody for controlled movement
        //rb.useGravity = false;

        // Clear any existing velocity to avoid unwanted initial motion
        //rb.velocity = Vector3.zero;

        Vector3 forceDirection = new Vector3(0, 0, 1);
      
        // Add a force to move the projectile forward (along the Z-axis relative to the spawn point)
        rb.AddForce(_projectile.transform.forward * ShootingForce, ForceMode.Impulse);
   
// Debug.Log("Spawn Point Forward Direction: " + (forceDirection * ShootingForce));
        // Rotate the projectile to face the direction of travel
      //  _projectile.transform.forward = shootDirection;

        // Re-enable gravity after the force is applied for more realistic movement
       //rb.useGravity = true;

    }//end Shoot

    //Calculate the direction to shoot 
    Vector3 CalculateShootDirection()
    {
        // Get the spawn point's position at the time of shooting
        _spawnPointPosition = _spawnPoint.position;

        // The Z direction is always forward (the gun's forward direction)
        Vector3 forwardDirection = _spawnPoint.forward;

        // Get the difference between the target's X and Y coordinates and the spawn point's position
        Vector3 targetDirection = _targetPosition - _spawnPointPosition;

        // We only want the X and Y components of the direction to be influenced by the mouse.
        targetDirection.z = 0;  // Set Z to 0 to keep the direction horizontal

        // Normalize the direction for correct movement scaling
        targetDirection.Normalize();

        // Combine the forward direction with the horizontal (X,Y) direction
        // This will ensure the projectile always moves forward, but will adjust for X and Y
        Vector3 shootDirection = forwardDirection + targetDirection;

        // Normalize the direction vector (to get just direction, not distance)
        shootDirection.Normalize(); //Doing this here avoids having it in the AddForce() parameter

        return shootDirection;

    }//end CalculateShootDireciton()

    //Create the instance of the projectile
    GameObject InstanaiateProjectile()
    {
        GameObject projectileInstance = Instantiate(_projectilePrefab, _spawnPointPosition, _spawnPoint.rotation);
        Debug.Log(projectileInstance.name);
        return projectileInstance;
    }//end InstatieProjectile()



}
