using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {


    enum States {Alive, Dead, Incap};
    States state;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]//Pouvoir re trouver ce paramètre dans le IDE de Unity
    private float lookSensitivityY = 3f;
    [SerializeField]
    private float lookSensitivityX = 3f;

    private PlayerMotor motor;
    private Animator PlayerAnimator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        PlayerAnimator = GetComponent<Animator>();
        state = States.Alive;
    }

    // Update() va être appeler à toutes les frames, est appelé avant que le frame se fait.
    private void Update()
    {
        if (state == States.Alive) HandleMovement();
    }

    private void HandleMovement()
    {
        // On va calculer la vélocité du mouvement du joueur en un Vecteur 3D
        // Permet de reconnaitre la direction vers laquelle on se dirige + à quelle vitesse


        // Différence entre GetAxis et GetAxisRaw


        float _xMovH = Input.GetAxisRaw("Horizontal"); //Nous permet de récupérer les touches pour aller à droite et à gauche
        /*
         -1 = left
         0 = le personnage ne bouge pas
         1 = droite
         */


        float _zMovV = Input.GetAxisRaw("Vertical"); //Nous permet de récupérer les touches pour avancer et reculer
        /*
         -1 = recule
         0 = ne bouge pas
         1 = avance
         */


        Vector3 _moveHorizontal = transform.right * _xMovH; // ( x, y, z) ---> ( 0, 0, 1 )
        Vector3 _moveVertical = transform.forward * _zMovV;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed; // Vector3 (moveHorizontal, _yRot, moveVertical);

        motor.Move(_velocity);

        // On va calculer la rotation du joueur en un Vecteur 3D
        float _yRot = Input.GetAxisRaw("Mouse X"); //Horizontal

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivityX;

        motor.Rotate(_rotation);

        //Partie Haut en Bas
        // On va calculer la rotation de la camera en un Vecteur 3D
        float _xRot = Input.GetAxisRaw("Mouse Y"); //Horizontal

        float _cameraRotationX = _xRot * lookSensitivityY;

        motor.RotateCamera(_cameraRotationX);

    }
}
