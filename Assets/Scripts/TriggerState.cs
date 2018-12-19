using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerState : MonoBehaviour {

    public bool pressed = false;
    public Rigidbody trigger;
    public Vector3 triggerInitialPosition;
    public Vector3 triggerPressedPosition;
    public float triggerInitialPositionY;
    public float triggerInitialPositionZ;
    public Transform triggerPosition;

	// Use this for initialization
	void Start () {
        triggerInitialPosition= new Vector3(trigger.position.x, trigger.position.y-0.3f, trigger.position.z);
        triggerPressedPosition = new Vector3(trigger.position.x, trigger.position.y, trigger.position.z);
    }
	
	// Update is called once per frame
	void Update () {

        if (pressed)
            transform.position = triggerInitialPosition;
        else
            transform.position = triggerPressedPosition;



    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            pressed = true;
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
            pressed = false;
    }
}
