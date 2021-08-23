using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBall : MonoBehaviour
{
    private float velocity = 3;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float newY;

    public Gamemanager GM;
    private int oldRandom =0;

    private bool isDead;
    private int starCount = 0;
    public TextMeshProUGUI starCountText;
    public TextMeshProUGUI tapText; //Tap to start text

    private bool firstClick = false;
    private bool insideObstacle = false;

    public ParticleSystem DeathPS;
    public ParticleSystem StarPS;
    enum Colors
    {
        RED,
        GREEN,
        BLUE,
        VIOLET
    }
    Colors playerColor;

    // Start is called before the first frame update
    void Start()
    {
        starCountText.text = starCount.ToString();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY; //Ball y axis is freezed at the start
        ChangeBallColor(); //Sets the color of ball at start
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is alive, input is avaible
        if (!isDead)
        {
            //Ball is not moving without first tap
            if (Input.GetMouseButtonDown(0))
                firstClick = true;

            if (firstClick)
            {
                rb.constraints = RigidbodyConstraints2D.None; //When player taps the screen, ball y axis is released
                tapText.gameObject.SetActive(false); //Hide tap text when first tap has done

                //Player can tap the left mouse button on everywhere on the game screen
                if (Input.GetMouseButtonDown(0))
                {
                    SoundManager.PlaySound("plop"); //Plop sound plays everytime when player click on screen
                    rb.velocity = Vector2.up * velocity; //Balls rigidbody gets up force multiplied with velocity variable
                }

                //if ball gets any higher than before, main camera follows it
                if (gameObject.transform.position.y >= newY)
                {
                    Camera.main.transform.position = new Vector3(0, transform.position.y, -10);
                    newY = transform.position.y;
                }

                //If ball drops under the game screen, its game over
                screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
                if (transform.position.y < screenBounds.y)
                {
                    SoundManager.PlaySound("defeat"); //Play dead sound
                    StartCoroutine(die()); //Starts the timer before game pauses and game over screen shows up
                }
            }
        }
    }
    void ChangeBallColor()
    {
        //Take a random value and set the color according to that value
        int random = Random.Range(0, 3);
        Color Red = new Color(1, 0.24f, 0.22f, 1);
        Color Green = new Color(0.16f, 0.87f, 0, 1);
        Color Blue = new Color(0, 0.39f, 1, 1);
        Color Violet = new Color(0.72f, 0, 0.91f, 1);
        Color[] RandomColor = { Red, Green, Blue, Violet };

        //If the random variable is same as before, it increase by one so it wont be same color
        if(oldRandom == random)
        {
            if (random < 4)
                random += 1;
            //If random variable is 3 it turns to its value to 0
            else random = 0;
        }
        oldRandom = random; //Sets oldRandom to same as random value

        //sets the ball color to given random color
        gameObject.GetComponentInChildren<SpriteRenderer>().color = RandomColor[random];

        //Sets also the enum according to random value 
        if (random == 0) playerColor = Colors.RED;
        else if (random == 1) playerColor = Colors.GREEN;
        else if (random == 2) playerColor = Colors.BLUE;
        else if (random == 3) playerColor = Colors.VIOLET;
    }

    //Lets check if the ball hits something
    void OnTriggerEnter2D(Collider2D other)
    {
        //If the ball hits the color switch
        if (other.gameObject.CompareTag("ColorSwitch"))
        {
            ChangeBallColor(); //Change the ball color
    
            GM.SpawnObstacle(); //Spawn new obstacle and color switch

            SoundManager.PlaySound("bling002"); //Play bling sound everytime when player gets color switcher

            Destroy(other.gameObject); //Destorys the picked colorswicth for memory managment
        }

        //If ball hits the star
        if (other.gameObject.CompareTag("Star"))
        {
            starCount++; //Player points increase by one
            SoundManager.PlaySound("bling"); //Play bling(Different than colorswicth) everytime player gets the star
            starCountText.text = starCount.ToString(); //Updates the HUD text to new count of stars
            Instantiate(StarPS, transform.position, transform.rotation); //Spawn and play star partcile effect
            Destroy(other.gameObject); //Destorys the picked star for memory managment
        }

        //If ball is in the obstacle, we have to check right colors and not destroy ball when it hits the star in the middle of obstacle
        if(insideObstacle)
            ObstacleManager(other);
    }

    void ObstacleManager(Collider2D other)
    {
        switch (playerColor)
        {
            //If ball is not red and hits the red part of obstacle, it dies
            case Colors.RED:
                if (!other.CompareTag("RedObstacle") && (!other.gameObject.CompareTag("Star")))
                {
                    StartCoroutine(die());
                }
                break;
            //If ball is not green and hits the red part of obstacle, it dies
            case Colors.GREEN:
                if (!other.CompareTag("GreenObstacle") && (!other.gameObject.CompareTag("Star")))
                {
                    StartCoroutine(die());
                }
                break;
            //If ball is not blue and hits the red part of obstacle, it dies
            case Colors.BLUE:
                if (!other.CompareTag("BlueObstacle") && (!other.gameObject.CompareTag("Star")))
                { 
                    StartCoroutine(die());
                }
                break;
            //If ball is not violet and hits the red part of obstacle, it dies
            case Colors.VIOLET:
                if (!other.CompareTag("VioletObstacle") && (!other.gameObject.CompareTag("Star")))
                {
                    StartCoroutine(die());
                }
                break;
        }
    }

    //"insideObstacle" boolean variable managment with OnTriggerStay and OnTriggerExit methods
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            insideObstacle = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            insideObstacle = false;
        }
    }

    //Timer for game over after the dead of the player
    IEnumerator die()
    {
        SoundManager.PlaySound("defeat"); //Play defeat sound
        isDead = true; //Sets the "isDead" boolean to true, so player has no input control anymore
        Instantiate(DeathPS, transform.position, transform.rotation); //Spawns and plays dead plarticle effect
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0); //Sets the color alpha to zero so the ball kind of dissapears
        
        //Wait one second and game pauses and gameover panel shows up
        yield return new WaitForSeconds(1);
        GM.GameOver(starCount);
    }


}
