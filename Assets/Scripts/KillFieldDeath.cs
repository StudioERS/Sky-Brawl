using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class KillFieldDeath : NetworkBehaviour {
    [SerializeField] Transform spawn;


[ClientRpc] private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            RpcKillPlayer();
        }
    }

    [ClientRpc] void RpcKillPlayer()
    {
        if (isLocalPlayer)
        {
            transform.position = spawn.position;
        }
    } 

}
