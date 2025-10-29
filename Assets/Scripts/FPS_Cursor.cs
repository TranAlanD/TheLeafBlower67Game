using UnityEngine;

public class FPS_Cursor: MonoBehaviour
{
    public float mouseSensitivity;
    float xRotation = 0;
    public Camera playerCamera;
    public int topAngle;
    public int bottomAngle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity *Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity *Time.deltaTime;

        xRotation -=mouseY;
        xRotation = Mathf.Clamp(xRotation, topAngle, bottomAngle);
        playerCamera.transform.localRotation =Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up*mouseX);
    }
}
