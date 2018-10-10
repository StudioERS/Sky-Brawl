using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class KillFieldDeath : MonoBehaviour {
    [SerializeField] Transform spawn;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Destroy(other.gameObject);
          //  RpcKillPlayer();
        }
    }

  /*  [ClientRpc] void RpcKillPlayer()
    {
        if (isLocalPlayer)
        {
            transform.position = spawn.position;
        }
    } */

}
