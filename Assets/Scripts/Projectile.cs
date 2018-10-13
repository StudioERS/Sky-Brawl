using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [Header("Graphics")]
    [Tooltip("Particle effect to generate in flight")] [SerializeField] protected ParticleSystem flightParticle;
    [Tooltip("Particle effect to generate on hit")] [SerializeField] protected ParticleSystem hitParticle;

    [Header("Balance")]
    [SerializeField] public float damageValue;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] public float baseKnockback;
}
