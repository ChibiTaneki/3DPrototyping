using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    private GameObject playerObject;
    public float speed = 700f;
    private Rigidbody missileObject;

    public void SetPlayer(GameObject _playerObject)
    {
        playerObject = _playerObject;
    }
	// Use this for initialization
	void Start () {
        missileObject = gameObject.transform.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        missileObject.velocity = speed * transform.right * Time.deltaTime;
	}


    //private void OnCollisionEnter(Collision other)
    //{
    //    if (playerObject != null)
    //    {
    //        if (other.gameObject.tag == "Player")
    //            playerObject.GetComponent<PlayerCollision>().TakeDamage();
    //    }
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (playerObject != null)
        {
            if (other.gameObject.tag == "Player")
                playerObject.GetComponent<PlayerCollision>().TakeDamage();

            if (other.gameObject.tag == "Block")
                FindObjectOfType<AudioManager>().PlayMusic("Block");
        }
        Destroy(gameObject);
    }
}
