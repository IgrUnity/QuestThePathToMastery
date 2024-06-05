using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform character;
    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Tracking();
    }

    private void Tracking()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        character.Rotate(Vector3.up * mouseX);
    }
}
