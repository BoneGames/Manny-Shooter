using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    [Tooltip("is the cursor hidden?")]
    public bool showCursor = true;
    public Vector2 speed = new Vector2(120f, 120f);
    public float yMinLimit = -80f, yMaxLimit = 80f;
    public bool invertControls = false;

    private float x, y; // Current X and Y degrees of rotation


    void Start ()
    {
        // Hide Cursor
        Cursor.visible = showCursor;

        Cursor.lockState = showCursor ? Cursor.lockState = CursorLockMode.None : CursorLockMode.Locked;
        // Get current camera Euler rotation
        Vector3 angles = transform.eulerAngles;
        // Set X and Y degrees to current camera rotation
        x = angles.y;
        y = angles.x;
        // Pitch (X) Yaw (Y) Roll (Z)
    }

    void Update ()
    {
        // get mouse offsets
        float mouseX = Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
        // check for inversion
        mouseY = invertControls ? -mouseY : mouseY;
        // Rotate camera based on Mouse X and Y
        x += mouseX;
        y -= mouseY;
        // Clamp the angle of the pitch
        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
        // Rotate parent on Y axis (yaw)
        // Rotate local on X axis (pitch)
        
        if(transform.parent)
        {
            transform.parent.rotation = Quaternion.Euler(0, x, 0);
            transform.localRotation = Quaternion.Euler(y, 0, 0);
        }
        else
        {
            // Rotate
            transform.localRotation = Quaternion.Euler(y, x, 0);
        }
        
    }
}
