using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFunctions : MonoBehaviour
{

    public Camera mainCamera;
    //Bool that keeps track if a levitatable object is being dragged over the screen
    private bool isDragging;
    
    private Rigidbody cubeObject;
    private PlayerAbility playerAbility;
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;
    private PlayerCollision playerCollision;
    public GameObject player;
    public GameObject particleSystem;
    RaycastHit hit;

    //Float value and Vector3 that keep the force applied to the object in relation to the distance between the object's position and mouseposition 
    public float forceMultiplier;
    public float distance;
    public float force;
    public Vector3 directionVector;
    public Vector3 startVector;
    private Vector3 mousePosition;
    private Ray mousePositionRay;
    private Plane projectionPlane;

    private Sound levitationSound;

    private bool playedSong;

    
    // Use this for initialization
    void Start ()
    {
        cubeObject = GetComponent<Rigidbody>();
        playerAbility = player.GetComponent<PlayerAbility>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerCollision = player.gameObject.GetComponent<PlayerCollision>();
        particleSystem = player.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        isDragging = false;
        forceMultiplier = 2;
        playedSong = false;
        levitationSound = FindObjectOfType<AudioManager>().FindSound("Levitation");
        force = 0.5f;

    }

    //On leftclick mousepressed down the player can levitate objects if he is in playerstate==1 (wizard)
    //in that case the player cannot move anymore, the particle is applied on the player and the target object
    private void OnMouseDown()
    {
        if (playerAbility.playerState == 1 && playerAbility.wizardAlive && playerMovement.isGrounded)
        {
            //playerRigidbody.isKinematic = true;
            //particleSystem.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            //Not used in  this project but it plays a sound when you levitate an object
            ////if (!playedSong)
            ////{
            ////    //levitationSound.source.volume = 0.42f;
            ////   // FindObjectOfType<AudioManager>().PlayMusic("Levitation");
            ////    playedSong = true;
            ////    //releaseMouse = false;
                
            ////}
            ///

            // Calculates the direction for the object in which it should later move towards to
            mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            projectionPlane = new Plane(Vector3.forward, transform.position);
            distance = 0;

            if (projectionPlane.Raycast(mousePositionRay, out distance))
            {
                mousePosition = mousePositionRay.GetPoint(distance);
                startVector = transform.position;
                directionVector = mousePosition - startVector;

            }
            gameObject.transform.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnMouseDrag()
    {
        // Security to keep the player stuck in place
        if (playerAbility.playerState == 1 && playerAbility.wizardAlive && !playerCollision.isInAir)
        {
            //playerRigidbody.isKinematic = true;
            //particleSystem.SetActive(true);

            if (!isDragging)
            {
                isDragging = true;
                playerMovement.isDragging = true;
            }
            // Calculates the direction for the object in which it should later move towards to
            mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (projectionPlane.Raycast(mousePositionRay, out distance))
            {
                mousePosition = mousePositionRay.GetPoint(distance);
                startVector = transform.position;
                directionVector = mousePosition - startVector;
            }
        }
    }

    private void OnMouseUp()
    {
       
        //Not used in this project
        // Stop sound when you stop levitating
        //////if (levitationSound.source.volume <= 0)
        //////{
        ////    FindObjectOfType<AudioManager>().StopMusic("Levitation");
        ////    playedSong = false;
        //////}
        ///

        //Frees player from kinematic state and shut down the particles systems
        //playerRigidbody.isKinematic = false;
       // particleSystem.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //By mousepressed release a force is apllied that will shot an object in the direction of the directionvector
        if (playerAbility.playerState == 1)
        {
            if (isDragging)
            {
                isDragging = false;
                playerMovement.isDragging = false;
            }
            cubeObject.AddForce(directionVector* forceMultiplier, ForceMode.Impulse);
            gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            cubeObject.AddForce(directionVector* forceMultiplier, ForceMode.Impulse);
            gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
        }
       

    }

    public void FixedUpdate()
    {
        //during levitation it is necessary to keep track if the player is floating when certain objects are moved to remove the platform under the player
        //in that case the player should stop the levitation process and physik should apply on the player
        //if(isDragging)
        //{
        //    //if the player should be in the air while being in levitation state then the physics should be applied on the player
        //    if (playerCollision.isInAir)
        //    {
        //        isDragging = false;
        //        playerRigidbody.isKinematic = false;
        //        directionVector = Vector3.zero;
        //        gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
        //        cubeObject.velocity = Vector3.zero;
        //        particleSystem.SetActive(false);
        //    }
        //}
        if (isDragging && !playerCollision.isInAir)
           cubeObject.velocity = directionVector * force;

    }
}
