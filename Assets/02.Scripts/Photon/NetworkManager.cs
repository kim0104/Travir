using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Selector;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class NetworkManager : MonoBehaviourPun
{
    public Transform spawnPoint;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            string characterName = PlayerInfoManager.Instance.CurrentPlayerInfo.CharacterName;
            PhotonNetwork.Instantiate(characterName, spawnPoint.position, Quaternion.identity);
        }
    }
}