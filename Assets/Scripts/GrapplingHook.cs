using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public GameObject hookParent;
    private GameObject hookChild;
    public GameObject hookedObject;
    public GameObject playerObject;
    public PlayerMovement playerMovement;
    public Camera mainCamera;
   

    private float hookSpeed;
    private float maxShotDistance;
    private float currentShotDistance;
    [Range(0.001f,10)]
    public float speed;
    public float distance;
    public Vector3 directionVector;
    public Vector3 startVector;
    public Vector3 hookedSpot;
    public Vector3 normalizedVector;

    private Vector3 mousePosition;
    private Ray mousePositionRay;
    private Plane projectionPlane;
    LineRenderer rope;

    public bool fired { get; set; }
    public bool hookOnObject;
    public bool grounded;

    private float angle;
    private SoftJointLimit jointLimitDistance;

    // Use this for initialization
    void Start ()
    {
        maxShotDistance = 22;
        speed = 30f;
        hookChild = hookParent.transform.GetChild(0).gameObject;
        startVector = hookParent.transform.position;

        playerObject = gameObject;
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        rope = hookChild.GetComponent<LineRenderer>();
        jointLimitDistance = hookChild.GetComponent<HookChecker>().jointLimit;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //when shot the line renderer will simulate the line between hook and player
        if(fired)
        {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, hookChild.transform.position);
        }
        //if nothing is hit , the hook will reset if he is over is maxreach  else it updates the hook travelling position
        if(fired && !hookOnObject)
        {

            hookParent.transform.Translate(directionVector* speed * Time.deltaTime);
            currentShotDistance = Vector3.Distance(startVector, hookChild.transform.position);

            if (currentShotDistance > maxShotDistance)
                ResetHookShot();
        }
        //when hooked the player can swing around, pull up or down or jump from the hook
        else if(hookOnObject)
        {

            if (Input.GetButtonDown("Jump"))
                ResetHookShot();
            else
            {
                // pull up and down works by changing the linear limit of the joint
                // the swing works by applying a force on the character
                float moveInput = Input.GetAxisRaw("Vertical");
                float forceInput = Input.GetAxisRaw("Horizontal");
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(forceInput * 4, 0, 0), ForceMode.Force);
                if (moveInput > 0 && hookChild.GetComponent<ConfigurableJoint>().linearLimit.limit > 3)
                {
                    hookChild.GetComponent<HookChecker>().jointLimit.limit -= 0.2f;
                    hookChild.GetComponent<ConfigurableJoint>().linearLimit = hookChild.GetComponent<HookChecker>().jointLimit;
                }
                else if (moveInput < 0 && hookChild.GetComponent<ConfigurableJoint>().linearLimit.limit < 22)
                {
                    hookChild.GetComponent<HookChecker>().jointLimit.limit += 0.2f;
                    hookChild.GetComponent<ConfigurableJoint>().linearLimit = hookChild.GetComponent<HookChecker>().jointLimit;
                }
            }
        }
        else
        {
            hookChild.transform.position = new Vector3(transform.position.x+10,transform.position.y,-100);
            Destroy(hookChild.GetComponent<ConfigurableJoint>());
        }
	}

    //Reset the hook position and destroy the joint
    public void ResetHookShot()
    {
        hookChild.transform.position = new Vector3(transform.position.x + 10, transform.position.y, -100);
        Destroy(hookChild.GetComponent<ConfigurableJoint>());
        currentShotDistance = 0;
        fired = false;
        hookOnObject = false;
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, transform.position);
        hookChild.transform.eulerAngles = new Vector3(0, 0, 0);

    }

    //checks the mousposition and shots the hook in that direction
    public void GrapplingShoot()
    {
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            fired = true;
            mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            projectionPlane = new Plane(Vector3.forward, transform.position);
            distance = 0;
            if (projectionPlane.Raycast(mousePositionRay, out distance))
            {
                mousePosition = mousePositionRay.GetPoint(distance);
                startVector = playerObject.transform.position;
                directionVector = (mousePosition - startVector).normalized;
                hookChild.transform.position = new Vector3(startVector.x +directionVector.x * 1.5f, startVector.y +directionVector.y * 1.5f, 0);
                Debug.Log(mousePosition);
                Debug.Log(transform.position);
            }
            angle = Vector3.Angle(Vector3.right, directionVector);
            hookChild.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }


    //not used
    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f; 
        Vector3 dir = new Vector3(0, -1);

        if(Physics.Raycast(transform.position,dir,out hit ,distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
