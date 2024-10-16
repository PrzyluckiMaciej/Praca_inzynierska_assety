using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject laserOutput;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float laserRange;
    [SerializeField] private Camera playerCamera;

    private GameObject spawnedLaser;
    private LineRenderer lineRenderer;

    private void Start() {
        spawnedLaser = Instantiate(laserPrefab, laserOutput.transform);
        lineRenderer = spawnedLaser.GetComponentInChildren<LineRenderer>();

        Debug.Log(lineRenderer);
        
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
        spawnedLaser.SetActive(true);
    }

    private void UpdateLaser() {
        if(laserOutput != null) {
            spawnedLaser.transform.position = laserOutput.transform.position;

            Vector3 rayOrigin = playerCamera.transform.position;

            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hitInfo)) {
                lineRenderer.SetPosition(1, hitInfo.point);
                /*var direction = (hitInfo.point - spawnedLaser.transform.position).normalized;
                var targetRotation = Quaternion.LookRotation(direction);
                spawnedLaser.transform.rotation = targetRotation;*/
            }
            else {
                lineRenderer.SetPosition(1, rayOrigin + (playerCamera.transform.forward * laserRange));
            }

            Debug.Log(hitInfo.point + " --- " + lineRenderer.GetPosition(1));
        }
    }

    private void DisableLaser() {
        spawnedLaser.SetActive(false);
    }
}
