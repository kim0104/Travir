using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Selector;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class NetworkManager : MonoBehaviourPun
{
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            // 캐릭터 선택 씬에서 선택한 캐릭터의 이름을 찾는다.
            string characterName = FindObjectOfType<SelectorManager>().ItemSelected.gameObject.name;

            // SpawnPoint 위치를 찾는다.
            Transform spawnPoint = GameObject.Find("SpawnPoint").transform;

            // SpawnPoint 위치에서 선택한 캐릭터를 인스턴스화 한다.
            PhotonNetwork.Instantiate(characterName, spawnPoint.position, Quaternion.identity);
        }
    }
}
