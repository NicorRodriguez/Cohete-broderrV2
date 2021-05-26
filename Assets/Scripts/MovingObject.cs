using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor; // Using 0 to 1, to set 0 as the start position and 1 as the maximun offset of the position
    [SerializeField] float period = 4f;

    void Start(){
        startingPos = transform.position; // Gets the starting position of the object

    }

    void Update(){
        float cycles = Time.time / period; // The variable cycles gets bigger with time

        const float tau = Mathf.PI * 2; // Constant value of 2pi

        float rawSinWave = Mathf.Sin(cycles * tau); // Uses sin to calculate a value between -1 and 1 using the varaible cicle to increment the value multiplied by 2pi

        movementFactor = (rawSinWave + 1f)/2; // Uses the value rawSinWave but transforms it to get a value between 0 and 1 instead of -1 to 1

        Vector3 offset = movementFactor * movementVector; // Multiplies the the final vector with the mmovement factor in the range of o to 1
        transform.position = startingPos + offset; // Moves the position of the objet to the calculated position

    }
}
