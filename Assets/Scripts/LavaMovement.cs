using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour {

    private Rigidbody lavaMovement;
    private Vector3 lavaStartingPosition;
    private Vector3 lavaCurrentPosition;

    private bool movingUp;
    private bool movingDown;
    private float heightMultiplier;
    private float changeCondition;

    private Vector3 currentPosition;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        lavaMovement = GetComponent<Rigidbody>();
        lavaStartingPosition = gameObject.transform.position;
        lavaCurrentPosition = gameObject.transform.position;
        movingUp = true;
        movingDown = false;
        heightMultiplier = 0.5f;
        changeCondition = 0.05f;
        targetPosition = lavaStartingPosition + Vector3.up * heightMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(lavaCurrentPosition.y >= lavaStartingPosition.y && movingUp)
        {
            gameObject.transform.position = Vector3.Lerp(lavaCurrentPosition, lavaStartingPosition + Vector3.up * heightMultiplier, 0.01f);
            lavaCurrentPosition = gameObject.transform.position;
            currentPosition = lavaCurrentPosition + new Vector3(0, changeCondition, 0);
            if (currentPosition.y >= targetPosition.y)
            {
                movingUp = false;
                movingDown = true;
                lavaCurrentPosition = lavaStartingPosition + Vector3.up * heightMultiplier;
            }
        }
        else if(lavaCurrentPosition.y <= lavaStartingPosition.y + heightMultiplier && movingDown)
        {
            gameObject.transform.position = Vector3.Lerp(lavaCurrentPosition, lavaStartingPosition, 0.01f);
            lavaCurrentPosition = gameObject.transform.position;
            currentPosition = lavaCurrentPosition - new Vector3(0, changeCondition, 0);
            if (currentPosition.y <= lavaStartingPosition.y)
            {
                movingUp = true;
                movingDown = false;
                lavaCurrentPosition = lavaStartingPosition;
            }
        }
    }
}
