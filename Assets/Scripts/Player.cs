using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

    [SerializeField]
    int maxHealth = 100;
    [SyncVar]
    int currentHealth;

    private PlayerMotor motor;
    private Rigidbody rb;


    private void Awake()
    {
        SetDefaults();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        print(gameObject.transform.parent);
    }

    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " a maintenant : " + currentHealth);
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    public void MoveDamaged(Vector3 _velocity)
    {
        if (_velocity != Vector3.zero)
        {
            // Si on détect un mouvement
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }
    }
}
