using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public bool fired;
    public bool hitOnObject;

    [Range(1f, 50)]
    public float speed;
    private float currentShotDistance;

    private Vector3 mousePosition;

    public Vector3 directionVector;
    public Vector3 startVector;
    public Vector3 hookedSpot;

    private Ray mousePositionRay;
    private Plane projectionPlane;
    public Camera mainCamera;
    public GameObject arrow;

    public bool timerStartBool;
    public float timerStart =0;
    public float respawnTimer=0;

    // Use this for initialization
    void Start ()
    {
        hitOnObject = false;
        fired = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fired && !hitOnObject && respawnTimer <4)
        {
            arrow.transform.Translate(directionVector * speed * Time.deltaTime);
            respawnTimer = Time.time - timerStart;
        }
        else
        {
            arrow.transform.position = new Vector3(transform.position.x-10,transform.position.y,-100);
            fired = false;
            hitOnObject = false;
            respawnTimer = 0;
        }
    }

    public void BowShot()
    {
        if (Input.GetMouseButtonDown(1) && !fired)
        {
            fired = true;
            timerStart = Time.time;
            mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            projectionPlane = new Plane(Vector3.forward, transform.position);
            float distance = 0;
            if (projectionPlane.Raycast(mousePositionRay, out distance))
            {
                //mousePosition = mousePositionRay.GetPoint(distance);
                //arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0);
                //startVector = arrow.transform.position;
                //directionVector = new Vector3(mousePosition.x - startVector.x, mousePosition.y - startVector.y,0);
                //Debug.Log(mousePosition);
                //Debug.Log(transform.position);

                mousePosition = mousePositionRay.GetPoint(distance);
                startVector = gameObject.transform.position;
                directionVector = (mousePosition - startVector).normalized;
               
                arrow.transform.position = new Vector3(startVector.x +directionVector.x*1.5f, startVector.y +directionVector.y*1.5f, 0);
            
                Debug.Log(mousePosition);
                Debug.Log(transform.position);
            }
        }
    }
}
