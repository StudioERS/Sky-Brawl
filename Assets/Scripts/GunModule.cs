using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunModule : MonoBehaviour {

    [SerializeField] GameObject defaultGunPrefab;
    private GameObject equippedGunPrefab;
    private GunBase equippedGun;
    private List<GunBase> availableGuns;

    private void Start()
    {
        equippedGunPrefab = Instantiate(defaultGunPrefab, gameObject.transform);

        equippedGun = equippedGunPrefab.GetComponent<GunBase>();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            equippedGun.Shoot(gameObject);
        }
    }
}

public abstract class GunBase : MonoBehaviour
{
    protected int ammo;

    [Header("Projectile")]
    [Tooltip("Class to use as projectile")] [SerializeField] protected GameObject projectileMesh;
    [Tooltip("Particle effect to generate in flight")] [SerializeField] protected ParticleSystem flightParticle;
    [Tooltip("Particle effect to generate on hit")] [SerializeField] protected ParticleSystem hitParticle;

    [SerializeField] protected float fireRate;
    [SerializeField] protected float damageValue;
    [SerializeField] protected float baseKnockback;


    abstract public void Shoot(GameObject origin);
}