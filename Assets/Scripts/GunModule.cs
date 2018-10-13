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
        //Process fire rate.

        equippedGun.ProcessShotCooldown();

        //Call appropriate shoot method.
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && equippedGun.readyToShoot)
        {
            equippedGun.Shoot();
            equippedGun.readyToShoot = false;
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