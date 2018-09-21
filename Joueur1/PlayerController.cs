using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lookSensivityX = 3f;
    [SerializeField]
    private float lookSensitivityY = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // on va calculer la velocite du mouvement du joueur en un vecteur 3D

        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        // on va calculer la rotation du joueur en un vecteur 3D
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensivityX;

        motor.Rotate(_rotation);

        // on va calculer la camera de la camera en un vecteur 3D
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivityY;

        motor.RotateCamera(_cameraRotationX);

        //on va faire un JUMP
        float _yMov = Input.GetAxisRaw("Jump");
        Vector3 _movUp = transform.up * _yMov;
        

    }


}
