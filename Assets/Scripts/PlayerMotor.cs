using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    public float health = 100f;

    // section camera
    [SerializeField]
    private Camera cam;
    private float currentCameraRotationX = 0f;
    private float cameraRotationX = 0f;
    [SerializeField]
    private float cameraRotationLimit = 85f;    // Definition de la limite de la rotation de la camera


    private int MunitionGun1;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    private Rigidbody rb;

    // Animator
    Animator Anim;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotation)
    {
        cameraRotationX = _cameraRotation;
    }


    // For aplaying forces to the rigidbody, C'est un update special pour la physique: before physics calculation
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            // Si on détect un mouvement
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            // Animer le joueur du mouvement selon le paramètre
            Anim.SetBool("Mouving", true);
        }
        else
            Anim.SetBool("Mouving", false);
    }

    void PerformRotation()
    {
        // Récuperation de la rotation + Clamp la rotation 
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        // Applique les changements à la caméra après le clamp
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
            Debug.Log("Player dead");
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(GetComponent<PlayerController>());
    }

    public void GiveMunition(string Gun)
    {
        if (Gun == "Gun1") { MunitionGun1 += 30; Debug.Log(this + ":" + MunitionGun1); }
            
    }

}

