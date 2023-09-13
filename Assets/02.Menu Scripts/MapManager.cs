using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using MalbersAnimations.Selector;

public class MapManager : MonoBehaviour
{
    public Transform seoulTr;
    public Transform jejuTr;

    void Start()
    {
        GameObject currentPlayer = GetCurrentPlayerInstance();
        if (currentPlayer != null)
        {
            switch (Data.spawnType)
            {
                case Data.SpawnType.Seoul:
                    currentPlayer.transform.position = seoulTr.position;
                    break;
                case Data.SpawnType.Jeju:
                    currentPlayer.transform.position = jejuTr.position;
                    break;
                default:
                    break; 
            }
        }
    }

   private GameObject GetCurrentPlayerInstance()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            PhotonView pv = player.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                return player.gameObject;
            }
        }
        return null;
    }
}

