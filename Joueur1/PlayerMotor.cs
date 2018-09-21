using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private CharacterController controller;

    //movement
    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    //rb
    private Rigidbody rb;

    //sauter et gravité
    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 10.0f;
    

    [SerializeField]
    private float cameraRotationLimit = 90f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        //recuperation de la rotation + clamp la rotation
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        //applique les changements a la camera apres le clamp
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

   /* private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
                rb.GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }

       
        
    }*/

    
}

