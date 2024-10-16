using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserGun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform laserOutputTransform;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float laserRange;

    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start() {
        DisableLaser();
    }

    private void Update() {

        if (gameInput.GetFireSinglePress()) {
            EnableLaser();
        }

        if (gameInput.GetFireDown()) {
            UpdateLaser();
        }

        if (gameInput.GetFireUp()) {
            DisableLaser();
        }
    }

    private void EnableLaser() {
        lineRenderer.enabled = true;
    }

    private void UpdateLaser() {
        lineRenderer.SetPosition(0, laserOutputTransform.position);

        Vector3 rayOrigin = playerCamera.transform.position;

        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hitInfo)) {
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else {
            lineRenderer.SetPosition(1, rayOrigin + (playerCamera.transform.forward * laserRange));
        }
    }

    private void DisableLaser() {
        lineRenderer.enabled = false;
    }
}
