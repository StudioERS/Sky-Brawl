using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class KillFieldDeath : NetworkBehaviour {
    


[ClientRpc] private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            RpcRespawn();
        }
    }

    [ClientRpc] void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            
        }
    } 

}
