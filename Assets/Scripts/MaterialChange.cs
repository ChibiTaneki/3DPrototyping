using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour {

    public Material[] material;
    private Renderer rend;
    private Light light;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        light = GetComponent<Light>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        rend.sharedMaterial = material[0];
        light.color = Color.green;
    }
    private void OnTriggerExit(Collider other)
    {
        rend.sharedMaterial = material[1];
        light.color = Color.red;
    }
}
