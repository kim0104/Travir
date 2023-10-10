using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private int nextSceneLevel;
    private bool shouldJoinRoom = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeToRoom(int sceneLevel)
    {
        nextSceneLevel = sceneLevel;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        shouldJoinRoom = true;
    }

    public override void OnConnectedToMaster()
    {
        if (shouldJoinRoom)
        {
            PhotonNetwork.JoinOrCreateRoom(nextSceneLevel.ToString(), new RoomOptions { MaxPlayers = 4 }, null);
            shouldJoinRoom = false;
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(nextSceneLevel);
    }
}
