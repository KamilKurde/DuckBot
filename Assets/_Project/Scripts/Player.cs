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

        if (GameManager.placable == null && !_placeTile.HasPlacable())
        {
            return;
        }

        if (GameManager.placable != null && _placeTile.HasPlacable())
        {
            return;
        }

        if (GameManager.placable == null && _placeTile.HasPlacable())
        {
            GameManager.placable = _placeTile.placable;
            _placeTile.placable = null;
            GameManager.placable.Hide();
            return;
        }

        if (GameManager.placable != null && !_placeTile.HasPlacable())
        {
            _placeTile.SetPlacable(GameManager.placable);
            GameManager.placable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var colidedInteractable = other.gameObject.GetComponent<IInteractable>();
        if (colidedInteractable != null)
        {
            _interactable = colidedInteractable;
        }

        var colidedTile = other.gameObject.GetComponent<PlaceTile>();
        if (colidedTile != null)
        {
            _placeTile = colidedTile;
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

        // Apply rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}