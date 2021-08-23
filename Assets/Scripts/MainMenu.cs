using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject soundManager; //Gameobject for soundmanager

    //Methof for start game button
    public void StartGame()
    {
        SceneManager.LoadScene(1); //Change to game scene
        DontDestroyOnLoad(soundManager); //Doesnt destroy the soundmanager gamebject so the music keeps playing 
    }
}
