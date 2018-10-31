using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    public float health = 100f;
    [SerializeField]
    private Transform OtherSpine;

    private int MunitionGun1;

    private Vector3 velocity;
    private Vector3 rotation;
    

    [SerializeField]
    private float cameraRotationLimit = 85f;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    private Rigidbody rb;

    // Animator
    Animator Anim;



    private void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
        //rotation du corps
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + rotation); // La méthode Euler prend directement une variable Vector3 ( x, y, z )

        //rotation de l'angle de other spine 
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        // Applique les changements à la caméra après le clamp
        OtherSpine.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

        //OtherSpine.transform.Rotate(-cameraRotation);// Le (-) est obligatoire car c'est renversé cause: les vieux jeux
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

