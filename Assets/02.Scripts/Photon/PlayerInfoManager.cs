using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager Instance { get; private set; }

    public PlayerInfo CurrentPlayerInfo { get; private set; }

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

    public void SetPlayerInfo(string nickName, string characterName)
    {
        CurrentPlayerInfo = new PlayerInfo(nickName, characterName);
    }
}
