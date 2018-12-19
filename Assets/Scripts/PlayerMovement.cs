using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public KeyCode keyInput;


    private Rigidbody playerBody;
    public PlayerAbility playerAbility;
    public bool facingRight = true;
    public bool facingLeft = false;
    public bool pressedJumped;
    private Vector3 directionVector;
    private float angle;
    private float magnitudeVector;

    private KeyCode button;

    private KnightFunctions knightFunctions;

    [Range(1, 100)]
    public float jumpVelocity = 13f;

    public float speed = 10f;
    public bool isGrounded;
    public int jumpCounter;
    private float moveInput;
    public float fallMultiplier = 1.5f;
    public float lowJumpMultiplier = 2.5f;

    public bool isDragging;

	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody>();
        playerAbility = GetComponent<PlayerAbility>();
        knightFunctions = GetComponent<KnightFunctions>();
        jumpCounter = 0;
        pressedJumped = false;
        isDragging = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //Player Movement checks what input has been pressed, that will be used in the fixedupdate to apply the playermovement physics
        if (!playerAbility.deathConfirmed)
        {
            if (!isDragging)
                moveInput = Input.GetAxisRaw("Horizontal");
            else
                moveInput = 0;
            if (moveInput < 0)
                keyInput = KeyCode.A;
            else if (moveInput > 0)
                keyInput = KeyCode.D;
            if (!GetComponent<GrapplingHook>().hookOnObject)
            {
                if (Input.GetButtonDown("Jump") && isGrounded && jumpCounter == 0 && keyInput != KeyCode.Space && !isDragging)
                {
                    keyInput = KeyCode.Space;
                   
                }
                if (playerBody.velocity.y > 0 && !Input.GetButton("Jump"))
                {
                    pressedJumped = true;
                }
                else
                {
                    pressedJumped = false;
                }
            }
        }
    }

    // update used for physics
    void FixedUpdate()
    {
        //Player Movement 
        if (!playerAbility.deathConfirmed)
        {
            if (!GetComponent<GrapplingHook>().hookOnObject)
            {
                // if not hooked the player moves to left and right while rotating to the correct direction
                playerBody.velocity = new Vector3(moveInput * speed, playerBody.velocity.y);
                if (moveInput > 0 && !facingRight)
                {
                    playerBody.transform.Rotate(new Vector3(0, 1, 0), 180, Space.Self);
                    facingRight = true;
                    facingLeft = false;
                }
                if (moveInput < 0 && !facingLeft)
                {
                    playerBody.transform.Rotate(new Vector3(0, 1, 0), 180, Space.Self);
                    facingRight = false;
                    facingLeft = true;
                }
                //if the player is on ground player will jump by apply an impulse
                if (keyInput == KeyCode.Space && isGrounded && jumpCounter == 0)
                {
                    isGrounded = false;
                    jumpCounter++;
                    playerBody.velocity = new Vector3(playerBody.velocity.x, 0, 0);
                    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                    keyInput = KeyCode.Keypad0;
                }
                //faking a more realistic fall by applying a velocity on the player
                if (playerBody.velocity.y > 0 || playerBody.velocity.y < 0)
                {
                    playerBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                else if (playerBody.velocity.y > 0)
                {
                    playerBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                }


            }
        }
    }
}
