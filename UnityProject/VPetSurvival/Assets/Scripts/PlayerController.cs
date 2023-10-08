using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    public float moveSpeed = 1;
    private bool move = false;
    private Vector2 moveDirection;
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["Move"];
        moveAction.started += OnMoveStarted;
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCancelled;
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
    }
    
    private void Update()
    {
        if (move)
        {
            Vector2 moveDelta = moveSpeed * moveDirection * Time.deltaTime;
            this.transform.position += new Vector3(moveDelta.x, 0, moveDelta.y);
        }
    }
}