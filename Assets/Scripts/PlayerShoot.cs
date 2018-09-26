using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    [SerializeField]
    public float knocktback = 10f;

    [SerializeField]
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera Cam; // CAmera du joueur

    [SerializeField]
    private LayerMask mask; // Tout ce que le raycast peut toucher

	void Start () {
		if(Cam == null) { this.enabled = false; }
	}

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client] // Ce passe seulement dans la partie client
    private void Shoot()
    {
        RaycastHit _hit;
        Vector3 HitOrigine = Cam.transform.position;
        Vector3 HitDirection = Cam.transform.forward;
        if (Physics.Raycast(HitOrigine, HitDirection, out _hit, weapon.range))
        {
            if(_hit.collider.tag == "Player")
            {
                CmdPlayerShot(_hit.collider.name , weapon.damage, HitOrigine, HitDirection);
            }
        }
    }

    [Command] // Ce passe dans la section serveur
    void CmdPlayerShot(string _playerId, int _damage, Vector3 pHitHorigine, Vector3 pHitDirection)
    {
        // est appelé par la fonction Shoot() 
        Debug.Log(_playerId + " a été touché.");

        Player _player = GameManager.GetPlayer(_playerId);
        _player.TakeDamage(_damage);

        Vector3 _velocity = (pHitHorigine + pHitDirection).normalized * knocktback; // Vector3 (moveHorizontal, _yRot, moveVertical);

        _player.MoveDamaged(_velocity);
    }

}
