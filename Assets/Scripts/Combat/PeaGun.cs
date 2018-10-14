using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGun : GunBase
{
    LineRenderer laser;

    protected override void Start()
    {
        base.Start();
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    public override void Shoot()
    {

        base.Shoot();                           //Handles firing rate and cooldown.
        ShowLaser();
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
