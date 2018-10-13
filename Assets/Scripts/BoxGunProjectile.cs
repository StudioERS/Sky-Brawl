using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGunProjectile : Projectile {

    Collider collider;
    Rigidbody rigidbody;
    Projectile projectileComponent;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        projectileComponent = GetComponent<Projectile>();



        collider = GetComponent<Collider>();
        Invoke("SelfDestruct", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnCollisionEnter(Collision collision)
    {
        rigidbody.detectCollisions = false;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
