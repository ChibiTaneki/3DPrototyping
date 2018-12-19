using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWall : MonoBehaviour {

    //private BoxCollider boxCollider;
    private int brickCounter;
    public bool timerStartBool;
    public float timerStart;
    public float lavaTimer;
    public float currentTimer;

    // Use this for initialization
    void Start () {
        timerStartBool = false;
        timerStart = 0;
        lavaTimer = 0;
        currentTimer = 0;
        //boxCollider = GetComponent<BoxCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timerStartBool)
            Destroy();
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.GetComponent<DestroyObjects>())
        {
          //  FindObjectOfType<AudioManager>().PlayMusic("Destruction");
            if (!timerStartBool)
            {
                timerStart = Time.time;
                timerStartBool = true;
            }
            
        }
    }

    private void Destroy()
    {
       
        currentTimer = Time.time - timerStart;
        if(currentTimer >1f && currentTimer <4f)
            gameObject.transform.GetComponent<BoxCollider>().enabled = false;
        if (currentTimer >4f)
        {
            brickCounter = gameObject.transform.childCount;
            for (int i = 0; i < brickCounter; i++)
            {
               
                gameObject.transform.GetChild(i).transform.GetComponent<BoxCollider>().enabled = true;
                gameObject.transform.GetChild(i).transform.GetComponent<BoxCollider>().isTrigger = true;
                //gameObject.transform.GetChild(i).transform.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.transform.GetChild(i).transform.GetComponent<Rigidbody>().mass = 0.1f;
                //gameObject.transform.GetChild(i).transform.GetComponent<Rigidbody>().useGravity = false;

                gameObject.transform.GetComponent<BoxCollider>().enabled = false;
                ///Clone destructible solution is not that good 

                gameObject.transform.GetChild(i).transform.GetComponent<Rigidbody>().isKinematic = false;
                // Destroy(gameObject.transform.GetChild(i), 2f);
                Destroy(gameObject, 2f);
            }
        }
    }
}
