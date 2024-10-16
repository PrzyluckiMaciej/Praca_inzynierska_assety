using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float interactionDistance = 3f;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameInput gameInput;

    private void Update() {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, interactionDistance, layerMask)) {
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                //Debug.Log(interactable.promptMessage);
                if (gameInput.GetInteractInput()) {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
