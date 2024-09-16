using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CompassJoystickStretch joystick;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // Make sure gravity is disabled for 2D movement.
    }

    private void Update()
    {
        // Get joystick input direction
        Vector2 movement = joystick.InputDirection;

        // Apply movement if there's input
        if (movement.magnitude > 0.1f)  // Tolerance to avoid small jitter
        {
            // Normalize movement to ensure consistent speed in diagonal directions
            movement = movement.normalized;
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;  // Stop movement when no input is provided
        }
    }
}
