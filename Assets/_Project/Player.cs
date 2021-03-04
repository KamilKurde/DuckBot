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
    void Start()
    {
        if (speed == 0)
            throw new Exception("Prędkosć to zero deklu");
    }

    void Update()
    {
        if (direction.magnitude >= 0.1f)
        {
            // Obracanie postaci w kierunku ruchu
            float targetRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationAngle, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            
            // Stosujemy kierunek i mnożymy go przez prędkość oraz czas który upłnynął od poprzedniej klatki
            controller.Move(direction * (speed * Time.deltaTime)); 
        }
    }

    public void movement(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        
        // Aktualizujemy kierunek i normalizujemy go
        direction = new Vector3(inputMovement.x , 0, inputMovement.y).normalized; 
    }
}
