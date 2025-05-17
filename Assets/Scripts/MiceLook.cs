using UnityEngine;

public class MiceLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor in center
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Handle vertical (up/down) rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent extreme up/down rotation

        // Apply vertical rotation to the camera itself (camera looks up/down)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply horizontal rotation to the camera itself (camera rotates left/right)
        transform.parent.Rotate(Vector3.up * mouseX); // Rotate the parent (player object) for horizontal movement
    }
}