using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunModule : MonoBehaviour {

    private Gun equippedGun;
    private List<Gun> availableGuns;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            equippedGun.Shoot(gameObject);
        }
    }
}

[System.Serializable]
public abstract class Gun : System.Object
{
    private int ammo;

    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float damageValue;
    [SerializeField] protected float baseKnockback;

    [Header("Projectile")]
    [Tooltip("Class to use as projectile")] [SerializeField] GameObject projectileMesh;
    [Tooltip("Particle effect to generate in flight")] [SerializeField] ParticleSystem flightParticle;
    [Tooltip("Particle effect to generate on hit")] [SerializeField] ParticleSystem hitParticle;


    abstract public void Shoot(GameObject origin);
}

public class PeaGun : Gun
{
    public override void Shoot(GameObject origin)
    {
        if (Physics.Raycast(new Ray(origin.transform.position, origin.transform.rotation.eulerAngles), out RaycastHit rch))
        {
            DamageHandler targetDamageHandler = rch.transform.GetComponent<DamageHandler>();
            targetDamageHandler.TakeDamage(origin, damageValue)
        }
    }
}