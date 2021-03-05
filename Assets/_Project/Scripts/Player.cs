using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField, Range(0.1f, 10f)] private float speed;
    [HideInInspector] public Vector3 direction = new Vector3(0, 0, 0);
    
    [Header("Roatation properties")] 
    [SerializeField, Range(1f, 50f)] private float smoothingSpeed = 20f;
    
    [SerializeField, Range(0.01f, 1f)] private float swingPower = 0.3f;

    // Variable ot keep last direction (for use when player is no longer moving)
    private Vector3 _lastDirection = new Vector3(0, 0, 1);

    void Start()
    {
    }

    void Update()
    {
        // Quaternion that hold rotation for character model
        var quaternion = Quaternion.LookRotation(_lastDirection);
        
        // If object is in move
        if (direction.magnitude >= 0.1f)
        {
            // Change rotation according to character movement direction + swing
            quaternion = Quaternion.LookRotation(direction + new Vector3(0f, -swingPower, 0f));
            _lastDirection = direction;
            controller.Move(direction * (speed * Time.deltaTime));
        }
        
        // Apply rotation
        var rotation = transform.rotation;
        rotation = Quaternion.Slerp(rotation, quaternion, smoothingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
    }

    // Method invoked by Player Input Component when input associated with movement changed
    public void Movement(InputAction.CallbackContext context)
    {
        var inputMovement = context.ReadValue<Vector2>();
        direction = new Vector3(inputMovement.x, 0, inputMovement.y).normalized;
    }
}