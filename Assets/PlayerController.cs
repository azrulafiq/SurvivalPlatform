using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed ; //set speed for player, set via unity editor
    void Update()
    {
        // Get Input from user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set movement with 3 coordinate based on user input.
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Normalize movement to set same speed of player when going diagonal
        movement.Normalize();

        // Set position of player
        transform.Translate(movement * speed * Time.deltaTime);
    }
}