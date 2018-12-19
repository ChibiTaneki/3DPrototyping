using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrink : MonoBehaviour {

    public CapsuleCollider player;
	// Use this for initialization
	void Start () {
        player = gameObject.GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("u"))
        {
            player.height = 2.5f;
           player.center = new Vector3(0, -0.7f, 0);
        }
        if(Input.GetKeyDown("i"))
        {
            player.height = 5f;
            player.center = new Vector3(0, 0.1f, 0);
        }
	}
}
