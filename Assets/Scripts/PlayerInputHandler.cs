using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;

    private PlayerMovement movement;

    private PlayerControls controls;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == controls.Player.Jump.name)
        {
            OnJump(obj);
        }
        if (obj.action.name == controls.Player.Flip.name)
        {
            OnFlip(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (movement != null)
            movement.SetMovement(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context)
    {
        if (movement != null && context.performed)
            movement.Jump();
    }

    public void OnFlip(CallbackContext context)
    {
        if (movement != null && context.performed)
            movement.Flip();
    }
}
