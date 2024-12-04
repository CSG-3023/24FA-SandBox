/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : SandBox
* FILE NAME       : ProjectileSpawner.cs
* DESCRIPTION     : Spawns Projectiles
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
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab of projectile")]
    private GameObject _projectilePrefab;

    [SerializeField]
    [Tooltip("Spawn position of projectile")]
    private Transform _spawnPoint;

    [SerializeField]
    [Tooltip("Speed (or force) of projectile movement")]
    private float _speed = 50f;

    [SerializeField]
    [Tooltip("Direction to move the projectile")]
    private Vector3 _direction;

    [SerializeField]
    [Tooltip("Boolean for testing if can shoot")]
    private bool _canShoot = true;

    [SerializeField]
    [Tooltip("Cooldown time before next shot")]
    private float _cooldown = 0f;



    // Tracks the remaining cooldown time
    private float _cooldownTimer = 0f;   

    //The vector 3 value of the spawn point
    private Vector3 _spawnPointPosition;

    //Instance of the projectile
    private GameObject _projectile;


    // Update is called once per frame
    void Update()
    {

        //Set the direction to the direction you are facing 
        _direction = transform.forward;

        //When mouse is clicked and _canshoot true
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            _projectile = InstanaiateProjectile();

            ShootProjectile(); 

            //sets the cooldown timer
            SetCooldownTimer(); 
            
        }

        // Continuously check if cooldown needs to be updated
        HandleCooldown();

    }//end Update()

    GameObject InstanaiateProjectile()
    {
        // Get the spawn point's position at the time of shooting
        _spawnPointPosition = _spawnPoint.position;
        
        //instantiate projectile
        GameObject projectileInstance = Instantiate(_projectilePrefab, _spawnPointPosition, _spawnPoint.rotation);

        return projectileInstance;
    }//end InstatieProjectile()

    void SetCooldownTimer()
    {
        // Check if the player is allowed to shoot
        if (_canShoot) 
        {
            // Trigger shooting logic here (e.g., spawn projectile)
            Debug.Log("Player shoots!");

            //shooting is disabled
            _canShoot = false;

            // start timer with cooldown duration
            _cooldownTimer = _cooldown; 
        }//end if (_canShoot) 

    }//end SetCooldownTimer()

    private void HandleCooldown()
    {
        // Check if canshoot is disabled, run cooldown timer
        if (!_canShoot)
        {
            // Reduce the cooldown timer by the time elapsed since the last frame
            _cooldownTimer -= Time.deltaTime;
            Debug.Log("Cooldown time: " + _cooldownTimer);

            // Check if the cooldown timer has finished
            if (_cooldownTimer <= 0f)
            {
                // Re-enable shooting
                _canShoot = true;

                //Reset the timer
                _cooldownTimer = 0f;

                Debug.Log("Cooldown complete, shooting reenabled!");
            }//end if (_cooldownTimer <= 0f)

        }//end if (!_canShoot) 
    }//end HandelCooldown



    //Shoot projectile using the move component on the projectile 
    private void ShootProjectile()
    {
        //Get reference to the MovePhysics component on the projectile prefab
        MovePhysics moveProjectile = _projectile.GetComponent<MovePhysics>();


        //Set the CanMove to false to prevent it from moving on it's own 
        moveProjectile.CanMove = false;

        //Move the projectile with the desired direction and speed
        moveProjectile.Move(_direction, _speed);


    }//end ShootProjectile()



}
