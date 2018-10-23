using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGunProjectile : Projectile {


    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        rigidbody.detectCollisions = false;
        SelfDestruct(0.25f);
    }
}
