using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour {

    public GameObject summonableCube;
    private Rigidbody playerRigidbody;
    private MouseFunctions mouseFunctionsSummonableCube;
    public Transform currentModel;
    public KnightFunctions knightFunctions;
    public PlayerMovement playerMovement;
    
    public Camera mainCamera;
    private Ray summonableCubeRay;
    private Vector3 summonableCubePosition;
    private bool activeObject;
    private Plane projectionPlane;
    private float distance;
    public int playerState;
    public int activehealth;
    public float thiefHealth;
    public float wizardHealth;
    public float knightHealth;
    public float maxHealth;
    public bool knightAlive;
    public bool thiefAlive;
    public bool wizardAlive;
    public bool deathConfirmed;

    private GameObject wizardChild;
    private GameObject thiefChild;
    private GameObject knightChild;

    public Image wizardImage;
    public Image thiefImage;
    public Image knightImage;


    private GrapplingHook grapple;
    private Bow bow;
    private ParticleSystem particleCube;
    private bool spawnSmoke;

    private int thiefSpeed;
    private int knightSpeed;
    private int wizardSpeed;


    // Use this for initialization
    void Start () {
        activeObject = false;
        playerState = 1;
        maxHealth = 5;
        thiefHealth = maxHealth;
        wizardHealth = maxHealth;
        knightHealth = 8;

        knightAlive = true;
        thiefAlive = true;
        wizardAlive = true;
        deathConfirmed = false;
        spawnSmoke = false;
        wizardChild = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        thiefChild = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        knightChild = gameObject.transform.GetChild(2).GetChild(0).gameObject;

        grapple = GetComponent<GrapplingHook>();
        bow = GetComponent<Bow>();
        mouseFunctionsSummonableCube = summonableCube.GetComponent<MouseFunctions>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        knightFunctions = gameObject.GetComponent<KnightFunctions>();

        particleCube = summonableCube.GetComponent<ParticleSystem>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        thiefSpeed = 10;
        knightSpeed = 10;
        wizardSpeed = 10;
}
	
	// Update is called once per frame
	void Update ()
    {
        //the Player is still alive this block should be executed
        if (!deathConfirmed)
        {
            // Switch case is there for the switch from wizard to any other character and vice versa
            //in Case 1 the wizard will be used and the wizard can used his spawnCube() function to summon a cube
            // if 2 or 3 is pressed the player be will be swtich to the thief or knight and enabling its abilites while setting the current character to not active
            // the switch to the a specific can olny occure that character is still alive
            //Furthermore the movementspeed will be adjusted for the active character and the active character will be highlighted
            switch (playerState)
            {
                case 1:
                    
                    SpawnCube();
                    if (Input.GetKeyDown("2"))
                    {
                        if (thiefHealth > 0 && thiefAlive)
                        {
                            playerMovement.speed = thiefSpeed;
                            playerState = 2;
                            wizardChild.SetActive(false);
                            thiefChild.SetActive(true);
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 1f);
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 0.18f);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    else if (Input.GetKeyDown("3"))
                    {
                        if (knightHealth > 0 && knightAlive)
                        {
                            playerState = 3;
                            playerMovement.speed = knightSpeed;
                            wizardChild.SetActive(false);
                            knightChild.SetActive(true);
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 1f);
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 0.18f);
                        }
                    }
                    break;
                case 2:
                    grapple.GrapplingShoot();
                    bow.BowShot();
                    if (Input.GetKeyDown("1"))
                    {
                        if (wizardHealth > 0 && wizardAlive)
                        {
                            playerState = 1;
                            playerMovement.speed = wizardSpeed;
                            mouseFunctionsSummonableCube.enabled = true;
                            wizardChild.SetActive(true);
                            thiefChild.SetActive(false);
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 0.18f);
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 1f);
                        }
                    }
                    else if (Input.GetKeyDown("3"))
                    {
                        if (knightHealth > 0 && knightAlive)
                        {
                            playerState = 3;
                            playerMovement.speed = knightSpeed;
                            knightChild.SetActive(true);
                            thiefChild.SetActive(false);
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 0.18f);
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 1f);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    break;
                case 3:
                    
                    if (Input.GetKeyDown("1"))
                    {
                        knightFunctions.ResetCarryState();
                        knightFunctions.ResetShieldState();
                        if (wizardHealth > 0 && wizardAlive)
                        {
                            playerState = 1;
                            playerMovement.speed = wizardSpeed;
                            mouseFunctionsSummonableCube.enabled = true;
                            wizardChild.SetActive(true);
                            knightChild.SetActive(false);
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 0.18f);
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 1f);
                        }
                    }
                    else if (Input.GetKeyDown("2"))
                    {
                        knightFunctions.ResetCarryState();
                        knightFunctions.ResetShieldState();
                        if (thiefHealth > 0 && thiefAlive)
                        {
                            playerState = 2;
                            playerMovement.speed = thiefSpeed;
                            thiefChild.SetActive(true);
                            knightChild.SetActive(false);
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 1f);
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 0.18f);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    break;
            }
        }
        else
        {
            //if a player has died, you cannot move the character anymore and you are forced to switch to a different character
            //except for that this switch case does the same thing as the previous one
           playerRigidbody.useGravity = false;
           playerRigidbody.isKinematic = true;
            switch (playerState)
            {
                case 1:
                    wizardImage.color = new Color(0, 0, 0, 0.18f);
                    if (Input.GetKeyDown("2"))
                    {
                        if (thiefHealth > 0 && thiefAlive)
                        { 
                            playerState = 2;
                            playerRigidbody.useGravity = true;
                             playerRigidbody.isKinematic = false;
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 1f);
                           
                            deathConfirmed = false;
                            thiefChild.SetActive(true);
                            wizardChild.SetActive(false);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    else if (Input.GetKeyDown("3"))
                    {
                        if (knightHealth > 0 && knightAlive)
                        {
                            playerState = 3;
                            playerRigidbody.useGravity = true;
                            playerRigidbody.isKinematic = false;
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 1f);
                           
                            deathConfirmed = false;
                            knightChild.SetActive(true);
                            wizardChild.SetActive(false);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    break;
                case 2:
                    thiefImage.color = new Color(0, 0, 0, 0.18f);
                    if (Input.GetKeyDown("1"))
                    {
                        if (wizardHealth > 0 && wizardAlive)
                        {
                            playerState = 1;
                            mouseFunctionsSummonableCube.enabled = true;
                            playerRigidbody.useGravity = true;
                            playerRigidbody.isKinematic = false;
                           
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 1f);
                            deathConfirmed = false;
                            wizardChild.SetActive(true);
                            thiefChild.SetActive(false);
                        }
                    }
                    else if (Input.GetKeyDown("3"))
                    {
                        if (knightHealth > 0 && knightAlive)
                        {
                            playerState = 3;
                            playerRigidbody.useGravity = true;
                            playerRigidbody.isKinematic = false;
                           
                            knightImage.color = new Color(knightImage.color.r, knightImage.color.g, knightImage.color.b, 1f);
                            deathConfirmed = false;
                            knightChild.SetActive(true);
                            thiefChild.SetActive(false);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    break;
                case 3:
                    knightImage.color = new Color(0,0,0, 0.18f);
                    if (Input.GetKeyDown("1"))
                    {
                        knightFunctions.ResetCarryState();
                        if (wizardHealth > 0 && wizardAlive)
                        {
                            playerState = 1;
                            mouseFunctionsSummonableCube.enabled = true;
                            playerRigidbody.useGravity = true;
                            playerRigidbody.isKinematic = false;
                           
                            wizardImage.color = new Color(wizardImage.color.r, wizardImage.color.g, wizardImage.color.b, 1f);
                            deathConfirmed = false;
                            wizardChild.SetActive(true);
                            knightChild.SetActive(false);
                        }
                    }
                    else if (Input.GetKeyDown("2"))
                    {
                         knightFunctions.ResetCarryState();
                        if (thiefHealth > 0 && thiefAlive)
                        {
                            playerState = 2;
                            playerRigidbody.useGravity = true;
                            playerRigidbody.isKinematic = false;
                            thiefImage.color = new Color(thiefImage.color.r, thiefImage.color.g, thiefImage.color.b, 1f);
                           
                            deathConfirmed = false;
                            thiefChild.SetActive(true);
                            knightChild.SetActive(false);
                        }
                        mouseFunctionsSummonableCube.enabled = false;
                    }
                    break;
            }
        }
	}

    //Wizard that spawns or despawns the cube
    void SpawnCube()
    {
       if (Input.GetMouseButtonDown(1))
       {
            //if an cube is already spawn, it will be despawned
            //this means the cube will be set a position far away from the camera and physcis will stop applying on it
            if (activeObject)
            {
                activeObject = false;
                summonableCube.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
                summonableCube.GetComponent<Rigidbody>().useGravity = false;
                summonableCube.GetComponent<Rigidbody>().isKinematic = true;
                summonableCube.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                //before the cube is there, there will be a particle system active for short period of time and this block will stop the particle system
                if (!activeObject && spawnSmoke)
                {
                    activeObject = false;
                    spawnSmoke = false;
                    summonableCube.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
                    summonableCube.GetComponent<Rigidbody>().useGravity = false;
                    summonableCube.GetComponent<Rigidbody>().isKinematic = true;
                    summonableCube.GetComponent<MeshRenderer>().enabled = false;
                }
                //start the particle system before the cube is visible in the scene
                //calculate the exacte location where the cube should spawn
                else
                    {
                    particleCube.Play();
                    spawnSmoke = true;
                    FindObjectOfType<AudioManager>().PlayMusic("Conjuration");
                    summonableCubeRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                    projectionPlane = new Plane(Vector3.forward, transform.position);
                    distance = 0;
                    if (projectionPlane.Raycast(summonableCubeRay, out distance))
                    {
                        summonableCubePosition = summonableCubeRay.GetPoint(distance);
                        summonableCube.transform.position = summonableCubePosition;
                    }
                }
            }
       }
       //if the particle system has played the cube will be spawn in that exacte locatino where the mous was
        if (!particleCube.IsAlive() && !activeObject && spawnSmoke)
        {

            summonableCube.GetComponent<MeshRenderer>().enabled = true;
            activeObject = true;
            spawnSmoke = false;
            summonableCube.GetComponent<Rigidbody>().useGravity = true;
            summonableCube.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
