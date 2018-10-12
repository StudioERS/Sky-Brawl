using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGun : GunBase
{
    public void Start()
    {
        flightParticle = Instantiate(flightParticle, gameObject.transform);
    }

    public PeaGun()
    {

    }

    public override void Shoot(GameObject origin)
    {
        print(flightParticle.transform.parent);
        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        flightParticle.Play();
        Debug.DrawRay(rayFromCamera.origin, rayFromCamera.direction, Color.yellow, 1f);
        RaycastHit rch;
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            flightParticle.transform.localPosition = Vector3.RotateTowards(flightParticle.transform.localPosition, rch.point, float.MaxValue, float.MaxValue);
            flightParticle.Play();
            GunModule.print("Hit " + rch.transform.name + " for " + damageValue);
        }
        else
        {
            Quaternion camRotation = Camera.main.transform.rotation;
            flightParticle.transform.rotation = camRotation;
            flightParticle.Play();
            print("Hit nothing");
        }
    }
}
