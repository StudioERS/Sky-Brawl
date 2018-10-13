using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGun : GunBase {

	// Use this for initialization
	void Start () { 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Shoot()
    {

        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit rch;
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            transform.LookAt(rch.point);
            GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform);
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();
            newProBody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
            transform.DetachChildren();
        }
        else
        {
            transform.LookAt(rayFromCamera.GetPoint(100));
            GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform, true);
            Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();

            newProBody.AddRelativeForce( Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
            transform.DetachChildren();
        }
    }
}
