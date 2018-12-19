using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour {

    public Camera mainCamera;

    public Vector3 gameObjectSreenPoint;
    public Vector3 mousePreviousLocation;
    public Vector3 mouseCurLocation;

    public Vector3 force;
    public float topSpeed = 10;
    public bool isDragging = false;

  
    // Update is called once per frame
    void Update()
    {
        //This prevents your thrown object from ascending to infinity and beyond. Disable if you're trying to throw Buzz Lightyear.
        if (force.y > 0.0f)
        {
            force.y -= 0.1f;
        }
    }
 
    void OnMouseDown()
    {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = mainCamera.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
    }


    void OnMouseDrag()
    {
        mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        force = (mouseCurLocation - mousePreviousLocation);//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;
        if (!isDragging)
            isDragging = true;

    }

    void OnMouseUp()
    {
        //Makes sure there isn't a ludicrous speed
        if(isDragging)
            isDragging = false;
        if (GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
            GetComponent<Rigidbody>().AddForce(force/2, ForceMode.Impulse);
           // force = GetComponent<Rigidbody>().velocity.normalized * topSpeed;
    }

    public void FixedUpdate()
    {
        if(isDragging)
        GetComponent<Rigidbody>().velocity = force;
    }
}
