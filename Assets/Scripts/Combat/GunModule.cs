using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunModule : MonoBehaviour {


}

public abstract class GunBase : MonoBehaviour
{
    //Todo implement ammo
    protected int ammo;
    protected float shotTimer = 0f;

    public Vector3 posOffset;



    [Tooltip("Class to use as projectile")] [SerializeField] protected GameObject projectilePrefab;

    [Header("Balance")]
    [SerializeField] protected int maxAmmo;
    [SerializeField] [Tooltip("Cooldown time")] protected float fireRate;
    [SerializeField] protected float projectileSpeed;

    protected bool readyToShoot = true;
    protected ProjectileModule projectileModule;
    protected Transform pmTransform;

    public bool ReadyToShoot { get { return readyToShoot; } }

    public BulletBin bulletBin;

    protected virtual void Start()
    {
        //Finds the projectile module attached to child prefab.
        projectileModule = GetComponentInChildren<ProjectileModule>();
        pmTransform = projectileModule.transform;

        bulletBin = FindObjectOfType<BulletBin>();
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

    virtual public void Shoot()
    {
        readyToShoot = false;

        //Casts ray from camera through middle of the screen.
        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit rch;

        GameObject newProjectile;

        //If the ray hit something
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            //Turn gun towards thing it hit
            transform.LookAt(rch.point);

            newProjectile = (GameObject)Instantiate(projectilePrefab, pmTransform.position, pmTransform.rotation);
            Transform newProTrans = newProjectile.GetComponent<Transform>();
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();

            //Detach projectile and place in trashcan object.
            newProTrans.SetParent(bulletBin.GetComponent<Transform>());

            //Shoot projectile.

            newProBody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
        }
        else
        {
            transform.LookAt(rayFromCamera.GetPoint(100));

            newProjectile = (GameObject)Instantiate(projectilePrefab, pmTransform.position, pmTransform.rotation);
            Transform newProTrans = newProjectile.GetComponent<Transform>();
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();

            //Detach projectile and place in trashcan object.
            newProTrans.SetParent(bulletBin.GetComponent<Transform>());

            newProBody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
        }

        NetworkServer.Spawn(newProjectile);
    }
}