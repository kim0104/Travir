using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Selector;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class NetworkManager : MonoBehaviourPun
{
    private static bool hasInstantiated = false;

    void Start()
    {
        if (!hasInstantiated && PhotonNetwork.IsConnected)
        {
            string characterName = FindObjectOfType<SelectorManager>().ItemSelected.gameObject.name;
            Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
            PhotonNetwork.Instantiate(characterName, spawnPoint.position, Quaternion.identity);
            hasInstantiated = true;
        }
    }
}