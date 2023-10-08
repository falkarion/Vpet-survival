using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    public float moveSpeed = 1;
    private bool move = false;
    private Vector2 moveDirection;

    private Rigidbody rb;

    public List<Attack> attacks;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.actions["Move"];
        moveAction.started += OnMoveStarted;
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCancelled;

        for (int i = 0; i < attacks.Count; i++)
        {
            attacks[i].InitializeAttack(this.transform);
        }
    }

    public void OnMoveStarted(CallbackContext _obj)
    {
        move = true;
    }
    
    public void OnMovePerformed(CallbackContext _obj)
    {
        moveDirection = _obj.ReadValue<Vector2>();
    }

    public void OnMoveCancelled(CallbackContext _obj)
    {
        move = false;
        rb.velocity = Vector3.zero;
    }
    
    private void Update()
    {
        if (move)
        {
            Vector2 moveDelta = moveSpeed * moveDirection;
            rb.velocity = new Vector3(moveDelta.x, 0, moveDelta.y);
        }
    }
}