using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile {

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        GetComponent<Collider>().enabled = false;
        rigidbody.detectCollisions = false;
        base.OnCollisionEnter(collision);
        transform.DetachChildren();

        mesh.enabled = false;
        SelfDestruct();
    }
}
