using UnityEngine;

//Attention besoin de cette librairie
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    // Creation d'une liste de tout les composant qu'on souhaite désactiver

    // Vérifier si le joueur qui possède ce script est notre joueur à nous (notre joueur local)
    // Pour désactiver le nessessaire, car les scripts de tout le monde sons par default utilisé par tout le monde (dans unity)
    // !! DANS UNITY ---> size: 4 (Player Motor, Player Controller, Camera du joueur, Listineur)
    [SerializeField]
    Behaviour[] componentsToDisable; 

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    private void Start()
    {
        // Si ce n'est pas notre joueur nous allons désactiver des composants, lesquelles sont gérés à partir de UNITY
        if (!isLocalPlayer)
        {
            DisableComponents();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                // Désactiver pour le joueur qui rejoint le serveur, mais pas la camera du serveur
                sceneCamera.gameObject.SetActive(false);

            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, player);
    }

    //Assigne à tout les personnage qui n'est pas notre joueur
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        // boucle pour désactiver les components des autres joueurs sur notre instance
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }

    }

    // Lors de la destruction de l'objet
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            // Lorsque le joueur quitte le jeu la camera reprend.
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }
}
