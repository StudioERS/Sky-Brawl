using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MushroomScript : NetworkBehaviour {

    public Collider mCollider;

	// Use this for initialization
	void Start () {
        mCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
