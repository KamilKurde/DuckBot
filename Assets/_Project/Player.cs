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
    public float smoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float swingDegree;
    private float swingSmoothVelocity;
    private float rotationAngle = 0;
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
            rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationAngle, ref turnSmoothVelocity, smoothTime);
            targetSwingAngle = swingDegree;
            // Stosujemy kierunek i mnożymy go przez prędkość oraz czas który upłnynął od poprzedniej klatki
            controller.Move(direction * (speed * Time.deltaTime)); 
        }
        
        // Obracanie postaci w kierunku ruchu oraz wychylenie
        var swingAngle =
            Mathf.SmoothDampAngle(transform.eulerAngles.x, targetSwingAngle, ref swingSmoothVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(swingAngle, rotationAngle, 0f);
    }

    public void movement(InputAction.CallbackContext context)
    {
        var inputMovement = context.ReadValue<Vector2>();
        
        // Aktualizujemy kierunek i normalizujemy go
        direction = new Vector3(inputMovement.x , 0, inputMovement.y).normalized; 
    }
}
