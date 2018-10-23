using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleProjectile : MonoBehaviour {

    [Header("Balance")]
    [SerializeField] public float damageValue;
    [SerializeField] public float baseKnockback;
    [SerializeField] public float upwardModifier;
    [SerializeField] public float effectRadius;

    new protected Transform transform;
    protected ParticleSystem particle;
    protected ParticleSystem.VelocityOverLifetimeModule volModule;

    // Use this for initialization
    protected virtual void Start () {
        transform = GetComponent<Transform>();
        particle = GetComponent<ParticleSystem>();
        volModule = particle.velocityOverLifetime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
