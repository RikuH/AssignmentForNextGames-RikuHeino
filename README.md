# AssignmentForNextGames-RikuHeino
 Assignment for Next Games trainee program
 
 ## Features I would like to add and how to implement them
 * More different obstacles
   * Just making them
 
 * Some kind of balanced ascending difficulty
   * Maybe bringing the obstacles closer to each other with star count variable
 
 * Saving feature
   * Saving the game progress to local machine with json file

* "Battle royale" online game mode
   * Using Photon PUN 2 engine

## About the game
The game must be started via the main menu

The project is made to 1080 x 1920 portrait resolution

## Prototype implementation
* When tap the screen, the ball moves up and camera follows it when it reach the highest point of y axis
  * It was clearer to me to move the ball rather than obstacles

* Random obstacle spawn every time when player catch the color switcher
  * I think it was the simpliest way to spawn obstacles rather than calculate the ball position in screen

* Obstacles are destroyed after they reach bottom of screen bounds
  * This saves the memory

* Ball color switcher is made using random variable, but it also check that the new color is not the same that the old color was.
  * I just take the color variable and put it in the temporary storage. If it is same as before, it will increased by one

* The stars are simply spawned middle of the obstacles and screen.

* Obstacles color part is just a game object with a color tag and in PlayerBall class it check if the ball hits to it.

## Credits
As it reads in the "sounds-copyright.txt" all sounds and musics are downloaded from freesound.org so is the game music (that was added by me)

And the "plop" sound was .flac file so I convert it to .wav

