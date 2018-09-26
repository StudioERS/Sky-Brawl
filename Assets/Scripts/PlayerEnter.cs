using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnter : MonoBehaviour {

    MeshRenderer GraphicGunPickUp;
    

	// Use this for initialization
	void Start () {
        GraphicGunPickUp = GetComponent<MeshRenderer>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMotor>().GiveMunition("Gun1");

            GraphicGunPickUp.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
