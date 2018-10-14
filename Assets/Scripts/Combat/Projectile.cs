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

    //Stashes bullets in there for cleanliness
    [Header("Miscellaneous")] [SerializeField] protected GameObject bulletBin;
 
    protected float maxLifetime = 5f;

    protected virtual void Start()
    {
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

        if (bulletBin != null)
        {
            transform.SetParent(bulletBin.transform);
        }
        Invoke("SelfDestruct", maxLifetime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (hitParticle != null)
        {
            hitParticle.Play();
        }
        CancelInvoke();
        Invoke("SelfDestruct", 0.5f);
    }

    protected void SelfDestruct()       //STRING REFERENCED
    {
        Destroy(gameObject);
    }
}
