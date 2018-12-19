using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public PlayerAbility playerAbility;


    public Slider wizardHealthbar;
    public Slider thiefHealthbar;
    public Slider knightHealthbar;

    public bool isInAir;
    public bool timerStartBool;
    public float timerStart;
    public float lavaTimer;
    public int currentTimer;

    public float distance;

    public LayerMask layer;
    RaycastHit hit;

    Collider colliderObject;
    MeshCollider meshCollider;
    PlayerMovement playerMovement;
    float RectWidth;
    float RectHeight;

    Vector3 contactPoint;
    Vector3 center ;

    public GameState gameState;

    Color thiefColorHealthBar;
    Color wizardColorHealthBar;
    Color knightColorHealthBar;
    private int maxHealth = 5;

    private void Start()
    {
        timerStartBool = false;
        isInAir = false;
        timerStart = 0;
        lavaTimer = 0;
        currentTimer = 0;
        playerAbility = GetComponent<PlayerAbility>();
        playerMovement = GetComponent<PlayerMovement>();
        gameState = GetComponent<GameState>();
        colliderObject = GetComponent<Collider>();

        wizardHealthbar.value = CalculateHealth(maxHealth);
        thiefHealthbar.value = CalculateHealth(maxHealth);
        knightHealthbar.value = CalculateHealth(maxHealth+3);

        thiefColorHealthBar = thiefHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        wizardColorHealthBar = wizardHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        knightColorHealthBar = knightHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color;


    }

    private void Update()
    {
        //update checks if on the characters has died, if so that character will be marked as dead and it is game over
        if (playerAbility.wizardAlive && playerAbility.wizardHealth <= 0)
        {
            playerAbility.wizardAlive = false;
            playerAbility.deathConfirmed = true;
            lavaTimer = 0;
            timerStart = 0;
            currentTimer = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            wizardColorHealthBar = wizardHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.18f);

        }
        if (playerAbility.thiefAlive && playerAbility.thiefHealth <= 0)
        {
            playerAbility.thiefAlive = false;
            playerAbility.deathConfirmed = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            thiefColorHealthBar = thiefHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0,0,0,0.18f);
        }
        if (playerAbility.knightAlive && playerAbility.knightHealth <= 0)
        {
            playerAbility.knightAlive = false;
            playerAbility.deathConfirmed = true;
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            knightColorHealthBar = knightHealthbar.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.18f);
        }
        //Switches to the game over screen and start the music
        if (playerAbility.wizardHealth <= 0 || playerAbility.knightHealth <= 0 || playerAbility.thiefHealth <= 0)
        {
            FindObjectOfType<AudioManager>().StopMusic("Theme");
            gameState.EndGame();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
       // colliderObject = other.collider;
       //// meshCollider = other.gameObject.GetComponent<meshCollider>();
       // //RectWidth = GetComponent<Collider>().bounds.size.x;
       // //RectHeight = GetComponent<Collider>().bounds.size.y;

       // contactPoint = other.contacts[0].point;
       // center = colliderObject.bounds.center;

       // if (contactPoint.y > center.y /*&&*/
       //                               /*(contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)*/)
       // {
       //     playerMovement.isGrounded = true;
       //     playerMovement.jumpCounter = 0;
       //     playerMovement.jump = false;
       // }


      //if the player hits the lava, a timer will start and you take damage over timer
        if (other.gameObject.name == "Lava")
        {
            if (!timerStartBool)
             {
                 timerStart = Time.time;
                 timerStartBool = true;
             }
             LavaDamage();
         }
        //goes over to the win screen when the victory has been reached
        if (other.gameObject.tag == "Victory")
        {
            // Victory Screen
            FindObjectOfType<AudioManager>().StopMusic("Theme");
            gameState.WinGame();
        }
    }

    //if the player stays in lava he will continue to take damage
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Lava")
        {
            lavaTimer = Time.time - timerStart;
            
            if ((int)lavaTimer % 1 == 0 && lavaTimer != 0 && currentTimer != (int)lavaTimer)
            {
                currentTimer = (int)lavaTimer;
                LavaDamage();
            }
        }
    }

    // stops lavacounter 
    private void OnCollisionExit(Collision other)
    {
        meshCollider = other.gameObject.GetComponent<MeshCollider>();
        if (other.gameObject.tag == "Lava")
        {
            timerStartBool = false;
            lavaTimer = 0;
            timerStart = 0;
            currentTimer = 0;
        }
        else if (other.gameObject.tag == "Item" && meshCollider.isTrigger)
            other.gameObject.GetComponent<MeshCollider>().isTrigger = false;

    }


    //if the player collides with an item , his health will be restored to max
    private void OnTriggerEnter(Collider other)
    {
        meshCollider = other.gameObject.GetComponent<MeshCollider>();
        if (other.gameObject.tag == "Item")
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            if (playerAbility.playerState == 1)
                if (playerAbility.wizardHealth < maxHealth)
                {
                    playerAbility.wizardHealth = maxHealth;
                    wizardHealthbar.value = CalculateHealth(playerAbility.wizardHealth);
                    gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
                else
                    meshCollider.isTrigger = true;
            else if (playerAbility.playerState == 2)
                if (playerAbility.thiefHealth < maxHealth)
                {
                    playerAbility.thiefHealth = maxHealth;
                    thiefHealthbar.value = CalculateHealth(playerAbility.thiefHealth);
                    gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
                else
                    meshCollider.isTrigger = true;
            else if (playerAbility.playerState == 3)
                if (playerAbility.knightHealth < maxHealth+3)
                {
                    playerAbility.knightHealth = maxHealth+3;
                    knightHealthbar.value = CalculateHealth(playerAbility.knightHealth);
                    gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
                else
                    meshCollider.isTrigger = true;
        }
      
        //player thats damage only once when he has fallen into the spikes
        if (other.gameObject.tag == "Trap")
        {
                TakeDamage();
        }
    }

    //Calculates Player currenthealth 
    float CalculateHealth(float currentHealth)
    {
        if(playerAbility.playerState == 1)
            return currentHealth / maxHealth;
        if (playerAbility.playerState == 2)
            return currentHealth / maxHealth;
        if (playerAbility.playerState == 3)
            return currentHealth / (maxHealth+3);
        else
            return currentHealth;
    }

    //checks what player is active and removes one life from it
    public void TakeDamage()
    {
        if (playerAbility.playerState == 1)
        {
            playerAbility.wizardHealth -= 1;
            wizardHealthbar.value = CalculateHealth(playerAbility.wizardHealth);
        }
        else if (playerAbility.playerState == 2)
        {
            playerAbility.thiefHealth -= 1;
            thiefHealthbar.value = CalculateHealth(playerAbility.thiefHealth);
        }
        else if (playerAbility.playerState == 3)
        {
            playerAbility.knightHealth -= 1;
            knightHealthbar.value = CalculateHealth(playerAbility.knightHealth);
        }
    }

    // //checks what player is active and removes one life from it
    public void LavaDamage()
    {
        if (playerAbility.playerState == 1)
        {
            playerAbility.wizardHealth -= 1;
            wizardHealthbar.value = CalculateHealth(playerAbility.wizardHealth);
        }
        else if (playerAbility.playerState == 2)
        {
            playerAbility.thiefHealth -= 1;
            thiefHealthbar.value = CalculateHealth(playerAbility.thiefHealth);
        }
        else if (playerAbility.playerState == 3)
        {
            playerAbility.knightHealth -= 1;
            knightHealthbar.value = CalculateHealth(playerAbility.knightHealth);
        }
    }

    //checks with 3 raycast if the player is really on the floor or if he is in air
    private void FixedUpdate()
    {
        Vector3 front = new Vector3(transform.position.x + 0.85f, transform.position.y, transform.position.z);
        Vector3 back = new Vector3(transform.position.x - 0.85f, transform.position.y, transform.position.z);
        if(Physics.Raycast(transform.position, Vector3.down,out hit, distance) || Physics.Raycast(front, Vector3.down, out hit, distance) || Physics.Raycast(back, Vector3.down, out hit, distance))
        {
            playerMovement.isGrounded = true;
           // isInAir = false;
            playerMovement.jumpCounter = 0;
        }
        //else
        //{
        //    isInAir = true;
        //}
    }
}

