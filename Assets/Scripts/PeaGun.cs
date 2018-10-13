﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGun : GunBase
{
    LineRenderer laser;

    public void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    public PeaGun()
    {

    }

    public override void Shoot()
    {
        Ray rayFromCamera = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit rch;
        if (Physics.Raycast(rayFromCamera, out rch))
        {
            transform.LookAt(rch.point);
            ShowLaser();


            DamageHandler target;
            if (rch.transform.GetComponent<DamageHandler>() != null)
            {
                target = rch.transform.GetComponent<DamageHandler>();
                target.TakeDamage(gameObject, rch.point);
            }
        }
        else
        {
            transform.LookAt(rayFromCamera.GetPoint(100));
            ShowLaser();
        }
    }

    private void ShowLaser()
    {
        laser.enabled = true;
        Invoke("HideLaser", 0.05f);
    }

    private void HideLaser()  //STRING REFERENCE
    {
        laser.enabled = false;
    }
}