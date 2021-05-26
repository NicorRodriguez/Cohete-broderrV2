using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody rb;
    AudioSource rocketAudio;
    [SerializeField] float mainPower = 1000f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] ParticleSystem mainRocketParticle;
    [SerializeField] ParticleSystem leftRocketParticle;
    [SerializeField] ParticleSystem rightRocketParticle;


    [SerializeField] AudioClip engineSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
        rocketAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessImput();
    }
    

    void ProcessImput(){
        //Movement of the rocket to up
        if (Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector2.up * mainPower * Time.deltaTime); //Aplies a up force to the object

            if(!mainRocketParticle.isPlaying) mainRocketParticle.Play(); //Check if the particles of the main engine of the rocket are not playing, in case they are not it will play them
            if(!rocketAudio.isPlaying) rocketAudio.PlayOneShot(engineSound); //Check if the sound of the main engine of the rocket is not playing, in case it is not it will play it

        }else{
            //Stops the rocket engine sound and the main motor particles in case no forced applied
            rocketAudio.Stop();
            mainRocketParticle.Stop();

        }
        
        if (Input.GetKey(KeyCode.A)) rotateLeft();
        else if (Input.GetKey(KeyCode.D)) rotateRight();
        else{
            //Stops both of the side motors particles in case of no rotation applied
            rightRocketParticle.Stop();
            leftRocketParticle.Stop();
        }
    }

    //Movement of the rocket to the right
    private void rotateRight()
    {
        applyRotation(-rotationThrust); //Applies rotation to the object

        if (mainRocketParticle.isPlaying) mainRocketParticle.Stop(); //Check if the particles of the main engine of the rocket are playing, in case they are it will stop it
        if (!rightRocketParticle.isPlaying) rightRocketParticle.Play(); //Checks that the particles of the right engine of the rocket are not playing, if this is the case it will play them
    }

    //Movement of the rocket to the left
    private void rotateLeft()
    {
        applyRotation(rotationThrust);//Applies rotation to the object

        if (mainRocketParticle.isPlaying) mainRocketParticle.Stop();//Check if the particles of the main engine of the rocket are playing, in case they are it will stop it
        if (!leftRocketParticle.isPlaying) leftRocketParticle.Play();//Checks that the particles of the left engine of the rocket are not playing, if this is the case it will play them
    }

    //Rotation of the rocket
    void applyRotation(float rotationThisFrame){
    
    rb.freezeRotation = true; //Freezes the rotation before applying the rotation to the object, this does not allow the phisics of collisions alter the rotation of the object
    transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame); //Applies the rotation get in rotationThisFrame to the object
    rb.freezeRotation = false; //Deactivates the freezed rotation after applying the rotation to the object, this allows the phisics of collisions alter the rotation of the object
}

}
