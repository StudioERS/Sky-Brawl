using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunModule : MonoBehaviour {

    [SerializeField] GameObject defaultGunPrefab;
    [SerializeField] GameObject[] gunPrefabs;
    private GameObject equippedGunPrefab;
    private GunBase equippedGun;
    private List<GunBase> availableGuns;

    private Animator anim;

    private void Start()
    {
        equippedGunPrefab = Instantiate(defaultGunPrefab, gameObject.transform);

        equippedGun = equippedGunPrefab.GetComponent<GunBase>();
    }

    private void Update()
    {
        equippedGun.ProcessShotCooldown();
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && equippedGun.readyToShoot)
        {
            equippedGun.Shoot();
            equippedGun.readyToShoot = false;
        }
    }
}

public abstract class GunBase : MonoBehaviour
{
    protected int ammo;
    protected float shotTimer = 0f;



    [Tooltip("Class to use as projectile")] [SerializeField] protected GameObject projectilePrefab;

    [Header("Balance")]
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float fireRate;
    [SerializeField] public float damageValue;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] public float baseKnockback;

    public bool readyToShoot = true;

    public void ProcessShotCooldown()
    {
        if (!readyToShoot)
        {
            shotTimer += Time.deltaTime;

            if (shotTimer >= fireRate)
            {
                readyToShoot = true;
                shotTimer = 0f;
            }
        }
    }
    abstract public void Shoot();
}