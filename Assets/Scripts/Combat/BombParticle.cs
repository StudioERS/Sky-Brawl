using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParticle : ParticleProjectile {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        volModule.speedModifierMultiplier *= effectRadius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
