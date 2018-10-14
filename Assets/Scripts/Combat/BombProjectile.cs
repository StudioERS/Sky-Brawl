using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile {

    [SerializeField] protected float explosionRadius;
    new Rigidbody rigidbody;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        hitParticle.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (Physics.SphereCast(rigidbody.centerOfMass, explosionRadius, Vector3.zero, out RaycastHit hitInfo))
        {
            //Implement GUI hit feedback here.
        }
    }
}
