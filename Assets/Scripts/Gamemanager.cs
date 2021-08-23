using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject[] Obstacles; //Array for obstacles
    public GameObject Star; //Collectable star
    public GameObject ColorSwitcher; //Collectable color switch

    float distance = 3; //Distance where the next obstacle spawns from previous

    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public TextMeshProUGUI GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        //Call the SpawnObstacle method twice so there is always 2 obstacles in kind of a "Stack"
        SpawnObstacle();
        SpawnObstacle();
    }

    //Method for new obstacle spawning. This is called at start and in the PlayerBall class
    public void SpawnObstacle()
    {
        float x = 0; //x variable is for various obstacles that is needs to be aside
        float y = 4; //y variable for offset of obstacle and color switcher
        int random = Random.Range(0, Obstacles.Length); //Variable for random obstacle index

        //If instantiated obstacle is "CrossObstacle" the x variable moves it little bit on the x axis
        if (Obstacles[random].gameObject.CompareTag("CrossObstacle"))
        {
            x = 0.9f;
        }
        //Else the instantiated obstacle is on the middle of x axis
        else
        {
            x = 0;
        }

        //Instantiates the obstacle and start inside of it and then the color switch above the obstacle
        Instantiate(Obstacles[random], new Vector2(x, distance), Quaternion.identity);
        Instantiate(Star, new Vector2(0, distance), Quaternion.identity);
        Instantiate(ColorSwitcher, new Vector2(0, distance + y), Quaternion.identity);

        distance += 7; //Increase distance by 7, so the obstalces are nicely away form each other
    }

    //Method for pause button
    public void PauseGame()
    {
        Time.timeScale = 0; //Sets the time scale to 0 so the game is not moving
        PausePanel.SetActive(true);
    }
    //Method for resume button
    public void ResumeGame()
    {
        Time.timeScale = 1;//Normalize the time scale
        PausePanel.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0); //Get back to main menu scene
        Destroy(GameObject.Find("SoundManager").gameObject); //Destorys the sound manager game object so the 
        Time.timeScale = 1; //Normalize the time scale
        GameOverPanel.SetActive(false);//Sets the Game over panel unactive
    }

    //Method for game over. Called from PlayerBall class with count of stars
    public void GameOver(int stars)
    {
        GameOverPanel.SetActive(true); //Sets the Game over panel active
        GameOverText.text = "Oh dear. You are dead.\nYou got "+ stars +" stars.\n\nTry again?";//Game over text with the count of stars
        Time.timeScale = 0; //Sets the time scale to 0 so the game is not moving
    }
    //Method for Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Restarts the game scene
        Time.timeScale = 1; //Normalize the time scale
        GameOverPanel.SetActive(false);//Sets the Game over panel unactive
    }

}
