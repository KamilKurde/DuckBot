using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public enum EqState
{
    CanPlace,
    CanTake,
    CantPlace,
    NoTile
}

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0.1f, 10f)] private float speed;

    [Header("Rotation properties")] 
    [SerializeField, Range(1f, 50f)] private float rotationSpeed = 20f;
    [SerializeField, Range(0.01f, 1f)] private float swingPower = 0.3f;
    public EqState state = EqState.NoTile;
    private IInteractable _interactable = null;
    private PlaceTile placeTile = null;

    private Vector3 _movementDirection = Vector3.zero;
    // Variable ot keep last direction (for use when player is no longer moving)
    private Vector3 _lastMovementDirection = Vector3.forward;
    private Vector3 playerVelocity;

    private bool isPaused = false;

    public bool levelFinished = false;

    private void Start()
    {
        GameManager.player = this;
        controller.Move(Vector3.up * 8f);
        if (SceneManager.GetActiveScene().buildIndex > GameSave.CurrentLevelId)
        {
            GameSave.CurrentLevelId = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void Update()
    {
        if (levelFinished)
        {
            return;
        }
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
        if (levelFinished)
        {
            return;
        }
        if (!context.started)
        {
            return;
        }
        _interactable?.Interact();
    }

    private void UpdateEqState()
    {

        if (placeTile == null)
        {
            state = EqState.NoTile;
            return;
        }
        
        if (GameManager.placeable != null && !placeTile.HasPlaceable)
        {
            state = EqState.CanPlace;
            return;
        }

        if (GameManager.placeable == null && placeTile.HasPlaceable)
        {
            state = EqState.CanTake;
            return;
        }

        if (GameManager.placeable != null && placeTile.HasPlaceable)
        {
            state = EqState.CantPlace;
            return;
        }
    }

    public void OnPlaceInput(InputAction.CallbackContext context)
    {
        if (levelFinished)
        {
            return;
        }
        
        if (!context.started)
        {
            return;
        }

        if (placeTile == null)
        {
            return;
        }

        if (GameManager.placeable == null && !placeTile.HasPlaceable)
        {
            return;
        }

        if (GameManager.placeable != null && placeTile.HasPlaceable)
        {
            return;
        }

        if (GameManager.placeable == null && placeTile.HasPlaceable)
        {
            GameManager.placeable = placeTile.placeable;
            placeTile.placeable = null;
            GameManager.placeable.Hide();
            UpdateEqState();
            return;
        }

        if (GameManager.placeable != null && !placeTile.HasPlaceable)
        {
            placeTile.SetPlaceable(GameManager.placeable);
            GameManager.placeable = null;
            UpdateEqState();
        }
    }

    public void SetPauseState(bool state)
    {
        isPaused = state;
        GameManager.uImanager.pauseGroup.alpha = isPaused ? 1f : 0f;
        GameManager.uImanager.pauseGroup.interactable = isPaused;
        GameManager.uImanager.pauseGroup.blocksRaycasts = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnPauseEnter(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        isPaused = !isPaused;
        SetPauseState(isPaused);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelFinished)
        {
            return;
        }
        var collidedInteractable = other.gameObject.GetComponent<IInteractable>();
        if (collidedInteractable != null)
        {
            _interactable = collidedInteractable;
        }

        var collidedTile = other.gameObject.GetComponent<PlaceTile>();
        if (collidedTile != null)
        {
            placeTile = collidedTile;
        }
        UpdateEqState();
    }

    private void OnTriggerExit(Collider other)
    {
        if (levelFinished)
        {
            return;
        }
        if (other.gameObject.GetComponent<IInteractable>() == _interactable)
        {
            _interactable = null;
        }

        if (other.gameObject.GetComponent<PlaceTile>() == placeTile)
        {
            placeTile = null;
        }
        UpdateEqState();
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
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            newRotation = Quaternion.LookRotation(_lastMovementDirection);
            audioSource.Stop();
        }

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.enabled = false;
        }

        playerVelocity.y = Physics.gravity.y * Time.deltaTime;

        if (!controller.isGrounded)
        {
            controller.Move(playerVelocity);
        }

        // Apply rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }

    public bool HasInteractable()
    {
        return _interactable != null;
    }
}