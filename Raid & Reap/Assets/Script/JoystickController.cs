using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompassJoystickStretch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joystickBackground;  // The background of the joystick (the compass)
    public RectTransform joystickHandle;      // The handle of the joystick (the directional arrow - red)
    public RectTransform centerHandle;        // The fixed white element in the center of the joystick
    public RectTransform blueHandle;  // The handle for the blue triangle
    public float blueDirectionOffset = 90f;  // Offset in degrees for the blue handle's direction
    public float handleRange = 100f;          // How far the handle can move
    public Canvas canvas;                     // The canvas containing the UI
    public float maxStretchFactor = 2.0f;     // Maximum stretch factor of the handle height

    public SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component

    public Sprite spriteUp;    // Sprite for walking up
    public Sprite spriteDown;  // Sprite for walking down
    public Sprite spriteLeft;  // Sprite for walking left
    public Sprite spriteRight; // Sprite for walking right

    private Vector2 inputDirection;
    private bool isDragging = false;

    public Vector2 InputDirection => inputDirection;  // Public property to get joystick direction

    private void Start()
    {
        // Hide the joystick background at the start
        joystickBackground.gameObject.SetActive(false);
        // Ensure the center handle is at the center of the joystick background
        centerHandle.localPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Set the joystick background to the point of touch and start dragging
        joystickBackground.position = eventData.position;  // Move to where the touch is
        joystickHandle.localPosition = Vector2.zero;  // Reset the handle to the center of the background
        joystickBackground.gameObject.SetActive(true);  // Show the joystick

        OnDrag(eventData);  // Start dragging
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
{
    if (!isDragging) return;

    // Get the drag position relative to the joystick background
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        joystickBackground, 
        eventData.position, 
        canvas.worldCamera, 
        out localPoint
    );

    // Calculate the joystick handle's new position relative to its background position
    Vector2 position = localPoint;
    position = Vector2.ClampMagnitude(position, handleRange);  // Clamp the handle within the defined range

    // Move the red joystick handle
    joystickHandle.localPosition = position;

    // Calculate input direction (normalized)
    inputDirection = position / handleRange;

    // Update the red handle's rotation to point in the direction of the input
    float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
    joystickHandle.rotation = Quaternion.Euler(0, 0, angle - 90);  // Rotate the handle accordingly

    // Stretch the red handle height based on the distance from the center
    float stretchFactor = 1 + (position.magnitude / handleRange) * (maxStretchFactor - 1);
    joystickHandle.localScale = new Vector3(1, stretchFactor, 1);  // Stretch along the Y-axis only

    // For the blue handle, calculate the opposite direction
    float blueAngle = angle + 180f;  // Opposite direction
    Vector2 blueDirection = new Vector2(Mathf.Cos(blueAngle * Mathf.Deg2Rad), Mathf.Sin(blueAngle * Mathf.Deg2Rad));

    // Set the position for the blue handle (same distance from center but opposite direction)
    blueHandle.localPosition = blueDirection * position.magnitude;

    // Rotate the blue handle to point in the opposite direction
    blueHandle.rotation = Quaternion.Euler(0, 0, blueAngle - 90);

    // Stretch the blue handle height (same as red)
    blueHandle.localScale = new Vector3(1, stretchFactor, 1);

    // Keep the white center handle fixed in the middle of the joystick
    centerHandle.localPosition = Vector2.zero;

    SetSpriteDirection(inputDirection);
}


void SetSpriteDirection(Vector2 direction)
{
    Debug.Log("Input direction: " + direction);  // Log the input direction to verify if it's detected

    if (direction.magnitude > 0.1f)  // Only switch sprites if there's a notable movement
    {
        if (direction.y > 0.1f) // Moving up
        {
            spriteRenderer.sprite = spriteUp;
            Debug.Log("Changed to Up Sprite");
        }
        else if (direction.y < -0.1f) // Moving down
        {
            spriteRenderer.sprite = spriteDown;
            Debug.Log("Changed to Down Sprite");
        }
        else if (direction.x > 0.1f) // Moving right
        {
            spriteRenderer.sprite = spriteRight;
            Debug.Log("Changed to Right Sprite");
        }
        else if (direction.x < -0.1f) // Moving left
        {
            spriteRenderer.sprite = spriteLeft;
            Debug.Log("Changed to Left Sprite");
        }
    }
}


    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick and hide it when touch is released
        inputDirection = Vector2.zero;
        joystickHandle.localPosition = Vector2.zero;  // Reset to center
        joystickHandle.localScale = Vector3.one;      // Reset the scale to default
        joystickBackground.gameObject.SetActive(false);  // Hide joystick when touch is released
        isDragging = false;

        // Ensure the white center handle stays centered
        centerHandle.localPosition = Vector2.zero;
    }
}
