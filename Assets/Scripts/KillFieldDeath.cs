using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class KillFieldDeath : NetworkBehaviour {
    [SerializeField] Transform spawn;
    

    private void OnTriggerEnter(Collider other)
    {
       
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {

            RpcKillPlayer(other);
            
        }
    }

   [ClientRpc] void RpcKillPlayer(Collider other)
    {
        if (isLocalPlayer)
        {

            GameObject go = other.gameObject;
            Destroy(other.gameObject);

             Instantiate(go, spawn.transform, spawn);
            PlayerController respawn = go.GetComponent<PlayerController>();
            PlayerMotor pmrespawn = go.GetComponent<PlayerMotor>();



        }
    } 

}
