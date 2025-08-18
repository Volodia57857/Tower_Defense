using UnityEngine;



public class FreeMoveCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;
    public float fastSpeedMultiplier = 2f;

    float rotationX = 0f;
    float rotationY = 0f;

    void Start()
    {
        // Lock cursor to center and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Prevent flipping

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Movement
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed *= fastSpeedMultiplier;

        Vector3 move = new Vector3(
            Input.GetAxis("Horizontal"), // A/D
            0,
            Input.GetAxis("Vertical")    // W/S
        );

        // Up/Down movement (Q/E or Space/Ctrl)
        if (Input.GetKey(KeyCode.E)) move.y += 1;
        if (Input.GetKey(KeyCode.Q)) move.y -= 1;

        transform.Translate(move * speed * Time.deltaTime);
    }
}