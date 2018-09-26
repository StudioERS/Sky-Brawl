using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]//Pouvoir re trouver ce paramètre dans le IDE de Unity
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // On va calculer la vélocité du mouvement du joueur en un Vecteur 3D
        // Permet de reconnaitre la direction vers laquelle on se dirige + à quelle vitesse

        
        float _xMov = Input.GetAxisRaw("Horizontal"); //Nous permet de récupérer les touches pour aller à droite et à gauche
        /*
         -1 = left
         0 = le personnage ne bouge pas
         1 = droite
         */
        float _zMov = Input.GetAxisRaw("Vertical"); //Nous permet de récupérer les touches pour avancer et reculer
        /*
         -1 = recule
         0 = ne bouge pas
         1 = avance
         */

        Vector3 _moveHorizontal = transform.right * _xMov; // ( x, y, z) ---> ( 0, 0, 1 )
        Vector3 _moveVertical = transform.forward * _zMov;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        motor.Move(_velocity);

        // On va calculer la rotation du joueur en un Vecteur 3D
        float _yRot = Input.GetAxisRaw("Mouse X"); //Horizontal

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivity;

        motor.Rotate(_rotation);

        //Partie Haut en Bas
        // On va calculer la rotation de la camera en un Vecteur 3D
        float _xRot = Input.GetAxisRaw("Mouse Y"); //Horizontal

        Vector3 _cameraRotation = new Vector3(_xRot, 0, 0) * lookSensitivity;

        motor.RotateCamera(_cameraRotation);
    }
}
