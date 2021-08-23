using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Vector2 ScreenBounds;

    float rotateSpeed = 50; //Speed for obstacle rotating

    public bool noRotate; //Boolean for non rotating obstacles
    public bool counterRotate; //If the obstacle need to rotate another way


    // Start is called before the first frame update
    void Start()
    {
        //If the counterRotate boolean is checked in Unity inspector, the obstacle is rotating another way
        if (counterRotate)
            rotateSpeed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)); //Calculate the screen bounds

        //If the noRotate boolean is checked in Unity inspector, the obstacle is not rotating
        //Else the obstacle rotates with rotateSpeed
        if (!noRotate)
            gameObject.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime,Space.World);

        //When the obstacle is below the screen bound, it will be destroyed
        if (transform.position.y < ScreenBounds.y - 2)
        {
            Destroy(gameObject);
        }
    }

}

