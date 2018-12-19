using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour {


    public Image helpButton;
    public Image helpScreen;
    private bool activeIcon;
	// Use this for initialization
	void Start () {
        activeIcon = false;
    }
	
	// Update is called once per frame
    //Turn off or on the help screen when pressing on the question icon on screen
	void Update () {
		if(helpButton.gameObject.transform.position.x - helpButton.rectTransform.rect.width/2 <= Input.mousePosition.x && helpButton.gameObject.transform.position.x + helpButton.rectTransform.rect.width / 2 >= Input.mousePosition.x && helpButton.gameObject.transform.position.y - helpButton.rectTransform.rect.height / 2 <= Input.mousePosition.y && helpButton.gameObject.transform.position.y + helpButton.rectTransform.rect.height / 2 >= Input.mousePosition.y && Input.GetMouseButtonDown(0) && !activeIcon)
        {
            activeIcon = true;
            helpScreen.gameObject.SetActive(true);

        }
        else if (helpButton.gameObject.transform.position.x - helpButton.rectTransform.rect.width / 2 <= Input.mousePosition.x && helpButton.gameObject.transform.position.x + helpButton.rectTransform.rect.width / 2 >= Input.mousePosition.x && helpButton.gameObject.transform.position.y - helpButton.rectTransform.rect.height / 2 <= Input.mousePosition.y && helpButton.gameObject.transform.position.y + helpButton.rectTransform.rect.height / 2 >= Input.mousePosition.y && Input.GetMouseButtonDown(0) && activeIcon)
        {
            activeIcon = false;
            helpScreen.gameObject.SetActive(false);
        }

    }
}
