using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [Header("Graphics")]
    [Tooltip("Particle effect to generate in flight")] [SerializeField] protected ParticleSystem flightParticle;
    [Tooltip("Particle effect to generate on hit")] [SerializeField] protected ParticleSystem hitParticle;

    [Header("Balance")]
    [SerializeField] public float damageValue;
    [SerializeField] public float baseKnockback;
    [SerializeField] public float upwardModifier;
    [SerializeField] public float explosionRadius;
    protected float maxLifetime = 5f;

    protected Rigidbody rigidbody;
    protected Transform transform;
    protected MeshRenderer mesh;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        mesh = GetComponent<MeshRenderer>();
        //Instantiate and activate particles.
        if (flightParticle != null)
        {
            flightParticle = Instantiate(flightParticle, gameObject.transform);
            flightParticle.Play();
        }

        if (hitParticle != null)
        {
            hitParticle = Instantiate(hitParticle, gameObject.transform);
        }
        SelfDestruct(maxLifetime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (hitParticle != null)
        {
            hitParticle.Play();
        }
    }

    protected void SelfDestruct(float timer)
    {
        CancelInvoke();
        Invoke("SelfDestruct", timer);
    }

    protected void SelfDestruct()       //STRING REFERENCED
    {
        Destroy(gameObject);
    }
}
