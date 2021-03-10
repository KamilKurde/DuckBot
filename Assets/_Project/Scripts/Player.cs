using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField, Range(0.1f, 10f)] private float speed;
    
    [Header("Rotation properties")] 
    [SerializeField, Range(1f, 50f)] private float rotationSpeed = 20f;
    [SerializeField, Range(0.01f, 1f)] private float swingPower = 0.3f;
    private IInteractable _interactable;
    private PlaceTile _placeTile;

    private Vector3 _movementDirection = Vector3.zero;
    // Variable ot keep last direction (for use when player is no longer moving)
    private Vector3 _lastMovementDirection = Vector3.forward;
    private Vector3 playerVelocity;

    private void Update()
    {
        Move();
    }

    // Method invoked by Player Input Component when input associated with movement changed
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        var inputMovement = context.ReadValue<Vector2>();
        _movementDirection = new Vector3(inputMovement.x, _movementDirection.y, inputMovement.y);
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        _interactable?.Interact();
    }

    public void OnPlaceInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (_placeTile == null)
        {
            return;
        }

        if (GameManager.placeable == null && !_placeTile.HasPlaceable)
        {
            return;
        }

        if (GameManager.placeable != null && _placeTile.HasPlaceable)
        {
            return;
        }

        if (GameManager.placeable == null && _placeTile.HasPlaceable)
        {
            GameManager.placeable = _placeTile.placeable;
            _placeTile.placeable = null;
            GameManager.placeable.Hide();
            return;
        }

        if (GameManager.placeable != null && !_placeTile.HasPlaceable)
        {
            _placeTile.SetPlaceable(GameManager.placeable);
            GameManager.placeable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var collidedInteractable = other.gameObject.GetComponent<IInteractable>();
        if (collidedInteractable != null)
        {
            _interactable = collidedInteractable;
        }

        var collidedTile = other.gameObject.GetComponent<PlaceTile>();
        if (collidedTile != null)
        {
            _placeTile = collidedTile;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() == _interactable)
        {
            _interactable = null;
        }

        if (other.gameObject.GetComponent<PlaceTile>() == _placeTile)
        {
            _placeTile = null;
        }
    }

    private void Move()
    {
        // New rotation for the character
        Quaternion newRotation;

        // If object is in move
        if(_movementDirection != Vector3.zero)
        {
            // Change rotation according to character movement direction + swing
            newRotation = Quaternion.LookRotation(_movementDirection + new Vector3(0f, -swingPower, 0f));
            controller.Move(_movementDirection * (speed * Time.deltaTime));
            _lastMovementDirection = _movementDirection;
        }
        else
        {
            newRotation = Quaternion.LookRotation(_lastMovementDirection);
        }

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y = Physics.gravity.y * Time.deltaTime;

        if (!controller.isGrounded)
        {
            controller.Move(playerVelocity);
        }

        // Apply rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}