using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFunctions : MonoBehaviour {

    public bool isCarrying;

    private Vector3 mousePosition;

    public Vector3 directionVector;
    public Vector3 startVector;
    public float force;
    public float carryState;
    private PlayerAbility playerAbility;

    public GameObject playerObject;
    public GameObject shieldObject;
    public GameObject cubeObject;
    public Rigidbody cubeRigibody;
    private PlayerMovement playerMovement;
    private float distanceMultiplier;

    private Ray mousePositionRay;
    private Plane projectionPlane;
    public Camera mainCamera;

    private float angle;

    // Use this for initialization
    void Start()
    {
        playerObject = gameObject;
        playerAbility = playerObject.GetComponent<PlayerAbility>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        isCarrying = false;
        force = 150;
        distanceMultiplier = 1.5f;

    }


    // Update is called once per frame
    void Update()
    {
        //checks if the knight is carriny stuff, if so he can throw the object
        //if it is not the knight every knight related abilites will be reset 
        //else he can use his shield to block by pressing right click and leftclick to throw stuff
        if (playerAbility.playerState == 3 && isCarrying)
        {

            SetCubePosition();
            ResetShieldState();
            if (Input.GetMouseButtonDown(0) && carryState == 1)
            {
                ResetCarryState();
                cubeRigibody.AddForce(directionVector * force, ForceMode.Impulse);
               
            }
            if (Input.GetMouseButtonUp(0) && carryState == 0 && isCarrying)
            {
                carryState = 1;
            }

        }
        
        else if(playerAbility.playerState !=3)
        {
            ResetCarryState();
            ResetShieldState();
        }
        else if (Input.GetMouseButton(1) && playerAbility.playerState == 3 && !isCarrying && playerAbility.knightAlive)
        {
            SetShieldPosition();
        }
        else if (Input.GetMouseButtonUp(1) && playerAbility.playerState == 3)
        {
            shieldObject.transform.position = new Vector3(shieldObject.transform.position.x, shieldObject.transform.position.y, -100);
        }
         else if(!Input.GetMouseButton(1) && playerAbility.playerState == 3 && !playerAbility.knightAlive)
        {
            ResetShieldState();
            shieldObject.SetActive(false);
        }
        else if (!Input.GetMouseButton(1) && playerAbility.playerState == 3)
        {
            ResetShieldState();
        }
    }


    //if he collides with a cube he may pick up that object
    private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetMouseButtonDown(0) && !isCarrying && collision.gameObject.tag == "Cube" && playerAbility.playerState == 3)
        {
            cubeObject = collision.gameObject;
            cubeRigibody = cubeObject.GetComponent<Rigidbody>();
            cubeRigibody.isKinematic = true;
            cubeRigibody.useGravity = false;
            isCarrying = true;
            SetCubePosition();

        }
    }

    //if he collides with a cube he may pick up that object
    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetMouseButtonDown(0) && !isCarrying && collision.gameObject.tag == "Cube" && playerAbility.playerState == 3)
        {
            cubeObject = collision.gameObject;
            cubeRigibody = cubeObject.GetComponent<Rigidbody>();
            cubeRigibody.isKinematic = true;
            cubeRigibody.useGravity = false;
            isCarrying = true;
            SetCubePosition();

        }
    }

    //When activating the shield, the shield be positioned at the direction where the mouse is pointing to, while rotation around the body in the right angle
    void SetShieldPosition()
    {
        mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        shieldObject.transform.GetComponent<BoxCollider>().enabled = true;
        projectionPlane = new Plane(Vector3.forward, transform.position);
        float distance = 0;
        if (projectionPlane.Raycast(mousePositionRay, out distance))
        {
            mousePosition = mousePositionRay.GetPoint(distance);
            startVector = playerObject.transform.position;
            directionVector = (mousePosition - startVector).normalized;

            shieldObject.transform.position = Vector3.Lerp(startVector,directionVector * distanceMultiplier + startVector,9f);
            angle = Vector3.Angle(Vector3.right, directionVector);
            if (directionVector.x < 0 && directionVector.y < 0)
            {
                angle = 180;
                shieldObject.transform.position = Vector3.left * distanceMultiplier + startVector;
            }
            if (directionVector.x > 0 && directionVector.y < 0)
            {
                angle = 0;
                shieldObject.transform.position = Vector3.right * distanceMultiplier + startVector;
            }
            if (angle > 90 && playerMovement.facingRight)
            {
                playerObject.transform.Rotate(Vector3.up, 180, Space.Self);
                playerMovement.facingLeft = true;
                playerMovement.facingRight = false;
            }
            else if (angle < 90 && playerMovement.facingLeft)
            {
                playerObject.transform.Rotate(Vector3.up, 180, Space.Self);
                playerMovement.facingLeft = false;
                playerMovement.facingRight = true;
            }

            shieldObject.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    //same as before rotate the cube around the player in relation to the mouse
    void SetCubePosition()
    {
        mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        projectionPlane = new Plane(Vector3.forward, transform.position);
        float distance = 0;
        if (projectionPlane.Raycast(mousePositionRay, out distance))
        {
            mousePosition = mousePositionRay.GetPoint(distance);
            startVector = playerObject.transform.position;
            directionVector = (mousePosition - startVector).normalized;

            cubeObject.transform.position = directionVector * 2 + startVector;
            angle = Vector3.Angle(Vector3.right, directionVector);
            if (directionVector.x < 0 && directionVector.y < 0)
            {
                angle = 180;
                cubeObject.transform.position = Vector3.left * 2 + startVector;
            }
            if (directionVector.x > 0 && directionVector.y < 0)
            {
                angle = 0;
                cubeObject.transform.position = Vector3.right * 2 + startVector;
            }
            if (angle > 90 && playerMovement.facingRight)
            {
                playerObject.transform.Rotate(Vector3.up, 180, Space.Self);
                playerMovement.facingLeft = true;
                playerMovement.facingRight = false;
            }
            else if (angle < 90 && playerMovement.facingLeft)
            {
                playerObject.transform.Rotate(Vector3.up, 180, Space.Self);
                playerMovement.facingLeft = false;
                playerMovement.facingRight = true;
            }

            cubeObject.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    //Reset carrying object 
    public void ResetCarryState()
    {
        isCarrying = false;
        if (cubeRigibody != null)
        {
            cubeRigibody.isKinematic = false;
            cubeRigibody.useGravity = true;
        }
        carryState = 0;
    }
    //reset shield to default state
    public void ResetShieldState()
    {
        if (playerAbility.playerState == 3 && playerMovement.facingRight)
        {
            shieldObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 0);
            shieldObject.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if(playerAbility.playerState == 3 && playerMovement.facingLeft)
        {
            shieldObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -2);
            shieldObject.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
            shieldObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -100);
      //  shieldObject.transform.eulerAngles = new Vector3(0, -90, 0);
        shieldObject.transform.GetComponent<BoxCollider>().enabled = false;
    }
}
