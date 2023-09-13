using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using MalbersAnimations.Selector;

public class MapManager : MonoBehaviour
{
    public Transform seoulTr;
    public Transform jejuTr;
    public NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        string characterName = FindObjectOfType<SelectorManager>().ItemSelected.gameObject.name;

        switch (Data.spawnType)
        {
            case Data.SpawnType.Seoul:
                // if (networkManager != null)
                // {
                //     networkManager.enabled = false;
                // }
                // PhotonNetwork.Instantiate(characterName, seoulTr.position, Quaternion.identity);
                break;
            case Data.SpawnType.Jeju:
                PhotonNetwork.Instantiate(characterName, jejuTr.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    private string GetCurrentPlayerPrefabName()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();
        foreach (var player in allPlayers)
        {
            if (player.photonView.IsMine)
            {
                Debug.Log("Found player's game object name: " + player.gameObject.name);
                return player.gameObject.name;
            }
        }
        return null;
    }
}
