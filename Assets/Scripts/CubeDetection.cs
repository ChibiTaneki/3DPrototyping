using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDetection : MonoBehaviour {


    public GameObject playerObject;
    public PlayerMovement playerMovement;
	// Use this for initialization
	void Start () {
        playerMovement = playerObject.GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube" && playerMovement.isDragging)
        {
          playerObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            playerObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cube" && playerMovement.isDragging)
        {
            playerObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            playerObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
