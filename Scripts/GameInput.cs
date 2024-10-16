using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public bool GetJumpInput() {
        return playerInputActions.Player.Jump.IsPressed();
    }

    public bool GetInteractInput() {
        return playerInputActions.Player.Interact.WasPerformedThisFrame();
    }

    public bool GetFireSinglePress() {
        return playerInputActions.Player.Fire.WasPerformedThisFrame();
    }

    public bool GetFireDown() {
        return playerInputActions.Player.Fire.IsPressed();
    }

    public bool GetFireUp() {
        return playerInputActions.Player.Fire.WasReleasedThisFrame();
    }
}
