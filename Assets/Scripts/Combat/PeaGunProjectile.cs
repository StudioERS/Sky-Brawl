using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGunProjectile : Projectile {

    new Rigidbody rigidbody;
    Projectile projectileComponent;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        projectileComponent = GetComponent<Projectile>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        rigidbody.detectCollisions = false;
        Destroy(gameObject);
    }
}
