using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRope : MonoBehaviour {

    //Destroy an object that has the tag rope on trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rope")
        {
            Destroy(other.gameObject);
        }
    }

    //Destroy an object that has the tag rope on collision
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.gameObject.tag == "Rope")
        {
            Destroy(other.gameObject);
        }
    }
}
