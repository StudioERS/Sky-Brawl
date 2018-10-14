using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunModule : MonoBehaviour {

    [SerializeField] GameObject defaultGunPrefab;
    [SerializeField] GameObject[] gunPrefabs;
    private GameObject equippedGunPrefab;
    private GunBase equippedGun;
    private List<GameObject> availableGuns;

    private int gunIndex = 0;
    private Animator anim;

    private void Start()
    {

        //Instantiates all guns, then equips first gun in array (index 0).
        availableGuns = new List<GameObject>();


        foreach (GameObject gun in gunPrefabs)
        {
            print(gun);
            availableGuns.Add(Instantiate(gun, gameObject.transform));
            print(availableGuns.Count);
        }

        EquipGun();
    }

    private void EquipGun()
    {
        //Todo activate/de-active meshes once we have them.

        //Looping
        if (gunIndex == availableGuns.Count)
        {
            gunIndex = 0;
        }
        else if (gunIndex < 0)
        {
            gunIndex = availableGuns.Count - 1;
        }

        //Change equipped gun prefab and class.
        equippedGunPrefab = availableGuns[gunIndex];
        equippedGun = equippedGunPrefab.GetComponent<GunBase>();
    }

    private void Update()
    {

        //Call appropriate shoot method.
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            equippedGun.Shoot();
        }

        //Weapon swapping.
        else if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") > 0f)  // Forward
        {
            ++gunIndex;
            EquipGun();
        }
        else if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") < 0f) // backward
        {
            --gunIndex;
            EquipGun();
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

    protected bool readyToShoot = true;
    protected ProjectileModule projectileModule;

    protected virtual void Start()
    {
        projectileModule = GetComponentInChildren<ProjectileModule>();
    }

    public void ProcessShotCooldown()
    {
        //only proceed if not ready to shoot.
        if (!readyToShoot)
        {
            //Adds to timer.
            shotTimer += Time.deltaTime;

            //If timer reaches firing rate, set ready to shoot to true and reset timer.
            if (shotTimer >= fireRate)
            {
                readyToShoot = true;
                shotTimer = 0f;
            }
        }
    }

    protected void Update()
    {
        ProcessShotCooldown();
    }

    protected void CreateProjectile()
    {
        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit rch;
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            transform.LookAt(rch.point);
            GameObject newProjectile = Instantiate(projectilePrefab, projectileModule.transform);
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();
            newProBody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
        }
        else
        {
            transform.LookAt(rayFromCamera.GetPoint(100));
            GameObject newProjectile = Instantiate(projectilePrefab, projectileModule.transform);
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();

            newProBody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
        }
    }

    virtual public void Shoot()
    {
        if (!readyToShoot) { return; }
        readyToShoot = false;

        CreateProjectile();
    }
}