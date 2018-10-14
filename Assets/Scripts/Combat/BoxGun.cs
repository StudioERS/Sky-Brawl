using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGun : GunBase {

    public override void Shoot()
    {

        base.Shoot();               //Base.Shoot handles fire rate. Returns if the gun isn't ready to fire otherwise sets readyToShoot = false
    }
}
