using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Function that checks the collision of the Aroow object with other objects
public class Arrow : MonoBehaviour
{

    public GameObject playerObject;

    //Function checks which colliders the arrow collides with
    private void OnTriggerEnter(Collider other)
    {
        //if the collider doesnt have the tag Player or Projectile, the component Bow will set the bool hitOnObject to true
        if (other.tag != "Player" && other.tag !="Projectile")
        {
            playerObject.GetComponent<Bow>().hitOnObject = true;
            //if the collider has the tag Rope, the gameobject with that collider will be destroyed
            if (other.tag == "Rope")
            {
                Destroy(other.gameObject);
            }
        }
       
    }



}
