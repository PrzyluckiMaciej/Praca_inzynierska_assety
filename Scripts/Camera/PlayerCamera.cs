using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour {

    public static PlayerCamera Instance { get; private set; }

    [SerializeField] private float sensitivity;   

    private Vector2 look;
    private float lookRotation;
    private float xRotation, yRotation;

    public void OnLook(InputAction.CallbackContext context) {
        look = context.ReadValue<Vector2>();
    }

    private void Awake() {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Look();
    }

    private void Look() {
        float xMouseInput = look.x * Time.deltaTime * sensitivity;
        float yMouseInput = look.y * Time.deltaTime * sensitivity;

        yRotation += xMouseInput;
        xRotation -= yMouseInput;

        // ograniczenie spojrzenia góra - dó³
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // obrócenie kamery
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); 
    }
}
