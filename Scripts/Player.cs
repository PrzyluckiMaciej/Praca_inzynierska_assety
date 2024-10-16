using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 1f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool isReadyTojump;


    private Rigidbody rb;
    private Vector2 inputVector;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isReadyTojump = true;
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    private void Update() {
        SpeedControl();
        CheckGround();
        AssignGroundDrag();
        //Debug.Log(isGrounded);
    }

    private void CheckGround() {
        // sprawdzenie czy gracz jest na ziemi
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    private void SpeedControl() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void AssignGroundDrag() {
        if (isGrounded) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = airDrag;
        }
    }

    private void Move() {
        inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        moveDir.y = 0f;

        if (isGrounded) {
            rb.AddForce(moveDir * moveSpeed * 10f * Time.deltaTime, ForceMode.Force);
        }
        else {
            rb.AddForce(moveDir * moveSpeed * 10f * Time.deltaTime * airMultiplier, ForceMode.Force);
        }

        /*Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        targetVelocity.y = 0f;
        targetVelocity *= Time.deltaTime * moveSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange.y = 0f;

        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);*/
    }

    private void Jump() {
        bool isJumpKeyDown = gameInput.GetJumpInput();
        if (isJumpKeyDown && isReadyTojump && isGrounded) {
            isReadyTojump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump() {
        isReadyTojump = true;
    }
}
