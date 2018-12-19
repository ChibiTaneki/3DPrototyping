using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    [SerializeField]
    GameObject door;

    public bool isOpened;
    public bool wasOpened;
    public bool permanentOpen;
    public bool playedMusicOpen;
    public bool playedMusicClosed;
    public Vector3 originalPosition;
    

	// Use this for initialization
	void Start () {
        isOpened = false;
        permanentOpen = false;
        playedMusicOpen = false;
        playedMusicClosed = true;
        originalPosition = door.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //moves the book away and plays a sound
		if(isOpened)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, 5), 0.4f);
            if (!playedMusicOpen)
            {
                FindObjectOfType<AudioManager>().PlayMusic("ButtonOn");
                playedMusicOpen = true;
                playedMusicClosed = false;
            }
        }
        //reset book and plays a different sound
        else if(!isOpened)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, originalPosition, 0.4f);
            if (!playedMusicClosed)
            {
                FindObjectOfType<AudioManager>().PlayMusic("ButtonOff");
                playedMusicClosed = true;
                playedMusicOpen = false;
            }
        }
	}

    //set the book on the open state
    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened)
        {
            isOpened = true;
        }  
    }

    // set the boon on the close state
    private void OnTriggerExit(Collider other)
    {
        if (door.tag != "PermanentOpen" && !permanentOpen)
        {
            isOpened = false;
        }
    }
}
