using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public CharacterController controller;
    public float speed;
    
    // Kierunek w którym poruszana jest postać co klatkę, zmieniany jest tylko gdy zmienia się input
    public Vector3 direction = new Vector3(0,0,0);

    // Wygładzanie obrotu postaci
    [Header("Roatation properties")]
    public float rotationSmoothingSpeed = 0.1f;
    private float _turnSmoothVelocity;
    [Header("Swing properties")]
    public float swingDegree;
    public float swingSmoothingSpeed = 0.01f;
    private float _swingSmoothVelocity;
    private float _rotationAngle = 0;
    void Start()
    {
        if (speed == 0f)
            throw new Exception("Speed is 0");
        if (swingDegree == 0f)
            throw new Exception("Swing is 0");
    }

    void Update()
    {
        // Wstępne obliczenia dla obrotu
        var targetSwingAngle = 0f;
        var targetRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        
        if (direction.magnitude >= 0.1f)
        {
            // Jeżeli w ruchu to przeliczamy obrót postaci, oraz ustawiamy docelowe wychylenie
            _rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationAngle, ref _turnSmoothVelocity, rotationSmoothingSpeed);
            targetSwingAngle = swingDegree;
            // Stosujemy kierunek i mnożymy go przez prędkość oraz czas który upłnynął od poprzedniej klatki
            controller.Move(direction * (speed * Time.deltaTime)); 
        }
        
        // Obracanie postaci w kierunku ruchu oraz wychylenie
        var currentSwingAngle =
            Mathf.SmoothDampAngle(transform.eulerAngles.x, targetSwingAngle, ref _swingSmoothVelocity, swingSmoothingSpeed);
        transform.rotation = Quaternion.Euler(currentSwingAngle, _rotationAngle, 0f);
    }

    public void movement(InputAction.CallbackContext context)
    {
        var inputMovement = context.ReadValue<Vector2>();
        
        // Aktualizujemy kierunek i normalizujemy go
        direction = new Vector3(inputMovement.x , 0, inputMovement.y).normalized; 
    }
}
