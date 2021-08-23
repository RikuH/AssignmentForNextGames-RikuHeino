using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static AudioClip tap, star, color, defeat; //Audio clips for tapping, star and color switch collect and defeat
    static AudioSource audioSource;

    public Slider volumeSlider; //UI slider for volume control.


    // Start is called before the first frame update
    void Start()
    {
        //Sounds are loaded from resource folder at the start
        tap = Resources.Load<AudioClip>("Sounds/plop");
        star = Resources.Load<AudioClip>("Sounds/bling");
        color = Resources.Load<AudioClip>("Sounds/bling002");
        defeat = Resources.Load<AudioClip>("Sounds/defeat");

        audioSource = GetComponent<AudioSource>();

    }

    //PlaySound method is called from PlayerBall class in various events
    public static void PlaySound(string SoundClip)
    {
        switch (SoundClip)
        {
            //Plop is played after screen tap
            case "plop":
                audioSource.PlayOneShot(tap);
                break;
            //Bling is played after player gets a star
            case "bling":
                audioSource.PlayOneShot(star);
                break;
            //Bling002 is played after player gets a color switch
            case "bling002":
                audioSource.PlayOneShot(color);
                break;
            //Defeat is played after player hits the wrong color in obstalce or drops below the camera border
            case "defeat":
                audioSource.PlayOneShot(defeat);
                break;
        }
    }

    //ChangeVolume Method is called in slider OnValueChanged event
    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value; //Sets the master volume by sliders value
    }
}
