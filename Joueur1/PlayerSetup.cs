using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    private void Start()
    {
        //boucle pour desactiver les components des autres joueurs sur notre instance
        if (!isLocalPlayer)
        {
            for (int i =0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }

        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera !=null)
            { 
                sceneCamera = Camera.main;
                Camera.main.transform.gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
           Camera.main.transform.gameObject.SetActive(true);
        }
    }

}
