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
        GameObject newProjectile = Instantiate(projectileMesh, gameObject.transform, true);
        Rigidbody newProBody = newProjectile.GetComponent<Rigidbody>();
        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 200f);
        RaycastHit rch;
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            transform.LookAt(rch.point);
            Vector3 gunFacing = transform.rotation.eulerAngles.normalized;
            newProBody.AddForce(gunFacing * projectileSpeed, ForceMode.VelocityChange);
        }
        else
        {
            transform.LookAt(rayFromCamera.GetPoint(100));
            Vector3 gunFacing = transform.rotation.eulerAngles.normalized;
            newProBody.AddForce(gunFacing * projectileSpeed, ForceMode.VelocityChange);
        }
    }
}
