using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGunProjectile : Projectile {

    new Collider collider;
    new Rigidbody rigidbody;
    Projectile projectileComponent;
    // Use this for initialization
    protected override void Start () {
        base.Start();

        rigidbody = GetComponent<Rigidbody>();
        projectileComponent = GetComponent<Projectile>();
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        rigidbody.detectCollisions = false;
    }
}
