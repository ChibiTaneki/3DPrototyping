using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Source https://www.youtube.com/watch?v=HVCsg_62xYw Tutorial for animation 
public class CharacterAnimation : MonoBehaviour {


    private Animator playerAnimation;
    private PlayerAbility playerAbility;
    public GameObject playerObject;
   
	// Use this for initialization
	void Start () {
        playerAnimation = GetComponent<Animator>();
        playerAbility = gameObject.transform.parent.parent.GetComponent<PlayerAbility>();
	}
	
	// Update is called once per frame
	void Update () {
      
        //Each character has its own animation
        //While you switch the character you have to switch the animation area can should be executed
        if (playerAbility.playerState == 1 )
            playerAnimation.SetInteger("playerState", 1);
        if (playerAbility.playerState == 2)
            playerAnimation.SetInteger("playerState", 2);
        if (playerAbility.playerState == 3)
            playerAnimation.SetInteger("playerState", 3);

        //If the Input is either A or D the character will play the run animation
        if ((Input.GetKey(KeyCode.A) && !playerObject.GetComponent<PlayerMovement>().isDragging || Input.GetKey(KeyCode.D) && !playerObject.GetComponent<PlayerMovement>().isDragging) && ((playerAbility.playerState == 1 && playerAbility.wizardAlive) || (playerAbility.playerState == 2 && playerAbility.thiefAlive) || (playerAbility.playerState == 3 && playerAbility.knightAlive)))
            playerAnimation.SetBool("isMoving", true);
        else
        {
            playerAnimation.SetBool("isMoving", false);
        }

        //If the Input is either Space the character will play the jump animation but in this version there is no jump animation
        ////if (Input.GetKey(KeyCode.Space))
        ////{
        ////    playerAnimation.SetTrigger("jump");
        ////}
    }
}
