using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionBehaviour : MonoBehaviour{

    [SerializeField] float crashDelay=1.5f;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] ParticleSystem loseParticle;
    [SerializeField] ParticleSystem winParticle;

    [SerializeField] public bool transitioning=false;

//This part of the code checks if the rocket collisioned with another object in the game, in each case it calls the respective function
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag){
            case "Respawn":
                Choke();
                break;
            case "Finish":
                WinSeq();
                break;
            default:
                break;
        }
    }

//Function that activates when the rocket collision with an oject that will end the level
    void Choke(){
        transitioningActions(loseSound, loseParticle);
        Invoke("ReloadScene", crashDelay); // Calls the reloadScene function
    }

    void WinSeq(){
        transitioningActions(winSound, winParticle);

        Invoke("NextScene", crashDelay); // Calls the NextScene function
    }

    void transitioningActions(AudioClip clip, ParticleSystem particles){
        if (!transitioning){ // Checks that it is not transitioning thru scenes, to avoid entering in a loop

            particles.Play(true); // Play the win or lose particles of the rocket
            GetComponent<AudioSource>().Stop(); // Stops the sound of the engine of the rocket
            GetComponent<AudioSource>().PlayOneShot(clip); // Plays the loosing or winning clip
            transitioning=true; //Sets the transitioning varaible to true
        }
        GetComponent<Mover>().enabled= false; // Deactivates the rocket Mover script
    }

    void NextScene(){
        if(SceneManager.GetActiveScene().buildIndex==2) SceneManager.LoadScene(0); // Checks if it is in the las scene of the game, in case it is goes to the first scene
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // if not it will go to the next scene
        transitioning=false; //Sets the transitioning varaible to false
    } 
    void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the current scene
        transitioning=false; //Sets the transitioning varaible to false
    }    
}