using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookChecker : MonoBehaviour {

    public GameObject playerObject;
    private ConfigurableJoint configurableGrapplingHook;
    private GrapplingHook grapple;
    public SoftJointLimit jointLimit;

    //private Transform playerRigibody;


    // Use this for initialization
    void Start () {
        // playerRigibody = playerObject.GetComponent<Transform>();
        grapple = playerObject.GetComponent<GrapplingHook>();
        jointLimit = new SoftJointLimit();

    }
	
	// Update is called once per frame
	void Update () {
        //if(!playerObject.GetComponent<GrapplingHook>().fired)
        //transform.position = playerRigibody.position;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Hookable")
        {
           // FindObjectOfType<AudioManager>().PlayMusic("GrapplingHook");
            grapple.hookOnObject = true;
            grapple.hookedSpot = grapple.hookParent.transform.position;
            grapple.hookedObject = other.gameObject;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            configurableGrapplingHook = gameObject.AddComponent<ConfigurableJoint>();
            configurableGrapplingHook.connectedBody = playerObject.GetComponent<Rigidbody>();
            configurableGrapplingHook.autoConfigureConnectedAnchor = false;
            configurableGrapplingHook.connectedAnchor = new Vector3(0.5f,-0.83f,0);
            configurableGrapplingHook.xMotion = ConfigurableJointMotion.Limited;
            configurableGrapplingHook.yMotion = ConfigurableJointMotion.Limited;
            configurableGrapplingHook.zMotion = ConfigurableJointMotion.Locked;
            configurableGrapplingHook.angularXMotion = ConfigurableJointMotion.Limited;
            configurableGrapplingHook.angularYMotion = ConfigurableJointMotion.Limited;
            configurableGrapplingHook.angularZMotion = ConfigurableJointMotion.Locked;
            float distanceToHook = Vector3.Distance(transform.position, playerObject.transform.position);
            jointLimit.limit = Vector3.Distance(transform.position, playerObject.transform.position);
            configurableGrapplingHook.linearLimit = jointLimit;



        }
        else if(other.tag !="Hookable" && other.tag != "Player")
        {
            grapple.ResetHookShot();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Hookable")
    //    {
    //        hingeGrapplingHook = gameObject.AddComponent<HingeJoint>();
    //        hingeGrapplingHook.connectedBody = collision.rigidbody;
    //    }
    //}
}
