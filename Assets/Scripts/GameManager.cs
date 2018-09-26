
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private const string PLAYER_ID_PREFIX = "Player ";


    // Dictionnaire de tout les joueurs dans le jeu
    private static Dictionary<string, Player> Players = new Dictionary<string, Player>();


    // Appelé à chaque fois qu'un joueur ce connecte
    public static void RegisterPlayer(string _netId, Player _player)
    {
        Players.Add(_player.transform.name = PLAYER_ID_PREFIX + _netId, _player);
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        Players.Remove(_playerID);
    }

    public static Player GetPlayer(string _PlayerID)
    {
        return Players[_PlayerID];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _playerID in Players.Keys)
    //        GUILayout.Label(_playerID + " - " + Players[_playerID].transform.name);

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}
}
