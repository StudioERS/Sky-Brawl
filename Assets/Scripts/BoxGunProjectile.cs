using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGunProjectile : Projectile {

    Collider collider;
    Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        

        collider = GetComponent<Collider>();
        Invoke("SelfDestruct", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        DamageHandler target = other.GetComponent<DamageHandler>();
        if (target == null)
        {
            return;
        }

        Vector3 forcePoint = collision.impulse.normalized;

        target.TakeDamage(this, forcePoint);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
