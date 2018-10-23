using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGunProjectile : Projectile {

    Projectile projectileComponent;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        projectileComponent = GetComponent<Projectile>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCollisionEnter(Collision collision)
    {
        rigidbody.detectCollisions = false;
        base.OnCollisionEnter(collision);

    }
}
