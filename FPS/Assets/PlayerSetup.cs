using UnityEngine;

//Attention besoin de cette librairie
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    // Creation d'une liste de tout les composant qu'on souhaite désactiver

    // Vérifier si le joueur qui possède ce script est notre joueur à nous (notre joueur local)
    // Pour désactiver le nessessaire, car les scripts de tout le monde sons par default utilisé par tout le monde (dans unity)

    [SerializeField]
    Behaviour[] componentsToDisable;

    private void Start()
    {
        // Si ce n'est pas notre joueur nous allons désactiver des composants
        if (!isLocalPlayer)
        {
            // boucle pour désactiver les components des autres joueurs sur notre instance
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }
}
