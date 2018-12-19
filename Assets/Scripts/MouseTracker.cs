using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour {

    private Vector3 mousePosition;
    private Vector3 mouseScreenToWorld;
    Vector3 position;
    public Camera mainCamera;
       
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition.z = 18f;

        mouseScreenToWorld = mainCamera.ScreenToWorldPoint(mousePosition);
        position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-8 * Time.deltaTime));
        transform.position = new Vector3(position.x,position.y,0);
    }
}
