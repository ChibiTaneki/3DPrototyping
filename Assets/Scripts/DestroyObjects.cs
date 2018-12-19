using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

    private float destructionRadius = 4f;
    private float destructionForce = 30f;
    private Vector3 forceVector;
    Collider[] colliders;
    Rigidbody currentCollider;

    //on collision the function destroyobstacle will be executed that destroys the wall by applying a force on it
    private void OnCollisionEnter(Collision other)
    {
        DestroyObstacle();
    }

    private void DestroyObstacle()
    {

        forceVector = transform.position;
        colliders = Physics.OverlapSphere(forceVector, destructionRadius);
        for(int i = 0; i< colliders.Length;i++)
        {
            currentCollider = colliders[i].GetComponent<Rigidbody>();
            if (colliders[i].GetComponent<Collider>().tag == "Destructible" && currentCollider)
            {
                currentCollider.isKinematic = false;
                FindObjectOfType<AudioManager>().PlayMusic("Destruction");
                currentCollider.AddExplosionForce(destructionForce, forceVector,destructionRadius);
            }
        }
    }
}
